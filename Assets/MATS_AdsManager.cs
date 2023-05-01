using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MATS_AdsManager : MonoBehaviour
{
    public static MATS_AdsManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        //if user consent was set, just initialize the SDK, else request user consent
        if (Advertisements.Instance)
        {
            Advertisements.Instance.SetCCPAConsent(true);
            Advertisements.Instance.SetUserConsent(true);
            Advertisements.Instance.Initialize();
        }
    }

    public void ShowBanner()
    {
        if (Advertisements.Instance)
        {
            Advertisements.Instance.ShowBanner(BannerPosition.TOP, bannerType: BannerType.Banner);
        }
    }
    public void HideBanner()
    {
        if (Advertisements.Instance)
        {
            Advertisements.Instance.HideBanner();

        }
    }
    public void ShowInterstitial()
    {
        if (Advertisements.Instance)
        {
            Advertisements.Instance.ShowInterstitial(InterstitialClosed);
        }

    }
    private void InterstitialClosed(string advertiser)
    {
        if (Advertisements.Instance.debug)
        {
            Debug.Log("Interstitial closed from: " + advertiser + " -> Resume Game ");
            GleyMobileAds.ScreenWriter.Write("Interstitial closed from: " + advertiser + " -> Resume Game ");
        }
    }
  


}
