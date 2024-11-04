using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] CharacterHealth characterHealth;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion
    private float gameTime;
    private bool isCounting;

    public int maxLives = 3;
    private int deaths = 0;

    private int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private int startRating;

    public int StarRating
    {
        get { return startRating; }
        set { startRating = value; }
    }

    private int maxScore = 16000;

    int[] starThresholds;


    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica se a cena carregada � a cena de Gameplay
        if (scene.name == "Gameplay")
        {
            StartTimer();
        }
    }

    private void Start()
    {
        starThresholds = new []{
            (int)(0.2 * maxScore), // 1 estrela para 20% do score m�ximo
            (int)(0.4 * maxScore), // 2 estrelas para 40% do score m�ximo
            (int)(0.6 * maxScore), // 3 estrelas para 60% do score m�ximo
            (int)(0.8 * maxScore), // 4 estrelas para 80% do score m�ximo
            maxScore // 5 estrelas para 100% do score m�ximo
        };
    }

    public void StartTimer()
    {
        gameTime = 0f;
        isCounting = true;
    }

    public void StopTimer()
    {
        isCounting = false;
        Debug.Log("Tempo final: " + gameTime + " segundos");
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    private void Update()
    {
        if (isCounting)
        {
            gameTime += Time.deltaTime;
            //Debug.Log(gameTime);
        }
    }


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

    [ContextMenu("test")]
    public void FinishGame()
    {
        Debug.Log("Voc� terminou a fase!");
        StopTimer();
        CalculateScore();
        UIManager.Instance.ShowEndingPopup();
    }

    
    public void CalculateScore()
    {
        int scoreFactor = deaths > 0 ? deaths : 1;
        int baseScore = 1000000;
        Score = (int)(baseScore / gameTime) / scoreFactor;

        CalculateStars(Score);
        Debug.Log(Score);
    }

    public void CalculateStars(int score)
    {
        int stars = 0;

        for (int i = 0; i < starThresholds.Length; i++)
        {
            if (score >= starThresholds[i])
            {
                stars = i + 1; // Incrementa uma estrela conforme o score alcan�a o pr�ximo limite
            }
            else
            {
                break; // Para o loop se o score n�o alcan�ar o pr�ximo limite
            }
        }

        StarRating = stars;
    }

}
