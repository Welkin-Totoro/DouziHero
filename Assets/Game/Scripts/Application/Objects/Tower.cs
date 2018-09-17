using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Tower : Role
{
    //protected Animator m_Animator;
    private float m_LastAttackTime = 0;
    public Transform shootPoint;
    public float DelayToDestroy = 2f;


    public TowerType towerType;
    public Camp camp;

    public float Height
    { get; private set; }
    public float DestorySpeed
    { get; private set; }
    public int AttackAmount
    { get; private set; }
    public float AttackRate
    { get; private set; }
    public float AttackRange
    { get; private set; }

    public Tower(TowerType towerType, Camp camp)
    {
        this.towerType = towerType;
        this.camp = camp;
    }

    private void Start()
    {
        base.OnSpawn();
        Load();

        //m_Animator = GetComponent<Animator>();
        //m_Animator.SetBool("IsDead", false);
    }
    private void Update()
    {
        Damage(LookupEnemy());
    }

    public virtual void Load()
    {
        //Debug.Log(towerType);
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerType);
        Height = info.Height;
        AttackAmount = info.AttackDamage;
        AttackRate = info.AttackRate;
        AttackRange = info.AttackRange;
        Hp = MaxHp = info.MaxHp;

        DestorySpeed = (float)Height / DelayToDestroy;
    }

    private Soldier LookupEnemy()
    {
        //Lookup Enemy
        GameObject[] soldiers = null;
        if (gameObject.tag == Tags.YELLOW)
            soldiers = GameObject.FindGameObjectsWithTag(Tags.GREEN);
        else if (gameObject.tag == Tags.GREEN)
            soldiers = GameObject.FindGameObjectsWithTag(Tags.YELLOW);

        if (soldiers == null) return null;

        float minDis = Mathf.Infinity;
        Soldier target = null;
        foreach (GameObject soldier in soldiers)
        {
            //Debug.Log(role);
            Soldier s = soldier.GetComponent<Soldier>();
            if (s == null)
                continue;
            if (Vector3.Distance(gameObject.transform.position, s.transform.position) < AttackRange
                && !s.IsDead
                && Vector3.Distance(gameObject.transform.position, s.transform.position) < minDis)
            {
                minDis = Vector3.Distance(gameObject.transform.position, s.transform.position);
                target = s;
            }
        }
        return target;
    }
    private void SniperFire(Soldier target)
    {
        shootPoint.LookAt(target.transform);
        GameObject go1 = Game.Instance.ObjectPool.Spawn("TowerBulletMuzzle");
        go1.transform.position = shootPoint.position;
        go1.transform.rotation = shootPoint.rotation;
        GameObject go2 = Game.Instance.ObjectPool.Spawn("TowerBulletBeam");
        go2.transform.position = shootPoint.position;
        go2.transform.rotation = shootPoint.rotation;
        GameObject go3 = Game.Instance.ObjectPool.Spawn("TowerBulletImpact");
        go3.transform.position = target.transform.position;

        StartCoroutine(UnspawnBullet(go1, go2,go3));
    }

    public virtual void Damage(Soldier target = null)
    {
        if (towerType == TowerType.big)
            return;
        if (target == null)
            return;

        //Debug.Log(name + " hit " + target.name);

        if (Time.time - m_LastAttackTime < 1 / AttackRate)
            return;

        m_LastAttackTime = Time.time;
        target.GetDamage(AttackAmount);

        //Animation
        SniperFire(target);

        //Sound
    }
    IEnumerator UnspawnBullet(GameObject go1,GameObject go2,GameObject go3)
    {
        yield return new WaitForSeconds(0.2f);
        Game.Instance.ObjectPool.Unspawn(go1);
        Game.Instance.ObjectPool.Unspawn(go2);
        Game.Instance.ObjectPool.Unspawn(go3);
    }
    IEnumerator DestroyTowerCoroutine(Role obj)
    {
        Destroy(obj.gameObject, DelayToDestroy);
        while (true)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, transform.position.y - Height, DestorySpeed * Time.deltaTime), transform.position.z);
            yield return new WaitForFixedUpdate();
        }
    }

    public override void Die(Role role)
    {
        StartCoroutine(DestroyTowerCoroutine(this));
        //Destroy(gameObject, DelayToDestroy);

        //Animation

        //Sound

    }
    private void OnDestroy()
    {
        if (towerType == TowerType.big)
        {
            if (camp == Camp.GREEN)
                MVC.SendEvent(Consts.E_Win);
            else if (camp == Camp.YELLOW)
                MVC.SendEvent(Consts.E_Lose);
        }
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        Load();

        //m_Animator = GetComponent<Animator>();
        //m_Animator.SetBool("IsDead", false);
    }
    public override void OnUnspawn()
    {
        base.OnUnspawn();

        m_LastAttackTime = 0;

        AttackAmount = 0;
        AttackRate = 0;
        AttackRange = 0;
    }
}