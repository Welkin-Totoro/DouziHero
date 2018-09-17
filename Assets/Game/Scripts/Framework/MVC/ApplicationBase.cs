using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class ApplicationBase<T> : Singleton<T>
    where T : MonoBehaviour
{
    //Register Controller
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}