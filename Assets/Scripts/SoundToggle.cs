using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SoundToggle : MonoBehaviour
{
    private Toggle _toggle;
    private float _valueToggle;
    // Start is called before the first frame update
    void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _valueToggle = SoundManager.Instance.soundLevel;
        if(_valueToggle == 1f)
            _toggle.isOn = false;
        else
            _toggle.isOn = true;
    }

    public void ChangingValue()
    {
        if(_toggle.isOn)
        {
            SoundManager.Instance.soundLevel = 0f;
            PlayerPrefs.SetFloat("SOUND", 0f);
        }
        else
        {
            SoundManager.Instance.soundLevel = 1f;
            PlayerPrefs.SetFloat("SOUND", 1f);
        }
    }
}
