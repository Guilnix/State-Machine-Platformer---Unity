using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager inputManagerInstance;

    public InputAction jumpAction;
    public InputAction attackAction;
    public InputAction moveAction;
    public InputAction pauseAction;

    private PlayerInput playerInput;

    private void Awake()
    {
        if (inputManagerInstance == null)
            inputManagerInstance = this;

        playerInput = this.GetComponent<PlayerInput>();

        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
        moveAction = playerInput.actions["Move"];
        pauseAction = playerInput.actions["Pause"];

    }
}
