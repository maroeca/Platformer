using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterHealth characterHealth;

    #region Singleton
    public static GameManager Instance { get; private set; }

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

    public int maxLives = 3;
    private int deaths = 0;

    public void RestartGame()
    {
        characterHealth.SetLives(maxLives);
    }

    public void IncreaseDeath()
    {
        deaths++;
        UIManager.Instance.RemoveLife();
    }

    public void FinishGame()
    {
        Debug.Log("Você terminou a fase!");
    }

}
