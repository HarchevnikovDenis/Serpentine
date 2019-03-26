using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private PlayerController player;
    [HideInInspector]public int bestScore;
    [HideInInspector]public int currentScore;
    [HideInInspector]public int coinsCount;
    [HideInInspector]public bool isPaused;
    // Start is called before the first frame update
    void Awake()
    {
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

        if(instance == null)
            instance = this;
        else if(instance != this)
                Destroy(this);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused)
            player.enabled = false;
        if(player.isOver)
            player.enabled = false;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        PlayerPrefs.SetInt("COINS", coinsCount);
        if(currentScore > bestScore)
            PlayerPrefs.SetInt("BEST", currentScore);
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
}
