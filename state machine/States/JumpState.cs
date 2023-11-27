using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : CharacterState
{
    bool canGoUp = true;

    public JumpState(CharacterBehaviour _character, CharacterStateMachine _characterStateMachine) : base(_character, _characterStateMachine)
    {
        character = _character;
        characterStateMachine = _characterStateMachine;
    }
    public override void OnEnter()
    {
        Debug.Log("JUMPED");
        jumpBuffer = false;
        character.characterRigidbody.gravityScale = character.minGravity;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        character.isCoyoteTimeOn = false;
        FlipCharacter();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (inputManager.jumpAction.IsPressed() && fixedTime < character.jumpTime && canGoUp)
            character.characterRigidbody.velocity = new Vector2(character.characterRigidbody.velocity.x, character.jumpMultiplier * Time.deltaTime);
        else canGoUp = false;

        if (character.characterRigidbody.velocity.y <= 0.1f)
            characterStateMachine.SetNextState(new FallState(character, characterStateMachine));

        MoveHorizontally();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
