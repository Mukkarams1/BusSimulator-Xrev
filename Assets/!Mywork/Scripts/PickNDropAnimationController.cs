using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using UnityEngine.SceneManagement;

public class PickNDropAnimationController : MonoBehaviour
{


    public static PickNDropAnimationController Instance;
    public bool isBusDoor_Open, isBusDoor_Close, isPick, isDrop;
    public PathManager PathContainer_Pick, PathContainer_Drop;
    public Transform ParkingCollider;
    public GameObject BusStaticCamera, DropPosition;
    


    [HideInInspector]
    public bool Is_Bus_Stopped;
    public GameObject SceneBUS;
    public GameObject TrafficHolder;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SceneBUS = this.gameObject;
        Debug.Log("PND BUS = " + this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (isBusDoor_Open == true)
        {
            ParkingCollider.GetComponent<BoxCollider>().enabled = false;


            BusStaticCamera.SetActive(true);
            //   GameManager.instance.door_btn.SetActive(false);
            StartCoroutine("Bus_Door_Open");
            isBusDoor_Open = false;
        }
        //if (isdoor_close_win == true)
        //{
        //    StartCoroutine("door_close_win");
        //}
        if (isBusDoor_Close == true)
        {
            StartCoroutine("door_close_wait");
        }
    }
    IEnumerator Bus_Door_Open()
    {
        yield return new WaitForSeconds(1.0f);
        this.gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("isdoor", true);
        BusController.Instance.Bus_Door_Sound.GetComponent<AudioSource>().Play();
        if (isPick == true)
        {
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<splineMove>().enabled = true;
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Pick;
            yield return new WaitForSeconds(1.5f);
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<splineMove>().enabled = true;
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Pick;
            yield return new WaitForSeconds(1.5f);
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<splineMove>().enabled = true;
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Pick;

            isPick = false;
           
        }
        else if (isDrop == true)
        {
            Debug.Log("check condition value level 2");
            ParkingCollider.transform.GetChild(0).transform.gameObject.SetActive(true);
            ParkingCollider.transform.GetChild(0).transform.position = DropPosition.transform.position;
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Drop;
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(0).transform.gameObject.GetComponent<splineMove>().enabled = true;

            yield return new WaitForSeconds(1.0f);
            ParkingCollider.transform.GetChild(1).transform.gameObject.SetActive(true);
            ParkingCollider.transform.GetChild(1).transform.position = DropPosition.transform.position;
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Drop;
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(1).transform.gameObject.GetComponent<splineMove>().enabled = true;

            yield return new WaitForSeconds(1.0f);
            ParkingCollider.transform.GetChild(2).transform.gameObject.SetActive(true);
            ParkingCollider.transform.GetChild(2).transform.position = DropPosition.transform.position;
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<splineMove>().pathContainer = PathContainer_Drop;
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<Animator>().SetBool("iswalk", true);
            ParkingCollider.transform.GetChild(2).transform.gameObject.GetComponent<splineMove>().enabled = true;


            yield return new WaitForSeconds(8.5f);
            //GameManager.instance.OnLevelComplete();

            //ParkingCollider.transform.GetChild(0).transform.gameObject.SetActive(false);
            //yield return new WaitForSeconds(1.5f);
            //ParkingCollider.transform.GetChild(1).transform.gameObject.SetActive(false);
            //yield return new WaitForSeconds(1.5f);
            //ParkingCollider.transform.GetChild(2).transform.gameObject.SetActive(false);
            //yield return new WaitForSeconds(1.5f);
           // GameManager.instance.OnLevelComplete();
            isDrop = false;
            isBusDoor_Close = true;
        }
    }
    IEnumerator door_close_wait()
    {
        isBusDoor_Close = false;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Animator>().SetBool("isdoor", false);
        BusController.Instance.Bus_Door_Sound.GetComponent<AudioSource>().Play();
        //bus_door_sound.SetActive(true);
        yield return new WaitForSeconds(2.5f);

        BusStaticCamera.SetActive(false);
        if (SceneManager.GetActiveScene().name == "CareerMode")
        {
            CarrierModeManager.Instance.RCC_Camera.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "ObstacleMode")
        {
            ObstacleModeManager.Instance.RCC_Camera.SetActive(true);
        }
        ParkingCollider.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        //bus_door_sound.SetActive(false);
        isPick = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        EventManager.ReachedBusStop(gameObject.transform);
        TrafficHolder.SetActive(true);
        // Bus_Cube.instance.count = 0;
    }
}
