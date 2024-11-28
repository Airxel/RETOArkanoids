using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreNumber, highscoreNumber;

    public float score = 0f;

    public float highScore = 0f;

    public static ScoreCount instance;

    private void Awake()
    {
        if (ScoreCount.instance == null)
        {
            ScoreCount.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
     void Start()
    {
        highScore = PlayerPrefs.GetFloat("High Score", 0);

        highscoreNumber.text = highScore.ToString("00000");
    }

    public void AddPoints(float points)
    {
        score = score + points;

        scoreNumber.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;

            PlayerPrefs.SetFloat("High Score", highScore);

            highscoreNumber.text = highScore.ToString("00000");
        }
    }
}
