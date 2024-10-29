using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterBehaviour characterBehaviour;
    private CharacterInput inputActions;
    private Vector2 moveInput;
    private bool jumpInput;
    private bool isHoldingAction = false;

    private void Awake()
    {
        inputActions = new CharacterInput();

        inputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Gameplay.Jump.performed += ctx => jumpInput = ctx.ReadValueAsButton();
        inputActions.Gameplay.Jump.canceled += ctx => jumpInput = false;


        inputActions.Gameplay.Run.performed += ctx => isHoldingAction = ctx.ReadValueAsButton() ;
        inputActions.Gameplay.Run.canceled += ctx => isHoldingAction = false;
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }

    public void OnHoldingAction(InputAction.CallbackContext context)
    {
        Debug.Log("Chamou");
        if (context.phase == InputActionPhase.Performed)
        {
            isHoldingAction = true; // Inicia o hold quando o botão é pressionado
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isHoldingAction = false; // Cancela o hold quando o botão é solto
        }
    }

    public void RunPerformed()
    {
        Debug.Log("Performing");
        isHoldingAction = true;
    }

    public void RunCanceled()
    {
        Debug.Log("Canceling");
        isHoldingAction = false;
    }

    public void Run()
    {
        characterMovement.IncreaseAccel();
    }

    public void Deaccelerate()
    {
        characterMovement.DecreaseAccel();
    }

    private void Update()
    {
        characterMovement.Move(moveInput.x);

        if (jumpInput)
        {
            characterBehaviour.ApplyJump();
        }

        if (isHoldingAction)
        {
            Debug.Log("Holding");
            Run();
        }
        else
        {
            Deaccelerate();
        }
    }

    private void Running()
    {
        Debug.Log("RUNNING");
        characterMovement.Run(moveInput.x);
    }
}
