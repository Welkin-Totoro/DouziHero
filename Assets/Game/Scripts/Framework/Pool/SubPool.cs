using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    Transform m_parent;

    //Prefab
    private GameObject m_prefab;

    //Group
    private List<GameObject> m_objects = new List<GameObject>();

    //Symbol
    public string Name
    {
        get { return m_prefab.name; }
    }

    //Construct
    public SubPool(Transform parent,GameObject prefab)
    {
        m_parent = parent;
        m_prefab = prefab;
    }

    //Get Object
    public GameObject Spawn()
    {
        GameObject go = null;

        foreach (GameObject obj in m_objects)
        {
            if (!obj.activeSelf)
            {
                go = obj;
                break;
            }
        }

        if (go == null)
        {
            go = GameObject.Instantiate<GameObject>(m_prefab,m_parent);
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
        return go;
    }

    //Recycle Object
    public void Unspawn(GameObject go)
    {
        if (m_objects.Contains(go))
        {
            go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //Recycle All Objects
    public void UnspawnAll()
    {
        foreach (GameObject go in m_objects)
        {
            if (go.activeSelf)
            {
                Unspawn(go);
            }
        }
    }

    //Find whether containing
    public bool Contains(GameObject go)
    {
        return m_objects.Contains(go);
    }

}
