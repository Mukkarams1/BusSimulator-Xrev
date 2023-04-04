using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvas : MonoBehaviour
{
    [SerializeField] GameObject objectivepanel;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.onContinueToNextLevel += ActiveObjectivePanel;
    }

    private void ActiveObjectivePanel()
    {
        objectivepanel.SetActive(true);
    }

   
}
