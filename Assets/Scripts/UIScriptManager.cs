using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScriptManager : MonoBehaviour
{
    [SerializeField] private Text _text;					//Отображание SCORE на экране
	[SerializeField] private Text _textCoins;				//Отображение кол-ва монет на экране

    // Update is called once per frame
    void Update()
    {
        _textCoins.text = GameManager.Instance.coinsCount.ToString();
        _text.text = GameManager.Instance.currentScore.ToString();
    }
}
