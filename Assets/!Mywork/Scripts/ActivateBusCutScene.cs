using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActivateCutScene", menuName = "Scriptable Objects/ActivateCutScene")]
public class ActivateBusCutScene : ScriptableObject
{
    [SerializeField]
    public Transform BusStartingPoint;
    [SerializeField]
    public GameObject BusStopTrigger;
    [SerializeField]
    private GameObject tmp;
    [SerializeField]
    private bool check;
    [SerializeField]
    public RCC_AIWaypointsContainer waypointsContainer;
    [SerializeField]
    public GameObject DummyBus;
    [SerializeField]
    public GameObject CurrentTriggerObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Obect + " + other.gameObject);
            other.gameObject.SetActive(false);
            BusStopTrigger.gameObject.GetComponent<BoxCollider>().enabled = true;
            Instantiate(DummyBus, BusStartingPoint.position, BusStartingPoint.rotation);
            DummyBus.gameObject.GetComponent<RCC_AICarController>().waypointsContainer = waypointsContainer;

            CurrentTriggerObject.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
