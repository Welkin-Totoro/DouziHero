using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : Singleton<StaticData>
{
    Dictionary<Arm, SoldierInfo> m_Soldiers = new Dictionary<Arm, SoldierInfo>();
    Dictionary<TowerType, TowerInfo> m_Towers = new Dictionary<TowerType, TowerInfo>();
    Dictionary<SkillType, SkillInfo> m_Skills = new Dictionary<SkillType, SkillInfo>();

    protected override void Awake()
    {
        base.Awake();

        InitSoldiers();
        InitTowers();
        InitSkills();
    }

    private void InitSoldiers()
    {
        m_Soldiers.Add(Arm.Swordsman, new SoldierInfo()
        { armType = Arm.Swordsman, AttackAmount = 1, AttackRange = 8, AttackRate = 1, Cost = 5, MaxHp = 10, Speed = 2 });
        m_Soldiers.Add(Arm.Knight, new SoldierInfo()
        { armType = Arm.Knight, AttackAmount = 1, AttackRange = 8, AttackRate = 1, Cost = 6, MaxHp = 10, Speed = 2 });
        m_Soldiers.Add(Arm.LanceKnight, new SoldierInfo()
        { armType = Arm.LanceKnight, AttackAmount = 1, AttackRange = 13, AttackRate = 1, Cost = 8, MaxHp = 10, Speed = 2 });
        m_Soldiers.Add(Arm.Hunter, new SoldierInfo()
        { armType = Arm.Hunter, AttackAmount = 1, AttackRange = 40, AttackRate = 1, Cost = 9, MaxHp = 10, Speed = 2 });
        m_Soldiers.Add(Arm.Horseman, new SoldierInfo()
        { armType = Arm.Horseman, AttackAmount = 1, AttackRange = 8, AttackRate = 1, Cost = 10, MaxHp = 10, Speed = 2 });

        m_Soldiers.Add(Arm.Leader, new SoldierInfo()
        { armType = Arm.Leader, AttackRange = 50 });
    }

    private void InitTowers()
    {
        m_Towers.Add(TowerType.small, new TowerInfo()
        { towerType = TowerType.small, Height = 7.2f, MaxHp = 25, AttackDamage = 2, AttackRange = 45, AttackRate = 1f });
        m_Towers.Add(TowerType.big, new TowerInfo()
        { towerType = TowerType.big, Height = 6f, MaxHp = 30, AttackDamage = 0, AttackRange = 0, AttackRate = 0.5f });
    }

    private void InitSkills()
    {
        m_Skills.Add(SkillType.FireBall, new SkillInfo()
        { skillType = SkillType.FireBall, Cost = 10, AttackAmount = 5 });
        m_Skills.Add(SkillType.ArrowRain, new SkillInfo()
        { skillType = SkillType.ArrowRain, Cost = 20, AttackAmount = 10 });
        m_Skills.Add(SkillType.Lighting, new SkillInfo()
        { skillType = SkillType.Lighting, Cost = 50, AttackAmount = 50 });
    }

    public SoldierInfo GetSoldierInfo(Arm armType)
    {
        return m_Soldiers[armType];
    }
    public TowerInfo GetTowerInfo(TowerType towerType)
    {
        return m_Towers[towerType];
    }
    public SkillInfo GetSkillInfo(SkillType skillType)
    {
        return m_Skills[skillType];
    }
}
