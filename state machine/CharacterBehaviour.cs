using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("J U M P")]
    public float jumpMultiplier;
    public float jumpTime;

    public float coyoteTime;
    private float currentCoyoteTime;
    [HideInInspector] public bool isCoyoteTimeOn;

    [Header("M O V E M E N T")]
    public float moveSpeed;
    public float acceleration;
    public float deacceleration;

    [Header("P H Y S I C S")]
    public float minGravity;
    public float maxGravity;
    public float fallMultiplier;
    public float maxFallSpeed;
    [SerializeField] private Bounds groundCheckBounds;
    [SerializeField] private LayerMask groundLayer;


    [HideInInspector] public Rigidbody2D characterRigidbody;
    [HideInInspector] public Animator characterAnimator;
    private CharacterStateMachine characterStateMachine;

    private void Awake()
    {
        characterStateMachine = this.GetComponent<CharacterStateMachine>();
        characterRigidbody = this.GetComponent<Rigidbody2D>();

        characterStateMachine.InitializeMachine(new FallState(this, characterStateMachine));
    }

    private void Update()
    {
        if (IsGrounded())
        {
            isCoyoteTimeOn = true;
            currentCoyoteTime = 0f;
        }
        else if (currentCoyoteTime < coyoteTime)
            currentCoyoteTime += Time.deltaTime;
        else
            isCoyoteTimeOn = false;
    }
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(transform.position + groundCheckBounds.center, groundCheckBounds.size, 0, groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + groundCheckBounds.center, groundCheckBounds.size);
    }
}
