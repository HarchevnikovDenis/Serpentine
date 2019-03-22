using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject[] obstacle;
    private GameObject currentObstacle;
    private GameObject _parent;
    private float chance;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 1f);
        if(chance > 0.75f)
            return;

        int index = Random.Range(0, obstacle.Length);
        currentObstacle = obstacle[index];
        pos = transform.position;
        if(currentObstacle.tag == "Coin")
        {
            float difference = Random.Range(1f, 4.9f);
            pos.y += difference;
        }

        MovingRoad road = GetComponentInParent<MovingRoad>();
        GameObject obst = Instantiate(currentObstacle, pos, Quaternion.identity) as GameObject;
        
        obst.transform.SetParent(road.gameObject.transform);
        if(obst.gameObject.tag == "Coin" && obst.gameObject.tag == "UpObstacle")
            return;
        float _scale = Random.Range(1f, 1.23f);
        obst.transform.localScale *= _scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
