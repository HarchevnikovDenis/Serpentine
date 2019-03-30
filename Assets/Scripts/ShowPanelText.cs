using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPanelText : MonoBehaviour
{
    private Text _textScore;

    void Awake()
    {
        _textScore = GetComponent<Text>();
    }

    void Update()
    {
        if(GameManager.Instance.isOver)
            ShowResults();
    }

    private void ShowResults() {
        if(GameManager.Instance.currentScore > GameManager.Instance.bestScore)
            _textScore.text = "BEST SCORE: " + GameManager.Instance.currentScore;
        else
            _textScore.text = "Your Score: " + GameManager.Instance.currentScore;
    }
}
