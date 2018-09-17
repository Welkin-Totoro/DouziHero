using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Transition
{
    NullTransition, //NULL

    FoundEnemy,
    LostEnemy,
    CanAttackEnemy,
    HpEmpty,
    HpRemain
}


public enum StateID
{
    NullState, //NULL
    
    LookupEnemy,
    ForwardEnemy,
    AttackEnemy,
    Dead
}


public abstract class FSMState
{
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateID;
    public StateID ID { get { return stateID; } }

    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        if (id == StateID.NullState)
        {
            Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
            return;
        }

        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                           "Impossible to assign to another state");
            return;
        }

        map.Add(trans, id);
    }
    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                       " was not on the state's transition list");
    }
    
    public StateID GetOutputState(Transition trans)
    {
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullState;
    }

    
    public virtual void DoBeforeEntering() { }
    public virtual void DoBeforeLeaving() { }

    
    public abstract void Reason(GameObject target = null);
    public abstract void Act(GameObject target = null);

}
