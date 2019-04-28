using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointSpawn : MonoBehaviour
{
    [SerializeField]private GameObject[] _playerPrefabs;        //Префабы Скинов игрока
                    private int playerIndex;                    //Индекс скина

    void Awake()
    {
        if(!PlayerPrefs.HasKey("PLAYER"))
            PlayerPrefs.SetInt("PLAYER", 0);
        
        playerIndex = PlayerPrefs.GetInt("PLAYER");
        Instantiate(_playerPrefabs[playerIndex], transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
