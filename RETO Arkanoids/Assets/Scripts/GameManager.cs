using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int level = 1;

    [SerializeField]
    public int lives = 3;

    [SerializeField]
    public int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void NewGame()
    {
        this.lives = 3;
        this.score = 0;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level " + level);
    }
}
