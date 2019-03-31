using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public float soundLevel;
    
    public static SoundManager Instance
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

        if(PlayerPrefs.HasKey("SOUND"))
        {
            if(PlayerPrefs.GetFloat("SOUND") == 1f)
                soundLevel = 1f;
            else
                soundLevel = 0f;
        }
        else
        {
            PlayerPrefs.SetFloat("SOUND", 1f);
            soundLevel = 1f;
        }
    }
}
