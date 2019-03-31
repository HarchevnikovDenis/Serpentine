using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField]private GameObject gameManager;
    [SerializeField]private GameObject soundManager;

    void Awake()
    {
        if(GameManager.Instance == null)
        {
            Instantiate(gameManager);
        }
        if(SoundManager.Instance == null)
        {
            Instantiate(soundManager);
        }
    }
}
