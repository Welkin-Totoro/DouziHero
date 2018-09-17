using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {
        SceneArgs e = data as SceneArgs;

        //Register View
        switch (e.SceneIndex)
        {
            case 0://Init
                break;

            case 1://Menu
                RegisterView(GameObject.FindObjectOfType<UIMenu>());
                break;

            case 2://Game
                GetModel<GameModel>().Initialize();

                RegisterView(GameObject.FindObjectOfType<UIInsBtn>());
                RegisterView(GameObject.FindObjectOfType<UISkillBtn>());
                RegisterView(GameObject.FindObjectOfType<UIEnergy>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIShop").GetComponentInChildren<UIShop>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIWin").GetComponentInChildren<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UILose").GetComponentInChildren<UILose>());

                RegisterView(GameObject.FindObjectOfType<Spawner>());
                break;

            case 3://Complete
                //RegisterView(GameObject.FindObjectOfType<UIComplete>());
                break;
            default:
                break;
        }
    }
}