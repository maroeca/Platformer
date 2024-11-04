using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenuComponent : MonoBehaviour
{
    [SerializeField] Button restartButton, goBackMenuButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(RestartGame);
        goBackMenuButton.onClick.AddListener(GoBackToMenu);
    }

    private void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    private void GoBackToMenu()
    {
        GameManager.Instance.GoBackToMenu();
    }
}
