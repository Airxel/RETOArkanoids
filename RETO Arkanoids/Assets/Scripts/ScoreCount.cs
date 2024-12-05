using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreNumber, highscoreNumber, scoreNumber2;

    public float score = 0f;

    public float highScore = 0f;

    //Singleton
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
        //Se llama al valor guardado en el PlayerPrefs para la mayor puntuación
        highScore = PlayerPrefs.GetFloat("High Score", 0);

        //Se pone ese valor en el texto te la UI
        highscoreNumber.text = highScore.ToString("00000");
    }

    public void AddPoints(float points)
    {
        //Se va sumando a la puntuación los puntos obtenidos al destruir bricks
        score = score + points;

        //Se ponen esos valores en los textos de la UI
        scoreNumber.text = score.ToString("00000");
        scoreNumber2.text = score.ToString("00000");

        if (highScore < score)
        {
            highScore = score;

            //Si la puntuación actual es superior a la mayor puntuación, se guarda el valor para las siguientes partidas
            PlayerPrefs.SetFloat("High Score", highScore);

            //Y se pone el valor en la UI
            highscoreNumber.text = highScore.ToString("00000");
        }
    }
}
