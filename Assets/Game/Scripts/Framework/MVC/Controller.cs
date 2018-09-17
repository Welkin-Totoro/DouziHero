using System;
using System.Collections.Generic;
using System.Text;

public abstract class Controller
{
    //Get Model
    protected T GetModel<T>()
        where T : Model
    {
        return MVC.GetModel<T>() as T;
    }
    //Get View
    protected T GetView<T>()
    where T : View
    {
        return MVC.GetView<T>() as T;
    }

    //Register
    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }
    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    //Handle Events
    public abstract void Execute(object data);
}