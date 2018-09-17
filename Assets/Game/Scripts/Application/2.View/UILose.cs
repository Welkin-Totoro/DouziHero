using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILose : View
{
    public override string Name
    { get { return Consts.V_Lose; } }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnBack()
    {
        Game.Instance.LoadScene(1);
    }

    public override void HandleEvent(string eventName, object data)
    {
    }
}
