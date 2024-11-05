using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] private List<GameObject> lives;
    [SerializeField] private GameObject checkpointPopup;
    [SerializeField] private GameObject finishedGamedPopup;
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private GameObject menu;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject gameOver;

    private void Start()
    {
        menuButton.onClick.AddListener(OpenMenu);
    }
    public void RemoveLife()
    {
        if (lives.Count <= 0)
        {
            return;
        }
        lives[0].GetComponent<PulseDestroyComponent>().PulseAndDestroy();
        lives.RemoveAt(0);
    }

    public void ShowCheckpointPopup(Transform checkpoint)
    {
        Instantiate(checkpointPopup, checkpoint.position, checkpoint.rotation);
    }

    public void ShowEndingPopup()
    {
        Instantiate(finishedGamedPopup, UICanvas.transform);
    }

    public void OpenMenu()
    {
        menu.GetComponent<UIEnterFromTop>().ShowUIFromTop();
    }

    [ContextMenu("Test game over")]
    public void GameOver()
    {
        var gameOverInstance = Instantiate(gameOver, UICanvas.transform);
        gameOverInstance.GetComponent<UIEnterFromTop>().ShowUIFromTop();
    }
}
