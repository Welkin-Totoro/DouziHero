using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillBtn : View
{

    public Button btnFireBall;
    public Button btnArrowRain;
    public Button btnLighting;
    public Button btnCancel;
    public Text txtCurrentChoice;


    public override string Name
    {
        get
        { return Consts.V_SkillBtn; }
    }

    private void Update()
    {
        SetBtnsState();
    }

    private void SetBtnsState()
    {
        GameModel gm = MVC.GetModel<GameModel>();

        btnFireBall.interactable = gm.CanUseSkill(SkillType.FireBall);
        btnArrowRain.interactable = gm.CanUseSkill(SkillType.ArrowRain);
        btnLighting.interactable = gm.CanUseSkill(SkillType.Lighting);
        btnCancel.interactable = btnFireBall.interactable || btnArrowRain.interactable || btnLighting.interactable;
    }

    public void OnFireBallClick()
    {
        //if (Game.Instance.StaticData.GetSoldierInfo(Arm.Swordsman).Cost > MVC.GetModel<GameModel>().Energy)
        //    return;

        //SpawnSoldierArgs e1 = new SpawnSoldierArgs() { arm = Arm.Swordsman, camp = Camp.YELLOW, pos = new UnityEngine.Vector3(35, 5, 0) };
        //SendEvent(Consts.E_SpawnSoldier, e1);
        //SpawnSoldierArgs e2 = new SpawnSoldierArgs() { arm = Arm.Swordsman, camp = Camp.GREEN, pos = new UnityEngine.Vector3(220, 28.5f, 0) };
        //SendEvent(Consts.E_SpawnSoldier, e2);
        MVC.GetModel<GameModel>().SetCurrentSkillType(SkillType.FireBall);
        txtCurrentChoice.text = "当前选择：" + "火球";
    }
    public void OnArrowRainClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSkillType(SkillType.ArrowRain);
        txtCurrentChoice.text = "当前选择：" + "箭雨";

    }
    public void OnLightingClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSkillType(SkillType.Lighting);
        txtCurrentChoice.text = "当前选择：" + "雷电";

    }
    public void OnCancel()
    {
        MVC.GetModel<GameModel>().SetCurrentSkillType(SkillType.NULL);
        txtCurrentChoice.text = "当前选择：" + "无";

    }

    public void OnShowShop()
    {
        SendEvent(Consts.E_ShowSHop);
        //Tower[] towers = GameObject.FindObjectsOfType<Tower>();
        //foreach (Tower tower in towers)
        //{
        //    if (tower.towerType == TowerType.big && tower.camp == Camp.YELLOW)
        //        tower.GetDamage(100);
        //}
    }


    public override void RegisterEvents()
    {
    }
    public override void HandleEvent(string eventName, object data)
    {
    }


}
