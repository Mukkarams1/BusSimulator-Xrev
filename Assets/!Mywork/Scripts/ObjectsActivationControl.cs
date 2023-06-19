using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsActivationControl : MonoBehaviour
{
    public GameObject[] ObjectToActivate, ObjectsToDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < ObjectsToDeactivate.Length; i++)
            {
                ObjectsToDeactivate[i].SetActive(false);
            }
            for (int i=0; i< ObjectToActivate.Length; i++)
            {
                ObjectToActivate[i].SetActive(true);
            }
        }
    }
}
