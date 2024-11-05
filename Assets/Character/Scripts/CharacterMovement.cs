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
    [SerializeField] private float coyoteTimeDuration = 0.5f; // Duração do coyote time
    private float coyoteTimeCounter; // Temporizador de coyote time



    [SerializeField] private float groundCheckDistance = 0.7f; // Distância do raycast para verificar o chão
    [SerializeField] private LayerMask groundLayer; // Camada que representa o chão e o de morte


    [SerializeField] private float acceleration = 1f;
    public float Acceleration
    {
        set
        {
            acceleration = value;
            animator.SetFloat("Speed", acceleration); //seta o parametro Speed de acordo com a aceleração para aumentar a velocidade da animação de corrida
        }
        get { return acceleration; }
    }

    [SerializeField] private float maxAcceleration = 5f;
    private bool wasGrounded; // Adiciona uma variável para rastrear o estado anterior

    [SerializeField]private bool isGrounded = true;
    public bool IsGrounded
    {
        get { return isGrounded; }
        private set
        {
            isGrounded = value;
            if (!isGrounded)
            {
                coyoteTimeCounter = coyoteTimeDuration; //inicia o coyote timer quando sair do chão
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
        if (isGrounded || coyoteTimeCounter > 0)
        {
            isJumping = true;
            jumpHoldTime = 0f;
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);

            // Reseta o coyote time após pular
            coyoteTimeCounter = 0f;
        }
    }

    public void StopJump()
    {
        isJumping = false;
    }

    private void FixedUpdate()
    {

        if (!isGrounded && coyoteTimeCounter > 0)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (isJumping && jumpHoldTime < maxHoldTime)
        {
            jumpHoldTime += Time.deltaTime;
            float currentJumpForce = Mathf.Lerp(initialJumpForce, maxJumpForce, jumpHoldTime / maxHoldTime);
            rb.velocity = new Vector2(rb.velocity.x, currentJumpForce);
        }
    }


    //Substituido por onCollision por problemas com o pulo
    private void CheckGrounded()
    {
        // Define a origem do raycast ligeiramente abaixo do centro do personagem
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.1f);
        Vector2 direction = Vector2.down;

        // Realiza o raycast abaixo do personagem para verificar o chão
        isGrounded = Physics2D.Raycast(origin, direction, groundCheckDistance, groundLayer);

        // Desenha a linha do raycast na Scene View para depuração
        Color lineColor = isGrounded ? Color.green : Color.red; // Verde se estiver no chão, vermelho se não estiver
        Debug.DrawLine(origin, origin + direction * groundCheckDistance, lineColor);

        Vector2 leftOrigin = origin + Vector2.left * 0.2f;
        Vector2 rightOrigin = origin + Vector2.right * 0.2f;

        bool isGroundedLeft = Physics2D.Raycast(leftOrigin, direction, groundCheckDistance, groundLayer);
        bool isGroundedRight = Physics2D.Raycast(rightOrigin, direction, groundCheckDistance, groundLayer);

        // Considera o personagem no chão se qualquer um dos raycasts detectar o chão
        IsGrounded = isGrounded || isGroundedLeft || isGroundedRight;

        // Desenha os raycasts laterais para depuração
        Debug.DrawLine(leftOrigin, leftOrigin + direction * groundCheckDistance, lineColor);
        Debug.DrawLine(rightOrigin, rightOrigin + direction * groundCheckDistance, lineColor);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            Debug.Log("##### Touch ground #####");
            ScreenShake.Instance.ShakeCamera();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            coyoteTimeCounter = coyoteTimeDuration;
        }
    }


    public void Move(float direction)
    {
        Direction = direction;
        var finalMoveSpeed = moveSpeed * Acceleration;
        transform.position += new Vector3(Direction * finalMoveSpeed * Time.deltaTime, 0);
    }


    public void IncreaseAccel()
    {
        //Bloqueia a função de correr se estiver fora do chão ou parado
        if (!isGrounded || Direction == Vector2.zero.magnitude) return;

        if (acceleration < maxAcceleration)
        {
            Acceleration += 0.02f;
        }
    }

    public void DecreaseAccel()
    {
        //Mantém o impulso da corrida durante o pulo
        if (!isGrounded) return;

        //Para a aceleração de o player não estiver movendo o direcional
        if (Direction == Vector2.zero.magnitude) { Acceleration = 1; }

        if (acceleration > 1f) { Acceleration -= 0.01f; }
        else { Acceleration = 1f; }
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
