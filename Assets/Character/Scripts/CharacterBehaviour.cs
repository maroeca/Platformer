using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private Animator animator;
    private PlayerStateMachine stateMachine;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
        stateMachine = new PlayerStateMachine();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Define o estado inicial como Idle
        stateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    public void ChangeState(PlayerState newState)
    {
        stateMachine.ChangeState(newState);
    }

    public bool IsMoving()
    {
        return characterMovement.IsMoving;
    }

    public bool IsRunning()
    {
        return characterMovement.IsRunning;
    }

    public void Flip(float direction)
    {
        spriteRenderer.flipX = direction < 0;
    }

    public void ApplyJump()
    {
        characterMovement.ApplyJump();
    }

    public bool IsGrounded()
    {
        return characterMovement.IsGrounded;
    }

    public float GetMovementDirection()
    {
        return characterMovement.GetDirection();
    }
}
