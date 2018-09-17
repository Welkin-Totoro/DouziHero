using System;
using System.Collections.Generic;
using System.Text;

public abstract class Model
{
    public abstract string Name { get; }

    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}