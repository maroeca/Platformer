using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;  // Canvas Group para controle do fade
    public float fadeDuration = 1f;      // Duração do fade


    private void Awake()
    {
        SceneManager.sceneLoaded += EndTransition;
    }
    // Método para iniciar a transição
    public void StartTransition(int sceneIndex)
    {
        // Faz o fade out para escurecer a tela
        fadeCanvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
        {
            // Carrega a nova cena quando o fade out termina
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);

            
        });
    }

    public void EndTransition(Scene scene, LoadSceneMode mode)
    {
        // Reinicia o fade para a nova cena
        fadeCanvasGroup.alpha = 1;
        fadeCanvasGroup.DOFade(0, fadeDuration);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= EndTransition;
    }
}
