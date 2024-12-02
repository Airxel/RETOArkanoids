using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject startMenu, player, ball, gameOverMenu, victoryMenu;

    [SerializeField]
    TextMeshProUGUI timerNumber, scoreNumber, livesNumber, scoreNumber2;

    private float startingTimer = 0f;

    private bool isStartClicked = false;

    private float lives = 3f;

    public BrickState brickState;

    public ScoreCount scoreCount;

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

    void Start()
    {
        player.SetActive(false);

        ball.SetActive(false);

        lives = 3f;

        timerNumber.text = "00:00";
        scoreNumber.text = "00000";
        scoreNumber2.text = "00000";
        livesNumber.text = "3";
    }

    void Update()
    {
        if (isStartClicked == true && lives > 0 && brickState.bricksAmount > 0)
        {
            startingTimer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(startingTimer / 60f);
            int seconds = Mathf.FloorToInt(startingTimer % 60f);

            timerNumber.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (isStartClicked == true && brickState.bricksAmount <= 0)
        {
            BricksCheck();
        }
    }

    public void StartIsClicked()
    {
        isStartClicked = true;

        player.SetActive(true);

        ball.SetActive(true);

        LeanTween.moveY(startMenu.GetComponent<RectTransform>(), 650f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    public void RestartIsClicked()
    {
        lives = 3f;
        startingTimer = 0f;

        scoreCount.score = 0f;

        timerNumber.text = "00:00";
        scoreNumber.text = "00000";
        scoreNumber2.text = "00000";
        livesNumber.text = "3";

        LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), 430f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.moveY(victoryMenu.GetComponent<RectTransform>(), 430f, 0.5f).setEase(LeanTweenType.easeInOutSine);

        isStartClicked = true;

        player.SetActive(true);

        ball.SetActive(true);
    }

    public void QuitIsClicked()
    {
        Debug.Log("Quitting...");

        //PlayerPrefs.DeleteKey("High Score");

        Application.Quit();
    }

    public void LivesCount()
    {
        lives = lives - 1f;

        livesNumber.text = lives.ToString("0");

        ball.SetActive(false);

        if (lives <= 0)
        {
            player.SetActive(false);

            LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -575f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
        else
        {
            BallRespawn();
        }
    }

    public void BallRespawn()
    {
        ball.SetActive(true);

        ball.transform.localPosition = new Vector3(0f, 10f, 0f);
    }

    public void BricksCheck()
    {
        player.SetActive(false);

        ball.SetActive(false);

        isStartClicked = false;

        LeanTween.moveY(victoryMenu.GetComponent<RectTransform>(), -575f, 0.5f).setEase(LeanTweenType.easeInOutSine);    
    }
}
