using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    public static GameManager instance;

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

            //ball.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.27f, 0f);
        }
    }

    public void StartIsClicked()
    {
        isGameStarted = true;

        timer.text = "00:00";
        score.text = "00000";
        lifes.text = "3";

        player.SetActive(true);
        ball.SetActive(true);

        LeanTween.moveY(startMenu.GetComponent<RectTransform>(), 430f, 0.5f).setEase(LeanTweenType.easeInOutSine);
    }
}
