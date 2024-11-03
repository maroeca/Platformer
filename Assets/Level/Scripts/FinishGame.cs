using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notifica o controlador para atualizar o ponto de respawn
            GameManager.Instance.FinishGame();
        }
    }
}
