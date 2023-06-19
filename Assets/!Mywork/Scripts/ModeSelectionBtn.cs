using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionBtn : MonoBehaviour
{
    public GameObject imageobj;

    public void SetImage(string imagename)
    {
        imageobj.GetComponent<Image>().sprite = Resources.Load<Sprite>(imagename);
    }
}
