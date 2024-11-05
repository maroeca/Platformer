using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckpointBehaviour : MonoBehaviour
{
    [SerializeField]private int checkpointId;

    public int CheckpointId
    {
        get { return checkpointId; }
        set { checkpointId = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notifica o controlador para atualizar o ponto de respawn
            RespawnController.Instance.SetRespawn(this);
            UIManager.Instance.ShowCheckpointPopup(transform);
            AnalyticsManager.Instance.CheckpointCollected(CheckpointId);
        }
    }
}
