using System;
using System.Collections.Generic;
using System.Text;

public static class MVC
{
    //Store MVC
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();//Name--Model
    public static Dictionary<string, View> Views = new Dictionary<string, View>();//Name--View
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();//EventName--ControllerType

    //Register
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        if (Views.ContainsKey(view.Name))
            Views.Remove(view.Name);

        view.RegisterEvents();

        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName, Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //Get
    public static T GetModel<T>()
        where T : Model
    {
        foreach (Model m in Models.Values)
        {
            if (m is T)
            {
                return m as T;
            }
        }
        return null;
    }

    public static T GetView<T>()
    where T : View
    {
        foreach (View v in Views.Values)
        {
            if (v is T)
            {
                return v as T;
            }
        }
        return null;
    }

    //Send Event
    public static void SendEvent(string eventName, object data = null)
    {
        //Controller Response Event
        if (CommandMap.ContainsKey(eventName))
        {
            Type t = CommandMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;

            //Controller Active
            c.Execute(data);
        }


        //View Response Event
        foreach (View v in Views.Values)
        {
            if (v.AttentionEvents.Contains(eventName))
            {
                //View Response
                v.HandleEvent(eventName, data);
            }
        }
    }
}