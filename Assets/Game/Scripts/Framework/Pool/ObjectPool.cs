using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    public string ResourceDir = "";

    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();


    //Get Object
    public GameObject Spawn(string name)
    {
        if (!m_pools.ContainsKey(name))
            RegisterNew(name);
        SubPool pool = m_pools[name];
        return pool.Spawn();
    }

    //Recycle Object
    public void Unspawn(GameObject go)
    {
        SubPool pool = null;

        foreach (SubPool p in m_pools.Values)
        {
            if (p.Contains(go))
            {
                pool = p;
                break;
            }
        }
        pool.Unspawn(go);
    }

    //Recycle Objects
    public void UnspawnAll()
    {
        foreach (SubPool p in m_pools.Values)
        {
            p.UnspawnAll();
        }
    }


    //Create a new subPool
    private void RegisterNew(string name)
    {
        //path
        string path = "";
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = name;
        }
        else
        {
            path = ResourceDir + "/" + name;
        }

        //prefab
        GameObject prefab = Resources.Load<GameObject>(path);

        //Construct subPool Obj
        SubPool pool = new SubPool(transform,prefab);
        m_pools.Add(pool.Name, pool);
    }
}
