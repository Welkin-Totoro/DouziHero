using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Leader : Role
{
    protected Animator m_Animator;
    //private float m_LastAttackTime = 0;
    //public float DelayToDestroy = 2f;

    private Arm armType;
    public bool isWarning;

    public float AttackRange
    { get; private set; }

    public Leader()
    {
        this.armType = Arm.Leader;
    }

    private void Start()
    {
        Load();

        m_Animator = GetComponent<Animator>();
        isWarning = false;
        m_Animator.SetBool("IsWarning", isWarning);
    }
    private void Update()
    {
        Warning(LookupEnemy());
    }

    public virtual void Load()
    {
        SoldierInfo info = Game.Instance.StaticData.GetSoldierInfo(armType);
        AttackRange = info.AttackRange;
    }

    public Soldier LookupEnemy()
    {
        //Lookup Enemy
        GameObject[] soldiers = null;
        if (gameObject.tag == Tags.YELLOWLEADER)
            soldiers = GameObject.FindGameObjectsWithTag(Tags.GREEN);
        else if (gameObject.tag == Tags.GREENLEADER)
            soldiers = GameObject.FindGameObjectsWithTag(Tags.YELLOW);

        if (soldiers == null) return null;

        Soldier target = null;
        foreach (GameObject soldier in soldiers)
        {
            //Debug.Log(role);
            Soldier s = soldier.GetComponent<Soldier>();
            if (s == null)
                continue;
            if (Vector3.Distance(gameObject.transform.position, s.transform.position) < AttackRange
                && !s.IsDead)
            {
                target = s;
                break;
            }
        }
        return target;
    }
    public void Warning(Soldier target = null)
    {
        if (target == null)
        {
            //Animation
            isWarning = false;
            m_Animator.SetBool("IsWarning", isWarning);
        }
        else
        {
            //Animation
            isWarning = true;
            m_Animator.SetBool("IsWarning", isWarning);
        }
    }

    public override void Die(Role role)
    {
        //NO, I WILL NEVER DIE
    }
}