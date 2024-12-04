using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject startMenu, player, ball, gameOverMenu, victoryMenu, soundsMenu, musicMenu;

    [SerializeField]
    public Slider soundsSlider, musicSlider;

    [SerializeField]
    TextMeshProUGUI timerNumber, scoreNumber, lifesNumber, scoreNumber2;

    private float startingTimer = 0f;

    private bool isStartClicked = false;

    private float lifes = 3f;

    public AudioSource soundsSource, musicSource;

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

        lifes = 3f;

        soundsSlider.value = soundsSource.volume;
        musicSlider.value = musicSource.volume;

        timerNumber.text = "00:00";
        scoreNumber.text = "00000";
        scoreNumber2.text = "00000";
        lifesNumber.text = "3";
    }

    void Update()
    {
        if (isStartClicked == true && lifes > 0 && brickState.bricksAmount > 0)
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

    public void OptionsIsClicked()
    {
        LeanTween.moveX(soundsMenu.GetComponent<RectTransform>(), 255f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.moveX(musicMenu.GetComponent<RectTransform>(), -255f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    public void RestartIsClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitIsClicked()
    {
        Debug.Log("Quitting...");

        //PlayerPrefs.DeleteKey("High Score");

        Application.Quit();
    }

    public void LifesCount()
    {
        lifes = lifes - 1f;

        lifesNumber.text = lifes.ToString("0");

        ball.SetActive(false);

        if (lifes <= 0)
        {
            player.SetActive(false);

            LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -575f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
        else
        {
            BallRespawn();
        }
    }

    public void AddLifes()
    {
        lifes = lifes + 1f;

        lifesNumber.text = lifes.ToString("0");

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

    public void SoundVolume()
    {
        soundsSource.volume = soundsSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
