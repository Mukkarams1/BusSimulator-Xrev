using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOnOff : MonoBehaviour
{
    public Slider slider;
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    public Image _image;

    public void Awake()
    {
        // _image = GetComponent<Image>();
        slider.wholeNumbers = true;
    }

    private void Start()
    {
        // Init based on Slider's value
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (slider.value == slider.minValue)
        {
            _image.sprite = disabledSprite;
            SoundsManager.Instance.ButtonClicked.GetComponent<AudioSource>().enabled = false;
            SoundsManager.Instance.ButtonClicked2.GetComponent<AudioSource>().enabled = false;
            Debug.Log("Sounds off");
        }
        else
        {
            _image.sprite = enabledSprite;
            SoundsManager.Instance.ButtonClicked.GetComponent<AudioSource>().enabled = true;
            SoundsManager.Instance.ButtonClicked2.GetComponent<AudioSource>().enabled = true;
            Debug.Log("Sounds onm");
        }
    }
}
