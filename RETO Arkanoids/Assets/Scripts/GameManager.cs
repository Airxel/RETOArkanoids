using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool isGameStarted = false;

    [SerializeField]
    TextMeshProUGUI timer;

    [SerializeField]
    TextMeshProUGUI score;

    [SerializeField]
    TextMeshProUGUI lifes;

    private float startingTimer = 0f;

    [SerializeField]
    GameObject startMenu, player, ball;

    private void Awake()
    {
        player.SetActive(false);
        ball.SetActive(false);

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted == true)
        {
            startingTimer = startingTimer + Time.deltaTime;
            timer.text = startingTimer.ToString("00:00");
            Debug.Log("Game Started");
        }
    }

    public void GameStart()
    {
        isGameStarted = true;
        startMenu.SetActive(false);
        player.SetActive(true);
        ball.SetActive(true);

        timer.text = "00:05";
        score.text = "00000";
        lifes.text = "3";
    }
}
