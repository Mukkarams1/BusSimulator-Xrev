using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    Button PlayBtn;
    [SerializeField]
    Button StartBtn;
    [SerializeField]
    GameObject ObjectivePAnel;
    [SerializeField]
    Slider AuidoSlider;
    [SerializeField]
    Slider MusicSlider;

    public GameObject LoadingBar;
    public Image LoadingBarFill;
    public float loadingTime;
    public GameObject BusCamera;
    public GameObject CameraPOS;



    #region Panels
    [SerializeField]
    GameObject GameModeSelectionPanel;
    [SerializeField]
    GameObject BusSelectionPanel;
    [SerializeField]
    GameObject levelSelectionPanel;
    #endregion

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

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
        BusCamera.transform.position = CameraPOS.transform.position;

        showSmallBanner();
        if(AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.LoadMediumBanner();
        }

        
        Invoke("loadinter", 1.5f);

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
        StartBtn.interactable = true;
    }
    public void Loading()
    {
        LoadingBar.SetActive(true);
        LoadingBarFill.fillAmount = 0;
        LoadingBarFill.DOFillAmount(1f, loadingTime);
        Invoke("HideLoading", loadingTime);
    }

    public void HideLoading()
    {
        LoadingBar.SetActive(false);
    }
    /// <summary>
    /// Ads
    /// </summary>
    /// 
    public void ShowAds()
    {
        hideSmallBanner();
        Loading();
        Invoke("ShowIntersitial", 2f);
        Invoke("ShowMediumBanner", 2.5f);
        Invoke("hideMediumBanner", 5f);
        Invoke("showSmallBanner", 5f);
    }
    public void showSmallBanner()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.ShowSmallBanner(GoogleMobileAds.Api.AdPosition.Top);
        }
    }
    public void ShowIntersitial()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.ShowInterstitial();
        }
    }
    public void ShowMediumBanner()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
            AdmobAdsManager.Instance.LoadInterstitial();
        }
    }
    public void hideSmallBanner()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.hideSmallBanner();
        }
    }
    public void hideMediumBanner()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.hideMediumBanner();
        }
    }
    public void loadinter()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.LoadInterstitial();
        }
    }


}
