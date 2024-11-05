using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.RestartGame();
    }

    public void BackToMenu()
    {
        GameManager.Instance.GoBackToMenu();
    }
}
