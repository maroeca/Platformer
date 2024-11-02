using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterBehaviour characterBehaviour;
    private CharacterInput inputActions;
    private Vector2 moveInput; //Recebe os valores do movimento horizontal
    private bool jumpInput; //Recebe o input do pulo em forma de bot�o
    private bool isHoldingRun = false; // Controla se o bot�o de corrida est� pressionado

    private void Awake()
    {
        inputActions = new CharacterInput();

        inputActions.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Gameplay.Jump.started += ctx => StartJump();
        inputActions.Gameplay.Jump.canceled += ctx => EndJump();


        inputActions.Gameplay.Run.performed += ctx => isHoldingRun = ctx.ReadValueAsButton() ;
        inputActions.Gameplay.Run.canceled += ctx => isHoldingRun = false;
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

    private void StartJump()
    {
        jumpInput = true;
        characterBehaviour.StartJump();
    }

    private void EndJump()
    {
        jumpInput = false;
        characterBehaviour.EndJump();
    }

    //Mudan�a para fixedUpdate para evitar que o personagem flicasse
    private void FixedUpdate()
    {
        characterMovement.Move(moveInput.x);

        

        //Acelera o personagem enquanto o bot�o de corrida est� acionado
        //e desacelera quando o bot�o � solto
        if (isHoldingRun)
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
