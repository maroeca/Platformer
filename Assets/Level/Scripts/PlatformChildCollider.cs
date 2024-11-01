using UnityEngine;

public class PlatformChildCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Define o jogador como filho da plataforma
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remove o jogador como filho da plataforma
            collision.transform.SetParent(null);
        }
    }

}
