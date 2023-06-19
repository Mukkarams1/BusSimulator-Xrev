using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class PassengerCount : MonoBehaviour
{
    public static PassengerCount instance;
    public int count;
    int pass_count = 0;
    //public GameObject[] passengers;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    void Start()
    {
        count = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "passenger")
        {
            count++;
            pass_count++;
            other.gameObject.GetComponent<Animator>().SetBool("iswalk", false);
            other.gameObject.GetComponent<splineMove>().enabled = false;
            other.gameObject.SetActive(false);
            if(pass_count == 3)
            {
                PickNDropAnimationController.Instance.isBusDoor_Close = true;
                pass_count = 0;
            }   
        }

    }
}
