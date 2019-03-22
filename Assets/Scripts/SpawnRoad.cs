using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoad : MonoBehaviour
{
        
    [SerializeField]
    private GameObject road;    //Link for last added serpantin
    private bool isCreate = false;
    [SerializeField]
    private GameObject newPlayer;   //Prefab of Serpantin
    private Vector3 newPos;

 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isCreate && road.transform.position.y > 0.24f)
        {
            isCreate = true;
            NewRoad();
        }
    }

    void NewRoad()
    {
        //yield return new WaitForSeconds(3.8f);
        newPos = Vector3.zero;
        newPos.y = road.gameObject.transform.position.y - 13.71f;
        
        GameObject player = Instantiate(newPlayer, newPos, Quaternion.Euler(-90, 0, 0)) as GameObject;
        //road.lastAddedRoad = null;
        road = player;
        isCreate = false;
        //yield return null;
        return;
    }
}
