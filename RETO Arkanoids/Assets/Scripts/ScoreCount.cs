using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    //public float points = 100f;
    public float score = 0f;
    public float highScore = 0f;

    [SerializeField]
    TextMeshProUGUI scoreNumber;

    [SerializeField]
    TextMeshProUGUI highscoreNumber;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints(float points)
    {
        score = score + points;
        scoreNumber.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;
            highscoreNumber.text = highScore.ToString("00000");
        }
    }
}
