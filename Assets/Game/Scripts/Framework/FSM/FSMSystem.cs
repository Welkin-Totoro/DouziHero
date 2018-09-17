using UnityEngine;
using System.Collections.Generic;



public class FSMSystem
{
    private List<FSMState> states;


    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    public void AddState(FSMState s)
    {
        if (s == null)
        {
            Debug.LogError("FSM ERROR: Null reference");
        }
        
        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }

        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: Impossible to add state " + s.ID.ToString() +
                               " because state has already been added");
                return;
            }
        }
        states.Add(s);
    }


    public void DeleteState(StateID id)
    {
        if (id == StateID.NullState)
        {
            Debug.LogError("FSM ERROR: NullState");
            return;
        }
        
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: Impossible to delete state " + id.ToString() +
                       ". It was not on the list of states");
    }


    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: NullTransition is not allowed for a real transition");
            return;
        }

        StateID id = currentState.GetOutputState(trans);
        if (id == StateID.NullState)
        {
            Debug.LogError("FSM ERROR: State " + currentStateID.ToString() + " does not have a target state " + " for transition " + trans.ToString());
            return;
        }
        
        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                currentState.DoBeforeLeaving();

                currentState = state;

                currentState.DoBeforeEntering();
                break;
            }
        }

    } 

}