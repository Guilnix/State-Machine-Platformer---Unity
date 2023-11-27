using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : CharacterState
{
    public FallState(CharacterBehaviour _character, CharacterStateMachine _characterStateMachine) : base(_character, _characterStateMachine)
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
        FlipCharacter();
        Debug.Log(character.characterRigidbody.velocity.y);
        if (character.IsGrounded())
        {
            if (jumpBuffer)
            {
                characterStateMachine.SetNextState(new JumpState(character, characterStateMachine));
                return;
            }
            if (moveDirection != 0) 
                characterStateMachine.SetNextState(new RunState(character, characterStateMachine));
            else characterStateMachine.SetNextState(new IdleState(character, characterStateMachine));
        }
        else if(character.isCoyoteTimeOn && jumpBuffer)
            characterStateMachine.SetNextState(new JumpState(character, characterStateMachine));

    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (character.characterRigidbody.gravityScale < character.maxGravity)
            character.characterRigidbody.gravityScale += character.fallMultiplier;

        character.characterRigidbody.velocity = new Vector2(character.characterRigidbody.velocity.x,
                                                            Mathf.Clamp(character.characterRigidbody.velocity.y, -character.maxFallSpeed, 100));
        MoveHorizontally();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
