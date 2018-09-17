using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMCtrl : MonoBehaviour
{
    public Leader[] leaders;

    private void Update()
    {
        if (TestWarning())
        {
            Game.Instance.Sound.PlayBGM("Battle");
        }
        else
        {
            Game.Instance.Sound.PlayBGM("Game");
        }
    }

    private bool TestWarning()
    {
        foreach (Leader leader in leaders)
        {
            if (leader.isWarning)
                return true;
        }
        return false;
    }
}
