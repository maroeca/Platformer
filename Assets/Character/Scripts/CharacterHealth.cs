using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public int deaths { get; private set; }

    public float fallThreshold = 2f; // altura para checar se colidiu com o box de morte
    public LayerMask deadZoneLayer;

    [SerializeField] private Transform respawnPosition;

    private Rigidbody2D rb;
    private bool isDead = false;

    private void Awake()
    {
        currentLives = maxLives;
        deaths = 0;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckFall();
    }

    private void CheckFall()
    {
        // Lança um Raycast para baixo para checar se encontra a deadzone abaixo do personagem
        if (IsGroundBelow() && !isDead)
        {
            HandleDeath();
        }
    }

    private bool IsGroundBelow()
    {
        // Raycast para detectar se há deadzone abaixo do personagem
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, fallThreshold, deadZoneLayer);
        return hit.collider != null;
    }

    private void HandleDeath()
    {
        GameManager.Instance.IncreaseDeath();
        currentLives--;
        isDead = true;
        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            // Trigger game over ou lógica de reset
            Debug.Log("Game Over");
            GameManager.Instance.GameOver();
        }
    }

    private void Respawn()
    {
        rb.velocity = new Vector2(0, 0); // zera a velocidade do player ao morrer

        // Coloque o personagem de volta na posição inicial ou no último checkpoint
        transform.position = respawnPosition.position;
        isDead = false;
    }

    public void SetLives(int amount)
    {
        currentLives = amount;
    }
}
