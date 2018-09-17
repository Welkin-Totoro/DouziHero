using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnergy : View
{
    private GameModel gm;
    public Image img;
    public Text text;

    public override string Name
    {
        get
        {
            return Consts.V_Energy;
        }
    }

    private void Start()
    {
        gm = MVC.GetModel<GameModel>();
        StartCoroutine(EnergyRecovery());
    }
    private void Update()
    {

    }

    public void UpdateEnergyShow(int enermy)
    {
        img.transform.localPosition = new Vector3(-365 + (float)gm.Energy / 100 * (-15.2f - (-365)), -1.09f, 0);
        text.text = enermy.ToString();
    }

    IEnumerator EnergyRecovery()
    {
        while (true)
        {
            gm.ReceiveEnergy(1);
            UpdateEnergyShow(gm.Energy);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_SpawnSoldier);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_SpawnSoldier:
                SpawnSoldierArgs e1 = data as SpawnSoldierArgs;
                if (e1.camp == Camp.YELLOW)
                {
                    gm.ConsumeEnergy(Game.Instance.StaticData.GetSoldierInfo(e1.arm).Cost);
                    UpdateEnergyShow(gm.Energy);
                }
                break;
            default:
                break;
        }
    }
}
