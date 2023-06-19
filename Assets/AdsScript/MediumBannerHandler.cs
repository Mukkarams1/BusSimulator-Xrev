using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumBannerHandler : MonoBehaviour
{
    MediationHandler mediationHandler;

    private void Awake()
    {
        mediationHandler = FindObjectOfType<MediationHandler>();
    }

    private void OnEnable()
    {
        mediationHandler.ShowMbanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
    }

    private void OnDisable()
    {
        mediationHandler.hideMediumBanner();
    }
}
