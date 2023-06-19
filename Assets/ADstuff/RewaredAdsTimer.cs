using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewaredAdsTimer : MonoBehaviour
{
 

    public float timeRemaining;
    public Text timer,adloadingstatus;
    bool allowcheck;

    public delegate void RewardFunction();
    public static RewardFunction _rewarded;
    private void OnEnable()
    {
        //ADManager.Instance.HideBanner();
        Debug.Log(GameAppManager.instance.mediationhandler);
        GameAppManager.instance.mediationhandler.LoadRewardedVideo();
        allowcheck = true;
        timeRemaining = 8;
    }
    void Update()
    {
        if (allowcheck)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
               
                 adloadingstatus.text = string.Format("{0}", " AD WILL SHOWN IN :");
                timer.gameObject.SetActive(true);
                timer.text = string.Format("{0:0}", timeRemaining);
            }
            else
            {
                allowcheck = false;
                //ADManager.Instance.ShowBanner();
                if (GameAppManager.instance.mediationhandler.IsRewardedAdReady())
                {
                    GameAppManager.instance.mediationhandler.ShowRewardedVideo();
                    gameObject.SetActive(false);
                }
                else
                {

                    timer.gameObject.SetActive(false);
                    adloadingstatus.text = string.Format("{0}", "AD COULD NOT BE LOADED PLEASE TRAY AGAIN LATER");
                    Invoke("deactive", 3f);
                }
            }

        }
    }

    public void deactive()
    {
        gameObject.SetActive(false);

    }
    private void OnDisable()
    {
        timeRemaining = 8;

    }

}
