using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Role : ReusableObject, IReusable
{
    public event Action<int, int> HpChanged;
    public event Action<Role> Dead;

    private int m_Hp;
    private int m_maxHp;

    public int Hp
    {
        get { return m_Hp; }
        set
        {
            value = Mathf.Clamp(value, 0, MaxHp);

            if (value == m_Hp)
                return;

            m_Hp = value;
            if (HpChanged != null)
                HpChanged(m_Hp, m_maxHp);

            if (IsDead)
                if (Dead != null)
                    Dead(this);
        }
    }
    public int MaxHp
    {
        get { return m_maxHp; }
        protected set
        {
            if (value <= 0)
                value = 0;

            m_maxHp = value;
        }
    }
    public bool IsDead
    { get { return m_Hp == 0; } }

    //public virtual void Damage()
    //{ }
    public virtual void GetDamage(int hit)
    {
        if (IsDead)
            return;

        Hp -= hit;
        //Debug.Log(name + "GetDamage");
    }
    public abstract void Die(Role role);

    public override void OnSpawn()
    {
        Dead += Die;
    }
    public override void OnUnspawn()
    {
        Hp = 0;
        MaxHp = 0;

        HpChanged = null;
        Dead = null;
    }
}
