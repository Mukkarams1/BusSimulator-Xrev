using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCutSceneBuses : MonoBehaviour
{
    public static switchCutSceneBuses Instance;
    public GameObject SceneBus;
    public GameObject NextSceneSwitchObject;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AIBUS"))
        {
            SceneBus = PickNDropAnimationController.Instance.SceneBUS;
            Debug.Log("AIBUS CutScene completed Trigger");
           // SceneBus = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("AI BUS = " + other.gameObject);
            Debug.Log("MAIN BUS = " + SceneBus.gameObject);
            SceneBus.transform.position = other.gameObject.transform.position;
            SceneBus.transform.rotation = other.gameObject.transform.rotation;
            Destroy(other.gameObject);
            SceneBus.SetActive(true);
            Destroy(this);
            if (NextSceneSwitchObject != null)
            {
                NextSceneSwitchObject.SetActive(true);
            }
        }
    }
}
