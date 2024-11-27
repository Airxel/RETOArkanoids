using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
            startingTimer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(startingTimer / 60f);
            int seconds = Mathf.FloorToInt(startingTimer % 60f);

            timerNumber.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
        Debug.Log("Quitting...");
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
            LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -490f, 0.5f).setEase(LeanTweenType.easeInOutSine);
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
}
