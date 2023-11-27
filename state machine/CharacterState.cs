using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    protected CharacterBehaviour character;
    protected CharacterStateMachine characterStateMachine;
    protected InputManager inputManager;

    protected float time { get; set; }
    protected float fixedTime { get; set; }

    protected bool jumpBuffer;
    protected bool attackBuffer;
    protected float moveDirection;

    private float jumpBufferTime;
    private float attackBufferTime;

    public CharacterState(CharacterBehaviour _character, CharacterStateMachine _characterStateMachine)
    {
        character = _character;
        characterStateMachine = _characterStateMachine;
        inputManager = InputManager.inputManagerInstance;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {
        time += Time.deltaTime;

        moveDirection = inputManager.moveAction.ReadValue<Vector2>().x;

        if (inputManager.jumpAction.WasPressedThisFrame())
        {
            jumpBuffer = true;
            jumpBufferTime = time + 1f;
        }
        if (time > jumpBufferTime)
            jumpBuffer = false;

        if (inputManager.attackAction.WasPressedThisFrame())
        {
            attackBuffer = true;
            attackBufferTime = time + 1f;
        }
        if (time > attackBufferTime)
            attackBuffer = false;
    }

    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }

    public virtual void OnExit()
    {

    }

    protected void MoveHorizontally()
    {
        float targetSpeed = moveDirection * character.moveSpeed * Time.deltaTime;
        float accelerationRate = Mathf.Sign(character.characterRigidbody.velocity.x) != Mathf.Sign(targetSpeed) ? character.deacceleration : character.acceleration;
        float movementForce = Mathf.MoveTowards(character.characterRigidbody.velocity.x, targetSpeed, accelerationRate * Time.deltaTime);
        character.characterRigidbody.velocity = new Vector2(movementForce, character.characterRigidbody.velocity.y);
    }

    protected void FlipCharacter()
    {
        if((int)moveDirection != 0)
            character.transform.localScale = new Vector3((int)moveDirection, character.transform.localScale.y, character.transform.localScale.z);
    }
    #region Métodos passados da Unity

    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    protected T GetComponent<T>() where T : Component { return characterStateMachine.GetComponent<T>(); }

    protected Component GetComponent(System.Type type) { return characterStateMachine.GetComponent(type); }

    protected Component GetComponent(string type) { return characterStateMachine.GetComponent(type); }

    #endregion
}
