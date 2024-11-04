using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] List<ScalePopupComponent> stars;

    private void Start()
    {
        SetScore();
    }

    public void SetScore()
    {
        scoreText.text = "Pontos: " + GameManager.Instance.Score.ToString();

        int rating = GameManager.Instance.StarRating;
        for (int i = 0; i < rating; i++)
        {
            stars[i].gameObject.SetActive(true);
        }
    }
}
