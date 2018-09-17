using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : View
{
    public override string Name
    {
        get
        {
            return Consts.V_Start;
        }
    }

    public void OnStart()
    {
        Game.Instance.LoadScene(2);
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.E_EnterScene);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs e1 = data as SceneArgs;
                if (e1.SceneIndex == 1)
                    Game.Instance.Sound.PlayBGM("Menu");
                break;
            default:
                break;
        }
    }
}
