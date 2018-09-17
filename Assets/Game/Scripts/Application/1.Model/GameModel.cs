using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameModel : Model
{

    private int m_Gold = 100;
    private int m_Energy = 0;
    private bool m_isPlaying = false;

    public int FireBallCount = 0;
    public int ArrowRainCount = 0;
    public int LightingCount = 0;

    public bool isUsedFireBall = false;
    public bool isUsedArrowRain = false;
    public bool isUsedLighting = false;


    public override string Name
    {
        get { return Consts.M_GameModel; }
    }

    public int Energy
    {
        get { return m_Energy; }
        private set
        {
            m_Energy = Mathf.Clamp(value, 0, 100);
        }
    }
    public int Gold
    {
        get { return m_Gold; }
        set { m_Gold = value; }
    }
    public bool IsPlaying
    {
        get { return m_isPlaying; }
        set { m_isPlaying = value; }
    }

    public Arm CurrentSpawnType
    { get; private set; }
    public SkillType CurrentSkillType
    { get; private set; }

    public void ReceiveEnergy(int n)
    { Energy += n; }
    public void ConsumeEnergy(int n)
    { Energy -= n; }
    public void SetCurrentSpawnType(Arm arm)
    {
        CurrentSkillType = SkillType.NULL;
        MVC.GetView<UISkillBtn>().txtCurrentChoice.text = "当前选择：" + "无";

        CurrentSpawnType = arm;
    }
    public void SetCurrentSkillType(SkillType skillType)
    {
        CurrentSpawnType = Arm.NULL;
        MVC.GetView<UIInsBtn>().txtCurrentChoice.text = "当前选择：" + "无";

        CurrentSkillType = skillType;
    }
    public bool CanInsSoldier(Arm arm)
    {
        return Energy > Game.Instance.StaticData.GetSoldierInfo(arm).Cost;
    }

    public int GetSkillCount(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.NULL:
                throw new ArgumentNullException("skillType");
            case SkillType.FireBall:
                return FireBallCount;
            case SkillType.ArrowRain:
                return ArrowRainCount;
            case SkillType.Lighting:
                return LightingCount;
            default:
                throw new ArgumentException("Wrong SkillType", "skillType");
        }
    }
    public bool GetSkillState(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.NULL:
                throw new ArgumentNullException("skillType");
            case SkillType.FireBall:
                return isUsedFireBall;
            case SkillType.ArrowRain:
                return isUsedArrowRain;
            case SkillType.Lighting:
                return isUsedLighting;
            default:
                throw new ArgumentException("Wrong SkillType", "skillType");
        }
    }
    public bool CanUseSkill(SkillType skillType)
    {
        switch (skillType)
        {
            case SkillType.NULL:
                throw new ArgumentNullException("skillType");
            case SkillType.FireBall:
                return !isUsedFireBall && !(FireBallCount <= 0);
            case SkillType.ArrowRain:
                return !isUsedArrowRain && !(ArrowRainCount <= 0);
            case SkillType.Lighting:
                return !isUsedLighting && !(LightingCount <= 0);
            default:
                throw new ArgumentException("Wrong SkillType", "skillType");
        }
    }




    public void Initialize()
    {
        m_Gold = 100;
        m_Energy = 0;
        m_isPlaying = false;

        FireBallCount = 0;
        ArrowRainCount = 0;
        LightingCount = 0;

        isUsedFireBall = false;
        isUsedArrowRain = false;
        isUsedLighting = false;
    }

}
