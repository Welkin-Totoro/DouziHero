using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWin : View
{
    public override string Name
    { get { return Consts.V_Win; } }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public override void HandleEvent(string eventName, object data)
    {
    }
}
