using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{

    // Properties
    public float speed;
    public float fuel;
    public float collisionCount;
    public int passengerCapacity;
    public Vector3 currentLocation;
    public Vector3 currentDestination;
    public List<Vector3> currentRoute;
    public BusState currentState;
    public RCC_CarControllerV3 controller;


    private void Start()
    {
        controller = FindObjectOfType<RCC_CarControllerV3>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name!="BusStop")
        {
            EventManager.BusCollision();
        }
    }
    int once = 0;
    private void Update()
    {
        speed =  controller.speed;
        //Debug.Log(speed);
        if (speed > LevelsDataManager.Instance.starWinningSpeed && once == 0)
        {
            once = 1;
            EventManager.StarWinningSpeedReached();
        }
    }
    public void Turn()
    {
        // Implement turning logic here
    }

    public void LoadPassengers(List<Passenger> passengers)
    {
        // Implement loading passengers logic here
    }

    public void UnloadPassengers(List<Passenger> passengers)
    {
        // Implement unloading passengers logic here
    }

    public void Refuel()
    {
        // Implement refueling logic here
    }

    public void Repair()
    {
        // Implement repairing logic here
    }

    public void CheckState()
    {
        // Implement state checking logic here
    }

    // Enums
    public enum BusState
    {
        Idle,
        Pickup,
        Dropoff,
        Moving,
        Finished
    }
}
