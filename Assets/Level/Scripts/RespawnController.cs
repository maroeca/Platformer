using UnityEngine;
using System.Collections.Generic;

public class RespawnController : MonoBehaviour
{
    public static RespawnController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform respawnPosition; // A posi��o atual de respawn
    public List<CheckpointBehaviour> respawnPoints; // Lista de pontos de respawn

    public void SetRespawn(CheckpointBehaviour reachedPoint)
    {
        // Atualiza a posi��o de respawn
        respawnPosition.position = reachedPoint.transform.position;

        // Encontra o �ndice do ponto de respawn atingido
        int index = respawnPoints.IndexOf(reachedPoint);

        // Desativa o Collider do ponto de respawn para evitar que seja acionado novamente
        reachedPoint.GetComponent<Collider2D>().enabled = false;

        // Remove todos os pontos anteriores (e o pr�prio ponto atingido)
        if (index >= 0)
        {
            respawnPoints.RemoveRange(0, index + 1);
        }
    }
}
