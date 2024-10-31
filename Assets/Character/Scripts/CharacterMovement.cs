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
    public float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 0.7f; // Dist�ncia do raycast para verificar o ch�o
    [SerializeField] private LayerMask groundLayer; // Camada que representa o ch�o


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

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        // Realiza o raycast abaixo do personagem para verificar o ch�o
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.down;

        isGrounded = Physics2D.Raycast(origin, direction, groundCheckDistance, groundLayer);

        // Desenha a linha do raycast na Scene View para depura��o
        Color lineColor = isGrounded ? Color.green : Color.red; // Verde se estiver no ch�o, vermelho se n�o estiver
        Debug.DrawLine(origin, origin + Vector2.down * groundCheckDistance, lineColor);
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

    public void ApplyJump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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
