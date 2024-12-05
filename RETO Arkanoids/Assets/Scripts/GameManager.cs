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

    //Referencia al audio
    public AudioSource soundsSource, musicSource;

    //Referencia al script BrickState
    public BrickState brickState;

    //Referencia al script ScoreCount
    public ScoreCount scoreCount;

    //Singleton
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

    private void Start()
    {
        //Valores bases generales al comenzar
        player.SetActive(false);
        ball.SetActive(false);

        lifes = 3f;

        //Sliders y volómenes
        soundsSlider.value = soundsSource.volume;
        musicSlider.value = musicSource.volume;

        //Textos de la UI
        timerNumber.text = "00:00";
        scoreNumber.text = "00000";
        scoreNumber2.text = "00000";
        lifesNumber.text = "3";
    }

    private void Update()
    {
        //Si se ha pulsado Start y hay bricks en la escena, se activa el timer
        if (isStartClicked == true && lifes > 0 && brickState.bricksAmount > 0)
        {
            startingTimer += Time.deltaTime;

            int minutes = Mathf.FloorToInt(startingTimer / 60f);
            int seconds = Mathf.FloorToInt(startingTimer % 60f);

            timerNumber.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        //Si la cantidad de bricks es 0, se activa la función
        else if (isStartClicked == true && brickState.bricksAmount <= 0)
        {
            BricksCheck();
        }
    }

    /// <summary>
    /// Valores y animaciones que se activan al darle click al botón Start de la UI
    /// </summary>
    public void StartIsClicked()
    {
        isStartClicked = true;

        player.SetActive(true);

        ball.SetActive(true);

        LeanTween.moveY(startMenu.GetComponent<RectTransform>(), 650f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    /// <summary>
    /// Animaciones que se activan al darle click al botón Opciones de la UI
    /// </summary>
    public void OptionsIsClicked()
    {
        LeanTween.moveX(soundsMenu.GetComponent<RectTransform>(), 255f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.moveX(musicMenu.GetComponent<RectTransform>(), -255f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }

    /// <summary>
    /// Acción que se activa al darle click al botón Restart de la UI
    /// </summary>
    public void RestartIsClicked()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Acciones que se activan al darle click al botón Quit de la UI
    /// </summary>
    public void QuitIsClicked()
    {
        Debug.Log("Quitting...");

        //PlayerPrefs.DeleteKey("High Score");

        Application.Quit();
    }

    /// <summary>
    /// Función que controla la cantidad de vidas del jugador, al golpear el muro inferior
    /// </summary>
    public void LifesCount()
    {
        lifes = lifes - 1f;

        lifesNumber.text = lifes.ToString("0");

        ball.SetActive(false);

        //Si las vidas son 0, se desactiva al jugador y sale el menú de derrota
        if (lifes <= 0)
        {
            player.SetActive(false);

            LeanTween.moveY(gameOverMenu.GetComponent<RectTransform>(), -575f, 0.5f).setEase(LeanTweenType.easeInOutSine);
        }
        //Si son más, se llama a la función
        else
        {
            BallRespawn();
        }
    }

    /// <summary>
    /// Se suma una vida al colisionar con el power-up de lifes
    /// </summary>
    public void AddLifes()
    {
        lifes = lifes + 1f;

        lifesNumber.text = lifes.ToString("0");
    }

    //La bola vuelve a su posición inicial
    public void BallRespawn()
    {
        ball.SetActive(true);

        ball.transform.localPosition = new Vector3(0f, 10f, 0f);
    }

    //Cuando el contador de bricks es cero, se desactiva todo y aparece el menú de victoria
    public void BricksCheck()
    {
        player.SetActive(false);

        ball.SetActive(false);

        isStartClicked = false;

        LeanTween.moveY(victoryMenu.GetComponent<RectTransform>(), -575f, 0.5f).setEase(LeanTweenType.easeInOutSine);    
    }

    /// <summary>
    /// Función que controla el sonido con los sliders
    /// </summary>
    public void SoundVolume()
    {
        soundsSource.volume = soundsSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
