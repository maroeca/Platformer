using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class AnalyticsManager : MonoBehaviour
{
    #region Singleton
    public static AnalyticsManager Instance { get; private set; }

    private void Awake()
    {
        // Se não houver uma instância ainda, define esta como a instância e a persiste entre cenas
        if (Instance == null)
        {
            Instance = this;            
        }
        else
        {
            // Caso já exista uma instância, destrói o novo objeto para garantir o Singleton
            Destroy(gameObject);
        }        
    }
    #endregion

    private bool isInitialized = false;
    private async void Start()
    {
        await UnityServices.InitializeAsync(); //Inicia asincronamente o unityServices
        AnalyticsService.Instance.StartDataCollection(); //Inicia a coleta de dados
        isInitialized = true;
    }

    public void CheckpointCollected(int checkpointID)
    {
        if (!isInitialized) return;

        CustomEvent myEvent = new CustomEvent("checkpoint_collected")
        {
            { "checkpoint_index", checkpointID}
        };

        AnalyticsService.Instance.RecordEvent(myEvent);
        AnalyticsService.Instance.Flush(); //Envia os dados direto para o analytics -> Recomendado usar somente em desenvolvimento
    }

    public void RestartGame()
    {
        AnalyticsService.Instance.RecordEvent("restart_game");
        AnalyticsService.Instance.Flush();
    }

    public void FinishtGame()
    {
        AnalyticsService.Instance.RecordEvent("finish_game");
        AnalyticsService.Instance.Flush();
    }

    public void GameOver()
    {
        AnalyticsService.Instance.RecordEvent("game_over");
        AnalyticsService.Instance.Flush();
    }
}
