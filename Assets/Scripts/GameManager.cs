using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;         //Singleton
    [SerializeField] private PlayerController player;   //Ссылка на игрока
    [HideInInspector]public int bestScore;              //Лучший результат
    [HideInInspector]public int currentScore;           //Текущий результат
    [HideInInspector]public int coinsCount;             //Кол-во монет
    [HideInInspector]public bool isPaused;              //Проверка паузы
    private GameObject _GameOverPanel;                  //Панель паузы
    private Text _PauseBestScoreText;                   //Best score во время паузы
    private Text _GameOverBestScoreText;                 //Best score во время смерти
    public bool isOver = false;                         //Идет ли еще игра

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            {
                Destroy(this);
                return;
            }
        DontDestroyOnLoad(gameObject);

        isOver = false;
        _GameOverPanel = GameObject.Find("GameOverPanel");
        _GameOverPanel.SetActive(false);

        isPaused = false;
        if(!PlayerPrefs.HasKey("COINS"))
            coinsCount = 0;
        else
            coinsCount = PlayerPrefs.GetInt("COINS");
       
        currentScore = 0;

        if(!PlayerPrefs.HasKey("BEST"))
            bestScore = 0;
        else
            bestScore = PlayerPrefs.GetInt("BEST");
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
            player.enabled = false;
        if(player.isOver)
            player.enabled = false;
        if(_GameOverPanel == null)
        {
            _GameOverPanel = GameObject.Find("GameOverPanel");
            _GameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("COINS", coinsCount);
        _GameOverPanel.SetActive(true);
        isPaused = true;
        if(currentScore > bestScore)
            PlayerPrefs.SetInt("BEST", currentScore);
        isOver = true;
    }


    public void Paused()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        isPaused = true;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        isPaused = false;
        currentScore = 0;
        isOver = false;
    }
}
