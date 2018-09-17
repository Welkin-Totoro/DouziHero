using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ReusableObject : MonoBehaviour, IReusable
{

    public abstract void OnSpawn();

    public abstract void OnUnspawn();
}

