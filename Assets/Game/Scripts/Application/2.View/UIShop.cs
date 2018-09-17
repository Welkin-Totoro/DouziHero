using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : View
{
    private GameModel gm;

    public Text txtGold;
    public Text txtFireBallCount;
    public Text txtArrowRainCount;
    public Text txtLightingCount;

    public override string Name
    { get { return Consts.V_Shop; } }

    private void Start()
    {
        gm = MVC.GetModel<GameModel>();
        txtGold.text = "拥有金币：" + gm.Gold;
    }

    public override void HandleEvent(string eventName, object data)
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnBuyFireBall()
    {
        if (Game.Instance.StaticData.GetSkillInfo(SkillType.FireBall).Cost > gm.Gold)
            return;

        gm.Gold -= Game.Instance.StaticData.GetSkillInfo(SkillType.FireBall).Cost;
        gm.FireBallCount++;

        txtGold.text = "拥有金币：" + gm.Gold;
        txtFireBallCount.text = "已拥有：" + gm.FireBallCount;
    }
    public void OnBuyArrowRain()
    {
        if (Game.Instance.StaticData.GetSkillInfo(SkillType.ArrowRain).Cost > gm.Gold)
            return;

        gm.Gold -= Game.Instance.StaticData.GetSkillInfo(SkillType.ArrowRain).Cost;
        gm.ArrowRainCount++;

        txtGold.text = "拥有金币：" + gm.Gold;
        txtArrowRainCount.text = "已拥有：" + gm.ArrowRainCount;
    }
    public void OnBuyLighting()
    {
        if (Game.Instance.StaticData.GetSkillInfo(SkillType.Lighting).Cost > gm.Gold)
            return;

        gm.Gold -= Game.Instance.StaticData.GetSkillInfo(SkillType.Lighting).Cost;
        gm.LightingCount++;

        txtGold.text = "拥有金币：" + gm.Gold;
        txtLightingCount.text = "已拥有：" + gm.LightingCount;
    }
    public void OnBack()
    {
        Hide();
    }
}
