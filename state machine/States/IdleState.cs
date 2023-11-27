using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : CharacterState
{
    private float stoppingForce;
    public IdleState(CharacterBehaviour _character, CharacterStateMachine _characterStateMachine) : base(_character, _characterStateMachine)
    {
        character = _character;
        characterStateMachine = _characterStateMachine;
    }
    public override void OnEnter()
    { 
    
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (character.characterRigidbody.velocity.x != 0)
            stoppingForce = Mathf.MoveTowards(character.characterRigidbody.velocity.x, 0, character.deacceleration * Time.deltaTime);        

        if (character.characterRigidbody.velocity.y < -0.01f) 
            characterStateMachine.SetNextState(new FallState(character, characterStateMachine));
        if(moveDirection != 0)
            characterStateMachine.SetNextState(new RunState(character, characterStateMachine));
        if(jumpBuffer)
            characterStateMachine.SetNextState(new JumpState(character, characterStateMachine));

    }

    public override void OnFixedUpdate()
    {
        if (character.characterRigidbody.velocity.x != 0)
            character.characterRigidbody.velocity = new Vector2(stoppingForce, character.characterRigidbody.velocity.y);

        base.OnFixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
