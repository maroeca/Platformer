using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] CharacterHealth characterHealth;

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
        //characterHealth.SetLives(maxLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void GoBackToMenu()
    {
        ChangeScene(0);
    }

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void IncreaseDeath()
    {
        deaths++;
        UIManager.Instance.RemoveLife();
    }

    public void FinishGame()
    {
        Debug.Log("Você terminou a fase!");
        UIManager.Instance.ShowEndingPopup();
    }

    public void CalculateScore()
    {

    }

}
