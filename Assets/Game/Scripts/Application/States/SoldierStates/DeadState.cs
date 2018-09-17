using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class DeadState : FSMState
{
    private GameObject soldier;

    public DeadState(GameObject soldier)
    {
        stateID = StateID.Dead;
        this.soldier = soldier;
    }

    public override void Reason(GameObject target)
    {
        if (!soldier.GetComponent<Soldier>().IsDead)
        {
            soldier.GetComponent<SoldierFSM>().SetTransition(Transition.HpRemain);
        }
    }
    public override void Act(GameObject target)
    {
    }

    public override void DoBeforeEntering()
    {
        soldier.GetComponent<Animator>().SetBool("IsDead", true);
    }
    public override void DoBeforeLeaving()
    {
    }
}
