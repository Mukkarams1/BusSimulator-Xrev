using UnityEngine;

public class Passenger : MonoBehaviour
{

    // Properties
    public string PassengerName;
    public int speed;
    public Vector3 currentLocation;
    public Vector3 currentDestination;
    public PassengerState currentState;

    // Methods
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentDestination, speed * Time.deltaTime);
        if (transform.position == currentDestination)
        {
            if (currentDestination == currentLocation)
            {
                currentState = PassengerState.Waiting;
            }
            else
            {
                currentLocation = currentDestination;
                currentState = PassengerState.Traveling;
            }
        }
    }

    public void Board()
    {
        // Implement boarding logic here
    }

    public void Disembark()
    {
        // Implement disembarking logic here
    }

    public void CheckState()
    {
        // Implement state checking logic here
    }

    // Enums
    public enum PassengerState
    {
        Waiting,
        Traveling,
        Boarding,
        Disembarking
    }
}