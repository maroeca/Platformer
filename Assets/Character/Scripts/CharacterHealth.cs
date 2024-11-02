using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public int deaths { get; private set; }

    public float fallThreshold = 2f; // altura para checar se colidiu com o box de morte
    public LayerMask deadZoneLayer;

    [SerializeField] private Transform respawnPosition;

    private void Awake()
    {
        currentLives = maxLives;
        deaths = 0;
    }

    private void Update()
    {
        CheckFall();
    }

    private void CheckFall()
    {
        // Lança um Raycast para baixo para checar se encontra a deadzone abaixo do personagem
        if (IsGroundBelow())
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

        if (currentLives > 0)
        {
            Respawn();
        }
        else
        {
            // Trigger game over ou lógica de reset
            Debug.Log("Game Over");
            Respawn(); // sempre respawn para teste
        }
    }

    private void Respawn()
    {
        // Coloque o personagem de volta na posição inicial ou no último checkpoint
        transform.position = respawnPosition.position;
    }

    public void SetLives(int amount)
    {
        currentLives = amount;
    }
}
