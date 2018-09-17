using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class test : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //agent.SetDestination(target.position);
    }

    public void Move(Vector2 delPos)
    {
        Debug.Log(delPos);
        transform.position += new Vector3(delPos.x, delPos.y, 0);
    }
    public void WhatDoYouTouch(Gesture ges)
    {
        if (ges.pickedObject != null)
        {
            Debug.Log("picked up " + ges.pickedObject);
        }
        else
        {
            Debug.Log("picked up nothing");
        }
    }
}
