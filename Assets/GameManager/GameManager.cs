using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterHealth characterHealth;
    public static GameManager Instance { get; private set; }

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

    public int maxLives = 3;
    private int deaths = 0;

    public void RestartGame()
    {
        characterHealth.SetLives(maxLives);
    }

    public void IncreaseDeath()
    {
        deaths++;
    }

}
