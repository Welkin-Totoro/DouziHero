using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AttackState : FSMState
{
    private GameObject soldier;

    public AttackState(GameObject soldier)
    {
        stateID = StateID.AttackEnemy;
        this.soldier = soldier;
    }

    public override void Reason(GameObject target)
    {
        if (target == null || target.GetComponent<Role>().IsDead || Vector3.Distance(soldier.transform.position, target.transform.position) > soldier.GetComponent<Soldier>().AttackRange)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.LostEnemy);
        }
        else if (soldier.GetComponent<Soldier>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.HpEmpty);
        }
    }
    public override void Act(GameObject target)
    {
        soldier.GetComponent<Soldier>().Damage(target.GetComponent<Role>());
    }

    public override void DoBeforeEntering()
    {
        soldier.GetComponent<Soldier>().PerformIdlle(0.8f);
    }
    public override void DoBeforeLeaving()
    {
    }
}