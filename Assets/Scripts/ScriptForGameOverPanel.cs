using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForGameOverPanel : MonoBehaviour
{
    [SerializeField]private GameObject _panel;
    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isOver)
            _panel.gameObject.SetActive(false);
        else
            _panel.gameObject.SetActive(true);
    }
}
