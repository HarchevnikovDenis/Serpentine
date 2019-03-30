using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour      //Скрипт для кнопки рестарта(поскольку при перезапуске сцены теряется ссылка на GameManager)
{
    public void OnButtonClick()
    {
        GameManager.Instance.Restart();
    }
}
