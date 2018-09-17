using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ForwardState : FSMState
{
    private GameObject soldier;

    public ForwardState(GameObject soldier)
    {
        stateID = StateID.ForwardEnemy;
        this.soldier = soldier;
    }

    public override void Reason(GameObject target)
    {
        if (target == null || target.GetComponent<Role>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.LostEnemy);
        }
        else if (Vector3.Distance(soldier.transform.position, target.transform.position) <= soldier.GetComponent<Soldier>().AttackRange)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.CanAttackEnemy);
        }
        else if (soldier.GetComponent<Soldier>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.HpEmpty);
        }
    }
    public override void Act(GameObject target)
    {
        soldier.GetComponent<Soldier>().MoveToTarget(target);
    }

    public override void DoBeforeEntering()
    {
    }
    public override void DoBeforeLeaving()
    {
    }
}
