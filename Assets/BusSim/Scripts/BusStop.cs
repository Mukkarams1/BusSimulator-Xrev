using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
     
            Debug.Log("bus stop reached");
            EventManager.ReachedBusStop(gameObject.transform);
            Destroy(gameObject);     
    }
}
