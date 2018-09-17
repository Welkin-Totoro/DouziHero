using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public abstract class View : MonoBehaviour
{
    //Symbol
    public abstract string Name { get; }

    //AttentionEvents
    [HideInInspector]
    public List<string> AttentionEvents = new List<string>();

    public virtual void RegisterEvents()
    { }

    //Event Handle
    public abstract void HandleEvent(string eventName, object data);

    //Get Model
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    //Send Event
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}