using UnityEngine;

/// <summary>
/// Controla o movimento do personagem
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float initialJumpForce = 5f;
    public float maxJumpForce = 10f;
    public float maxHoldTime = 0.5f;

    private float jumpHoldTime;
    private bool isJumping;

    [Header("Coyote Time Settings")]
    [SerializeField] private float coyoteTimeDuration = 0.5f; // Dura��o do coyote time
    private float coyoteTimeCounter; // Temporizador de coyote time



    [SerializeField] private float groundCheckDistance = 0.7f; // Dist�ncia do raycast para verificar o ch�o
    [SerializeField] private LayerMask groundLayer; // Camada que representa o ch�o e o de morte


    [SerializeField]private float acceleration = 1f;
    public float Acceleration
    {
        set {            
            acceleration = value;
            animator.SetFloat("Speed", acceleration); //seta o parametro Speed de acordo com a acelera��o para aumentar a velocidade da anima��o de corrida
        }
        get { return acceleration; }
    }

    [SerializeField] private float maxAcceleration = 5f;
    private bool isGrounded = true;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set
        {
            isGrounded = value;
            if (!isGrounded)
            {
                coyoteTimeCounter = coyoteTimeDuration; //inicia o coyote timer quando sair do ch�o
            }
        }
    }

    private float direction;
    public float Direction
    {
        get { return direction; }
        set 
        { 
            direction = value;
            IsMoving = direction != 0;
        }
    }

    private bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    public void BeginJump()
    {
        Debug.Log(coyoteTimeCounter);
        if (isGrounded || coyoteTimeCounter > 0)
        {
            isJumping = true;
            jumpHoldTime = 0f;
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);

            // Reseta o coyote time ap�s pular
            coyoteTimeCounter = 0f;
        }
    }

    public void StopJump()
    {
        isJumping = false;
    }

    private void Update()
    {
        CheckGrounded();

        if (!isGrounded)
        {
            // Reduz o contador de coyote time
            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
                //Debug.Log(coyoteTimeCounter);
            }
        }

        if (isJumping && jumpHoldTime < maxHoldTime)
        {
            jumpHoldTime += Time.deltaTime;
            float currentJumpForce = Mathf.Lerp(initialJumpForce, maxJumpForce, jumpHoldTime / maxHoldTime);
            rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
        }
    }


    private void CheckGrounded()
    {
        // Define a origem do raycast ligeiramente abaixo do centro do personagem
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.1f); // Ajuste conforme necess�rio
        Vector2 direction = Vector2.down;

        // Realiza o raycast abaixo do personagem para verificar o ch�o
        isGrounded = Physics2D.Raycast(origin, direction, groundCheckDistance, groundLayer);

        // Desenha a linha do raycast na Scene View para depura��o
        Color lineColor = isGrounded ? Color.green : Color.red; // Verde se estiver no ch�o, vermelho se n�o estiver
        Debug.DrawLine(origin, origin + direction * groundCheckDistance, lineColor);

        // (Opcional) Adiciona offsets laterais para bordas
        Vector2 leftOrigin = origin + Vector2.left * 0.1f;
        Vector2 rightOrigin = origin + Vector2.right * 0.1f;

        bool isGroundedLeft = Physics2D.Raycast(leftOrigin, direction, groundCheckDistance, groundLayer);
        bool isGroundedRight = Physics2D.Raycast(rightOrigin, direction, groundCheckDistance, groundLayer);

        // Considera o personagem no ch�o se qualquer um dos raycasts detectar o ch�o
        IsGrounded = isGrounded || isGroundedLeft || isGroundedRight;

        // Desenha os raycasts laterais para depura��o
        Debug.DrawLine(leftOrigin, leftOrigin + direction * groundCheckDistance, lineColor);
        Debug.DrawLine(rightOrigin, rightOrigin + direction * groundCheckDistance, lineColor);
    }


    public void Move(float direction)
    {
        Direction = direction;
        var finalMoveSpeed = moveSpeed * Acceleration;
        transform.position += new Vector3(Direction * finalMoveSpeed * Time.deltaTime, 0);        
    }


    public void IncreaseAccel()
    {
        //Bloqueia a fun��o de correr se estiver fora do ch�o
        if (!isGrounded) return;
        
        if (acceleration < maxAcceleration)
        {
            Acceleration += 0.02f;
        }
    }

    public void DecreaseAccel()
    {
        //Mant�m o impulso da corrida durante o pulo
        if (!isGrounded) return;

        if (acceleration > 1f)
        {
            Acceleration -= 0.01f;
        }
        else
        {
            Acceleration = 1f;
        }
    }
    

    public float GetDirection()
    {
        return Direction;
    }

    public float GetAcceleration()
    {
        return Acceleration;
    }
}
