using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    public void ChangeScene(int sceneIndex)
    {
        GameManager.Instance.ChangeScene(sceneIndex);
    }
}
