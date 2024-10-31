using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterBehaviour characterBehaviour;
    private CharacterInput inputActions;
    private Vector2 moveInput; //Recebe os valores do movimento horizontal
    private bool jumpInput; //Recebe o input do pulo em forma de botão
    private bool isHoldingAction = false; // Controla se o botão de corrida está pressionado

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

    public void Accelerate()
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

        //Acelera o personagem enquanto o botão de corrida está acionado
        //e desacelera quando o botão é solto
        if (isHoldingAction)
        {
            Debug.Log("Holding");
            Accelerate();
        }
        else
        {
            Deaccelerate();
        }
    }
}
