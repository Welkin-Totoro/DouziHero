using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpShow : MonoBehaviour
{

    private void Start()
    {
        transform.parent.GetComponent<Tower>().HpChanged += HpShow_HpChanged;
    }
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void HpShow_HpChanged(int hp, int maxHp)
    {
        transform.localScale = new Vector3(0.8f * hp / maxHp, transform.localScale.y, transform.localScale.z);
        //Debug.Log(hp + " / " + maxHp);
    }
}
