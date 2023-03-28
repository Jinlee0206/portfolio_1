using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pStateMachine
{
    public pState CurrentState { get; protected set; }

    public void Initialize(pState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();

        Debug.Log(startingState);

    }

    public void ChangeState(pState newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();

        Debug.Log(newState);
    }


}