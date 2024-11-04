using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // Se n�o houver uma inst�ncia ainda, define esta como a inst�ncia e a persiste entre cenas
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mant�m o GameManager ao carregar novas cenas
        }
        else
        {
            // Caso j� exista uma inst�ncia, destr�i o novo objeto para garantir o Singleton
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
