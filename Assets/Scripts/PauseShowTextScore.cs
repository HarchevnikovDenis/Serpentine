using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseShowTextScore : MonoBehaviour
{
    private Text _textOnPause;

    void Awake()
    {
        _textOnPause = GetComponent<Text>();
        StartPause();
    }

    void StartPause()
    {
        int best = PlayerPrefs.GetInt("BEST");
        _textOnPause.text = "BEST: " + best;
        Debug.Log(best);
    }
}
