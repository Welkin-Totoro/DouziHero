using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LookupState : FSMState
{
    private GameObject soldier;

    public LookupState(GameObject soldier)
    {
        stateID = StateID.LookupEnemy;
        this.soldier = soldier;
    }

    public override void Reason(GameObject target = null)
    {
        if (target != null && !target.GetComponent<Role>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.FoundEnemy);
        }
        else if (soldier.GetComponent<Soldier>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.HpEmpty);
        }
    }

    public override void Act(GameObject target = null)
    {
        //Lookup Enemy
        GameObject[] roles = null;
        if (soldier.tag == Tags.YELLOW)
            roles = GameObject.FindGameObjectsWithTag(Tags.GREEN);
        else if (soldier.tag == Tags.GREEN)
            roles = GameObject.FindGameObjectsWithTag(Tags.YELLOW);

        if (roles == null) return;

        float minDis = Mathf.Infinity;
        foreach (GameObject role in roles)
        {
            //Debug.Log(role);
            Role r = role.GetComponent<Role>();
            float m = Vector3.Distance(soldier.transform.position, r.transform.position);
            if (!r.IsDead && m < minDis)
            {
                minDis = m;
                soldier.GetComponent<SoldierFSM>().target = r.gameObject;
            }
        }
    }

    public override void DoBeforeEntering()
    {
        soldier.GetComponent<Soldier>().PerformIdlle(0);
    }
    public override void DoBeforeLeaving()
    {
    }
}