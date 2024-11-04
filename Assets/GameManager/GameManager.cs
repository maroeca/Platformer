using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterHealth characterHealth;

    #region Singleton
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
    #endregion

    public int maxLives = 3;
    private int deaths = 0;

    public void RestartGame()
    {
        characterHealth.SetLives(maxLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void IncreaseDeath()
    {
        deaths++;
        UIManager.Instance.RemoveLife();
    }

    public void FinishGame()
    {
        Debug.Log("Voc� terminou a fase!");
        UIManager.Instance.ShowEndingPopup();
    }

}
