using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickPoint"))
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
                Debug.Log("PickPointReached");
                PickNDropAnimationController.Instance.isPick = true;
                PickNDropAnimationController.Instance.ParkingCollider = other.gameObject.transform;
                PickNDropAnimationController.Instance.isBusDoor_Open = true;
                Debug.Log("Bus Door Open? " + PickNDropAnimationController.Instance.isBusDoor_Open);

        }
        if (other.gameObject.CompareTag("DropPoint"))
        {
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("DropPointReached");
                PickNDropAnimationController.Instance.isDrop = true;
                PickNDropAnimationController.Instance.ParkingCollider = other.gameObject.transform;
                PickNDropAnimationController.Instance.isBusDoor_Open = true;
                Debug.Log("Bus Door Open? " + PickNDropAnimationController.Instance.isBusDoor_Open);
        }
        if (other.gameObject.CompareTag("ParkingPoint"))
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(0).gameObject.GetComponent<ParkingWiningCam>().enabled = true;
        }
    }
}
