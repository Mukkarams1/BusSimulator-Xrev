using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GarageAnim : MonoBehaviour
{
        public GameObject trigger;
        public GameObject GarageDoor;
      //  public GameObject leftdoor;
        Animator GarageDoorAnim;
        //Animator rightanim;

        void Start()
        {
           // GarageDoorAnim = GarageDoor.GetComponent<Animator>();
            //rightanim = rightdoor.GetComponent<Animator>();
        }
        public void OnTriggerEnter(Collider coll)
        {
    //    Debug.Log("on trigger enter ho gya hay");
            if (coll.gameObject.tag == "Player")
            {
           //SlideDoors(true);
            GarageDoor.GetComponent<Animator>().enabled = false;

            GarageDoor.transform.DOLocalRotate(new Vector3(0, 0, -60), .5f);
          //mm  GarageDoor.transform.Rotate(0, 0, -220* Time.deltaTime * 10f);
            //GarageDoor.transform.Rotate(Vector3.up * Time.deltaTime);

            //GarageDoor.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10f);
           // Debug.Log("Animation play");
            }
 
             
        }
         public void OnTriggerExit(Collider coll)
         {
             if (coll.gameObject.tag == "Player")
             {
           // GarageDoor.transform.Rotate(0, 0, 220 * Time.deltaTime * 10f);
            GarageDoor.transform.DOLocalRotate(new Vector3(0, 0, 1), .5f);
            // GarageDoor.GetComponent<Animator>().enabled = true;

            //  SlideDoors(false);
         //   Debug.Log("Animation exit");
             }
         }
    void SlideDoors(bool state)
        {
            GarageDoorAnim.SetBool("slide", state);
            //rightanim.SetBool("slide", state);
        }


}