using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Se não houver uma instância ainda, define esta como a instância e a persiste entre cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o GameManager ao carregar novas cenas
        }
        else
        {
            // Caso já exista uma instância, destrói o novo objeto para garantir o Singleton
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private List<GameObject> lives;


    public void RemoveLife()
    {
        lives[0].GetComponent<PulseDestroyComponent>().PulseAndDestroy();
        lives.RemoveAt(0);
    }
}
