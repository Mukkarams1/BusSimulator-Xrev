using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using UnityEngine.SceneManagement;

public class ActivateBusAiCutscene : MonoBehaviour
{
    public Transform BusStartingPoint;
    public GameObject BusStopTrigger;
    private GameObject tmp;
    private bool check;
    public RCC_AIWaypointsContainer waypointsContainer;
    public GameObject AIBUS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Obect + " + other.gameObject);
            other.gameObject.SetActive(false);
            BusStopTrigger.gameObject.GetComponent<BoxCollider>().enabled = true;
            Instantiate(AIBUS, BusStartingPoint.position, BusStartingPoint.rotation);
            // AIBUS.gameObject.GetComponent<RCC_AICarController>().waypointsContainer = waypointsContainer;
            RCC_AICarController.Instance.waypointsContainer = waypointsContainer;

            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.SetActive(false);
            if (SceneManager.GetActiveScene().name == "CareerMode")
            {
                CarrierModeManager.Instance.RCC_Camera.SetActive(false);
            }
            if (SceneManager.GetActiveScene().name == "ObstacleMode")
            {
                ObstacleModeManager.Instance.RCC_Camera.SetActive(false);
            }
            PickNDropAnimationController.Instance.TrafficHolder = GameObject.FindGameObjectWithTag("TrafficHolder");
            GameObject.FindGameObjectWithTag("TrafficHolder").SetActive(false);
        }
    }
}
