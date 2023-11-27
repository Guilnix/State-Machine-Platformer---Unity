using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : CharacterState
{
    public RunState(CharacterBehaviour _character, CharacterStateMachine _characterStateMachine) : base(_character, _characterStateMachine)
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

        if (character.characterRigidbody.velocity.y < -0.01f)
            characterStateMachine.SetNextState(new FallState(character, characterStateMachine));
        if(moveDirection == 0)
            characterStateMachine.SetNextState(new IdleState(character, characterStateMachine));
        if (jumpBuffer)
            characterStateMachine.SetNextState(new JumpState(character, characterStateMachine));
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        MoveHorizontally();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
