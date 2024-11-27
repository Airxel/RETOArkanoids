using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private bool isStartClicked = false;

    [SerializeField]
    TextMeshProUGUI timerNumber;

    [SerializeField]
    TextMeshProUGUI scoreNumber;

    [SerializeField]
    TextMeshProUGUI livesNumber;

    private float startingTimer = 0f;

    [SerializeField]
    GameObject startMenu, player, ball, gameOverMenu;

    private float lives = 3f;

    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        ball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStartClicked == true && lives > 0)
        {
            startingTimer = startingTimer + Time.deltaTime;
            timerNumber.text = startingTimer.ToString("00:00");
        }
    }

    public void StartIsClicked()
    {
        isStartClicked = true;

        timerNumber.text = "00:00";
        scoreNumber.text = "00000";
        livesNumber.text = "3";

        player.SetActive(true);
        ball.SetActive(true);

        LeanTween.moveY(startMenu.GetComponent<RectTransform>(), 430f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    public void QuitIsClicked()
    {
        Application.Quit();
    }

    public void livesCount()
    {
        Debug.Log("Deadge");

        lives = lives - 1;

        livesNumber.text = lives.ToString("0");

        ball.SetActive(false);

        if (lives <= 0)
        {
            player.SetActive(false);
            LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -475f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
        else
        {
            Invoke(nameof(BallRespawn), 1f);
        }
    }

    public void BallRespawn()
    {
        ball.SetActive(true);
        ball.transform.parent = player.transform;
        ball.transform.localPosition = new Vector3(0f, 10f, 0f);
    }
}
