using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Skill : ReusableObject, IReusable
{
    public event Action<Skill> Dead;

    //protected Animator m_Animator;
    public float DelayToDestroy = 2f;

    public SkillType skillType;
    public Camp campType;


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

    public Skill(SkillType skillType, Camp campType)
    {
        this.skillType = skillType;
        this.campType = campType;
    }

    //protected virtual void Awake() { }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");

        if (other.tag == Tags.YELLOW)
            return;

        Soldier soldier = other.GetComponent<Soldier>();
        if (soldier == null)
            return;

        Damage(soldier);
    }

    public virtual void Load()
    {
        SkillInfo info = Game.Instance.StaticData.GetSkillInfo(skillType);
        Cost = info.Cost;
        Speed = info.Speed;
        AttackAmount = info.AttackAmount;
        AttackRate = info.AttackRate;
        AttackRange = info.AttackRange;
    }
    public virtual void Damage(Soldier target)
    {
        target.GetDamage(AttackAmount);
    }
    IEnumerator DelayToDestroyCoroutine()
    {
        yield return new WaitForSeconds(DelayToDestroy);
        Dead(this);
    }

    public void Die(Skill skill)
    {
    }


    public override void OnSpawn()
    {
        Load();

        Dead += Die;
        StartCoroutine(DelayToDestroyCoroutine());
    }
    public override void OnUnspawn()
    {
        Cost = 0;
        Speed = 0;
        AttackAmount = 0;
        AttackRate = 0;
        AttackRange = 0;
    }

}
