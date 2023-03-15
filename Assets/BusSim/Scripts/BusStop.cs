using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    //Transform transform;
    //public float collsiontime;
    //private void Start()
    //{
    //    transform = GetComponent<Transform>();
    //}
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
     
        Debug.Log("bus stop reached");
        
        //if (collsiontime >= 5)
        //{
        //    collsiontime = 0;
            EventManager.ReachedBusStop(gameObject.transform);
            Destroy(gameObject);
        
        
                
    }
    //private void Update()
    //{
    //    collsiontime += Time.deltaTime;
    //}
}
