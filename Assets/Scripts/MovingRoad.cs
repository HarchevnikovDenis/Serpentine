using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoad : MonoBehaviour
{
    //public GameObject lastAddedRoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 99 * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * 1.8382f);

        if(transform.position.y > 40f)
            Destroy(gameObject);
    }
}
