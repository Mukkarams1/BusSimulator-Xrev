using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;

public class BusSpecifictionPanel : MonoBehaviour
{
    [SerializeField] Button nextBtn;
    [SerializeField] Button BackBtn;

    public RCC_CarControllerV3 bus;

    [SerializeField] Slider handlingText;
    [SerializeField] Slider accelerationText;
    [SerializeField] Slider MaxSpeedText;
    [SerializeField] Slider BrakeText;
    // Start is called before the first frame update
    void Start()
    {
        getRccController();
        nextBtn.onClick.AddListener(getRccController);
        BackBtn.onClick.AddListener(getRccController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void getRccController()
    {
        bus = FindObjectOfType<RCC_CarControllerV3>();
        SetBusSpecification();
    }

    private void SetBusSpecification()
    {
        MaxSpeedText.value = bus.maxspeed/360;
        BrakeText.value = bus.brakeTorque/20000;
        accelerationText.value = bus.maxEngineTorque/2000;
        handlingText.value = bus.TCSStrength/3f;

        Debug.Log("maxspeed = " + bus.maxspeed + " brakeTorque = " + bus.brakeTorque + " maxEngineTorque = " + bus.maxEngineTorque + " TCSStrength = "  + bus.TCSStrength);
    }
}
