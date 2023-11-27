using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public CharacterState currentState { get; private set; }
    private CharacterState nextState;

    public void InitializeMachine(CharacterState _initialState)
    {
        currentState = _initialState;
        currentState.OnEnter();
    }

    private void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }
        if (currentState != null)
            currentState.OnUpdate();
    }
    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.OnFixedUpdate();
    }

    private void SetState(CharacterState _nextState)
    {
            if (currentState != null)
                currentState.OnExit();

            currentState = _nextState;
            currentState.OnEnter();    
    }

    public void SetNextState(CharacterState _newState)
    {
        if (_newState != null)
            nextState = _newState;
    }
}
