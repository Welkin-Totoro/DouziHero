using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SoldierFSM))]
public class Soldier : Role
{
    protected Animator m_Animator;
    private float m_LastAttackTime = 0;
    public float DelayToDestroy = 2f;

    public Camp campType;
    public Arm armType;

    /// <summary>
    /// 花费能量
    /// </summary>
    public int Cost
    { get; private set; }
    /// <summary>
    /// 移动速度
    /// </summary>
    public int Speed
    { get; private set; }
    /// <summary>
    /// 攻击伤害量
    /// </summary>
    public int AttackAmount
    { get; private set; }
    /// <summary>
    /// 攻击速度：每秒攻击的次数
    /// </summary>
    public float AttackRate
    { get; private set; }
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange
    { get; private set; }

    public Soldier(Arm arm, Camp camp)
    {
        armType = arm;
        campType = camp;
    }

    //protected virtual void Awake() { }
    private void Update()
    {
        LookAt(GetComponent<SoldierFSM>().target);
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public virtual void Load()
    {
        SoldierInfo info = Game.Instance.StaticData.GetSoldierInfo(armType);
        Cost = info.Cost;
        Speed = info.Speed;
        AttackAmount = info.AttackAmount;
        AttackRate = info.AttackRate;
        AttackRange = info.AttackRange;
        Hp = MaxHp = info.MaxHp;
    }
    private void LookAt(GameObject target)
    {
        if (target == null)
            return;
        else
        {
            //float tx = transform.eulerAngles.x;
            //float tz = transform.eulerAngles.z;
            transform.LookAt(new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z));
            //transform.eulerAngles = new Vector3(tx, transform.eulerAngles.y, tz);
        }
    }
    public void PerformIdlle(float forward)
    {
        m_Animator.SetFloat("Forward", 0);
        m_Animator.ResetTrigger("Attack");
    }
    public void MoveToTarget(GameObject target)
    {
        //MOVE FROM SOLDIER TO TARGET
        transform.Translate((target.transform.position - transform.position).normalized * 0.1f * Speed, Space.World);
        //Animation
        m_Animator.SetFloat("Forward", 1);

    }
    public virtual void Damage(Role target)
    {
        if (Time.time - m_LastAttackTime < 1 / AttackRate)
            return;

        //Debug.Log(name + " hit " + target.name);

        //Model
        m_LastAttackTime = Time.time;
        target.GetDamage(AttackAmount);

        //Animation
        m_Animator.SetTrigger("Attack");

        //Sound
    }

    public override void Die(Role role)
    {
        GetComponent<CapsuleCollider>().enabled = false;
    }


    public override void OnSpawn()
    {
        base.OnSpawn();
        Load();

        GetComponent<CapsuleCollider>().enabled = true;
        m_Animator = GetComponent<Animator>();
        m_Animator.SetFloat("Forward", 0);
        m_Animator.SetBool("IsDead", false);
    }
    public override void OnUnspawn()
    {
        base.OnUnspawn();

        m_Animator.ResetTrigger("Attack");
        m_Animator = null;
        m_LastAttackTime = 0;

        Cost = 0;
        Speed = 0;
        AttackAmount = 0;
        AttackRate = 0;
        AttackRange = 0;
    }

}
