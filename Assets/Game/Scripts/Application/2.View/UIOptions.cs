using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptions : View
{
    public Toggle togBGM;
    public Slider sliBGMVol;
    public Slider sliESVol;

    public override string Name
    {
        get{ return Consts.V_Options; }
    }

    public void SetBGMVol()
    {
        if (togBGM.isOn)
        {
            sliBGMVol.interactable = true;
            Game.Instance.Sound.BGMVol = sliBGMVol.value;
        }
        else
        {
            sliBGMVol.interactable = false;
            Game.Instance.Sound.BGMVol = 0;
        }
        Debug.Log(Game.Instance.Sound.BGMVol);
    }
    public void SetESVol()
    {
        Game.Instance.Sound.EffectSoundVol = sliESVol.value;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void HandleEvent(string eventName, object data)
    {
    }
}
