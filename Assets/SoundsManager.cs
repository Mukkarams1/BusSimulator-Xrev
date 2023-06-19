using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioSource MainMenuSound, ButtonClicked, ButtonClicked2;
    void Start()
    {
        if(!Instance)
        {
            Instance = this;
        }
        MainMenuSound.GetComponent<AudioSource>().Play();
        
    }
    public void ButtonClickedSound()
    {
        ButtonClicked.GetComponent<AudioSource>().Play();
    }
    public void ButtonClickedSound2()
    {
        ButtonClicked2.GetComponent<AudioSource>().Play();
    }
}
