using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Button PlayBtn;
    [SerializeField]
    Button StartBtn;
    [SerializeField]
    GameObject ObjectivePAnel;
    [SerializeField]
    Slider AuidoSlider;



    #region Panels
    [SerializeField]
    GameObject GameModeSelectionPanel;
    [SerializeField]
    GameObject BusSelectionPanel;
    [SerializeField]
    GameObject levelSelectionPanel;
    #endregion



    void Start()
    {
        PlayBtn.onClick.AddListener(ShowBusSelectionPanel);
        StartBtn.onClick.AddListener(OpenGameScene);
        AuidoSlider.onValueChanged.AddListener(GetVolumeSlider);

        float volume = PlayerPrefs.GetFloat("SFX");
        SetVolumeSlider(volume);

        EventManager.onLevelSelected += ShowStartBtn;
        EventManager.onBusSelected += ShowModeSelectionPanel;
        EventManager.onShowLevelSelectionPanel += ShowLevelSelectionPanel;

    }

    private void GetVolumeSlider(float arg0)
    {

        float SliderValue = arg0;
        PlayerPrefs.SetFloat("SFX", SliderValue);
        SetVolumeSlider(SliderValue);
    }

    private void OnDisable()
    {
        EventManager.onLevelSelected -= ShowStartBtn;
        EventManager.onBusSelected -= ShowModeSelectionPanel;
        EventManager.onShowLevelSelectionPanel -= ShowLevelSelectionPanel;


    }
    private void SetVolumeSlider(float voluime)
    {
        AuidoSlider.value = voluime;
        AudioListener.volume = voluime;
    }

    void ShowModeSelectionPanel(int model)
    {
        //PlayBtn.gameObject.SetActive(false);
        GameModeSelectionPanel.SetActive(true);
    }

    void ShowBusSelectionPanel()
    {
        BusSelectionPanel.gameObject.SetActive(true);
    }
    void ShowModeSelectBtn()
    {
     //   PlayBtn.gameObject.SetActive(false);
      //  modeSelectionBtn.gameObject.SetActive(true);   
    }
    void ShowLevelSelectionPanel()
    {
        GameModeSelectionPanel.SetActive(false);  
        levelSelectionPanel.SetActive(true);
    }
    void ShowStartBtn(int level)
    {
        levelSelectionPanel.gameObject.SetActive(false);
        ObjectivePAnel.gameObject.SetActive(true);
        StartBtn.gameObject.SetActive(true);
    }
    public void ShowPlayBtn()
    {
        PlayBtn.gameObject.SetActive(true);
    }

    void OpenGameScene()
    {
        StartBtn.interactable = false;
        LevelsDataManager.Instance.LoadScene();
        
        StartCoroutine(setIntrectative());
    }
    IEnumerator setIntrectative()
    {
        yield return new WaitForSeconds(2f);
        StartBtn.interactable |= true;
    }
    
}
