using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInsBtn : View
{

    public Button btnSwordsman;
    public Button btnKnight;
    public Button btnLanceKnight;
    public Button btnHunter;
    public Button btnHorseman;
    public Button btnCancel;
    public Text txtCurrentChoice;


    public override string Name
    {
        get
        { return Consts.V_InsBtn; }
    }

    private void Update()
    {
        SetBtnsState();
    }

    private void SetBtnsState()
    {
        GameModel gm = MVC.GetModel<GameModel>();

        btnSwordsman.interactable = gm.CanInsSoldier(Arm.Swordsman);
        btnKnight.interactable = gm.CanInsSoldier(Arm.Knight);
        btnLanceKnight.interactable = gm.CanInsSoldier(Arm.LanceKnight);
        btnHunter.interactable = gm.CanInsSoldier(Arm.Hunter);
        btnHorseman.interactable = gm.CanInsSoldier(Arm.Horseman);
        btnCancel.interactable = btnSwordsman.interactable || btnKnight.interactable || btnLanceKnight.interactable || btnHunter.interactable || btnHorseman.interactable;
    }

    public void OnSwordsmanClick()
    {
        //if (Game.Instance.StaticData.GetSoldierInfo(Arm.Swordsman).Cost > MVC.GetModel<GameModel>().Energy)
        //    return;

        //SpawnSoldierArgs e1 = new SpawnSoldierArgs() { arm = Arm.Swordsman, camp = Camp.YELLOW, pos = new UnityEngine.Vector3(35, 5, 0) };
        //SendEvent(Consts.E_SpawnSoldier, e1);
        //SpawnSoldierArgs e2 = new SpawnSoldierArgs() { arm = Arm.Swordsman, camp = Camp.GREEN, pos = new UnityEngine.Vector3(220, 28.5f, 0) };
        //SendEvent(Consts.E_SpawnSoldier, e2);
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.Swordsman);
        txtCurrentChoice.text = "当前选择：" + "剑客";
    }
    public void OnKnightClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.Knight);
        txtCurrentChoice.text = "当前选择：" + "盾兵";

    }
    public void OnLanceKnightClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.LanceKnight);
        txtCurrentChoice.text = "当前选择：" + "长矛兵";

    }
    public void OnHunterClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.Hunter);
        txtCurrentChoice.text = "当前选择：" + "弓箭手";

    }
    public void OnHorsemanClick()
    {
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.Horseman);
        txtCurrentChoice.text = "当前选择：" + "骑兵";

    }
    public void OnCancel()
    {
        MVC.GetModel<GameModel>().SetCurrentSpawnType(Arm.NULL);
        txtCurrentChoice.text = "当前选择：" + "无";

    }

    public override void RegisterEvents()
    {
    }
    public override void HandleEvent(string eventName, object data)
    {
    }


}
