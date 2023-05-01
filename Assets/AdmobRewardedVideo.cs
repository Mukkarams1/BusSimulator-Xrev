using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class AdmobRewardedVideo : MonoBehaviour
{
    [SerializeField] int Index = 0;
    [SerializeField] Text coinsText;
    public bool isRewardDone = false;



    private void OnEnable()
    {
        isRewardDone = false;
    }

    #region Give Reward

    public void RewardAfterAd()// reward for rewarded video
    {
        if (!isRewardDone)
        {
            if (Index == 0)//for free cash
            {
                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + 1000);
                coinsText.text = PlayerPrefs.GetInt("Currency", 0).ToString();
            }
            else if (Index == 1)
            {
              //  if (UiManager.instance)
               // {
               //     UiManager.instance.SkipLevel();
               // }
              //  else
              //  {

               //     FindObjectOfType<UiManager>().SkipLevel();
              //  }
            }
            isRewardDone = true;
        }

    }


    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("...................reward "+ completed+"adv"+advertiser);

        if (completed == true)
        {

            if (Index == 0)//for free cash
            {
                PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency", 0) + 1000);
                coinsText.text = PlayerPrefs.GetInt("Currency", 0).ToString();
                Debug.Log("...................Deward index 1");
            }
            else if (Index == 1)
            {
               // if (UiManager.instance)
             //   {
              //      UiManager.instance.SkipLevel();
              //  }
              //  else
              //  {

               //     FindObjectOfType<UiManager>().SkipLevel();
               // }
            }
        }
        else
        {
            //no reward
        }
    }
    #endregion

    //public void ShowRewardedVideo()
    //{if(AdsManager.Instance)
    //    AdsManager.Instance.ShowRewardedVideo(this.GetComponent<AdmobRewardedVideo>());
    //}
    public void ShowRewardedVideo()
    {
        if (Advertisements.Instance)
        {
           if(Advertisements.Instance)
            Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

        }
    }
    
}