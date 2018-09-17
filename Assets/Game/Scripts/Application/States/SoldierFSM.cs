using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(Soldier))]
public class SoldierFSM : MonoBehaviour
{
    private FSMSystem fsm;

    [HideInInspector]
    public GameObject target = null;

    public void Start()
    {
        MakeFSM(gameObject.GetComponent<Soldier>());
    }
    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(target);
        fsm.CurrentState.Act(target);
        //Debug.Log(name +" : " + fsm.CurrentState);
    }

    private void MakeFSM(Soldier soldier)
    {
        LookupState lookupState = new LookupState(soldier.gameObject);
        lookupState.AddTransition(Transition.FoundEnemy, StateID.ForwardEnemy);
        lookupState.AddTransition(Transition.HpEmpty, StateID.Dead);

        ForwardState forwardState = new ForwardState(soldier.gameObject);
        forwardState.AddTransition(Transition.LostEnemy, StateID.LookupEnemy);
        forwardState.AddTransition(Transition.CanAttackEnemy, StateID.AttackEnemy);
        forwardState.AddTransition(Transition.HpEmpty, StateID.Dead);

        AttackState attackState = new AttackState(soldier.gameObject);
        attackState.AddTransition(Transition.LostEnemy, StateID.LookupEnemy);
        attackState.AddTransition(Transition.HpEmpty, StateID.Dead);

        DeadState deadState = new DeadState(soldier.gameObject);
        deadState.AddTransition(Transition.HpRemain, StateID.LookupEnemy);

        fsm = new FSMSystem();
        fsm.AddState(lookupState);
        fsm.AddState(forwardState);
        fsm.AddState(attackState);
        fsm.AddState(deadState);
    }
    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

}