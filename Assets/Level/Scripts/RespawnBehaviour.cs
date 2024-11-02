using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RespawnBehaviour : MonoBehaviour
{    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notifica o controlador para atualizar o ponto de respawn
            RespawnController.Instance.SetRespawn(this);
        }
    }
}
