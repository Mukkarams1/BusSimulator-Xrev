//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
////using GoogleMobileAds.Api;
//using UnityEngine.Advertisements;
//using GameAnalyticsSDK;
//using UnityEngine.UI;
////public enum AdNetwork
////{
////    Unity,
////    GoogleMobileAds
////}
//public enum RewardType
//{
//    STORE_COINS = 0,
//    UNLOCK_NEXT_DAY= 1,
//    REVIVE = 2,
//    ADD_LEVEL_TIME = 3,
//    LEVEL_COMPLETE_2XCOINS = 4,
//    CLAIM_NEXT_DAY_DAILYREWARD= 5,
//    UNLOCK_NEXT_LEVEL = 6
//}
//[System.Serializable]
//public class UnityAdsIDs
//{
//    public bool testMode = false;
    
//    public string gameID = "3916091";
//    //public string gameID = "1486550";
    
//    public string InterstitialKey = "interstitialPlacement";
//    //public string VideoKey = "";
    
//    public string RewardedAdKey = "rewardedVideo";
//    //
    
//    public string BannerAdKey = "bannerPlacement";
//}
//[System.Serializable]
//public class AdmobAdsIDs
//{
//	public string InterstitionAdKey = "ca-app-pub-6263347419612757/1336303253";
//	public string RewardedAdKey = "ca-app-pub-6263347419612757/2102590018";
//	public string BannerAdKey = "ca-app-pub-6263347419612757/8181106729";
//}

//[System.Serializable]
//public class AdsPriorityManager
//{
//    public int firstAdCount;
//    public AdNetwork firstAdNetwork = AdNetwork.Admob;
//    public int secondAdCount;
//    public AdNetwork secondAdNetwork = AdNetwork.Unity;
//}

//public enum AdNetwork
//{
//    Unity,
//    Admob
//}

//public class AdsManager : MonoBehaviour, IUnityAdsListener
//{

//    public const string UnityTestGameID = "1486550";
//	public const string admobIadTestKey = "ca-app-pub-3940256099942544/1033173712";
//	public const string admobRadTestKey = "ca-app-pub-3940256099942544/5224354917";
//	public const string admobBadTestKey = "ca-app-pub-3940256099942544/6300978111";
//	//public AdsPriorityManager adsPriorityManager;


//	public UnityAdsIDs unityAdsKeys;
//	public AdmobAdsIDs admobAdsKeys;

//	//private InterstitialAd interstitial;
//	//private RewardedAd rewardedAd;
//	//[HideInInspector]
//	//public BannerView bannerView;
//	//private BannerView bannerViewMediumRectangle;

//	public static AdsManager Instance;
//	private static RewardUserDelegate NotifyReward;

//    public delegate void RewardUserDelegate();

//    public static RewardType rewardType;
//    //ShowOptions options = new ShowOptions();

//    //bool isRadPlaying;
//    bool isInterstitialLoading, isRewardedLoading;
//	//private void BindInterstitialEvents()
//	//{
//	//	// Called when an ad request has successfully loaded.
//	//	this.interstitial.OnAdLoaded += HandleOnIAdLoaded;
//	//	// Called when an ad request failed to load.
//	//	this.interstitial.OnAdFailedToLoad += HandleOnIAdFailedToLoad;
//	//	// Called when an ad is shown.
//	//	this.interstitial.OnAdOpening += HandleOnIAdOpened;
//	//	// Called when the ad is closed.
//	//	this.interstitial.OnAdClosed += HandleOnIAdClosed;
//	//	// Called when the ad click caused the user to leave the application.
//	//	this.interstitial.OnAdLeavingApplication += HandleOnIAdLeavingApplication;
//	//}
//	//private void BindRewardedEvents()
//	//{
//	//	// Called when an ad request has successfully loaded.
//	//	this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//	//	// Called when an ad request failed to load.
//	//	this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//	//	// Called when an ad is shown.
//	//	this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//	//	// Called when an ad request failed to show.
//	//	this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//	//	// Called when the user should be rewarded for interacting with the ad.
//	//	this.rewardedAd.OnUserEarnedReward += HandleUserEarnedRewardAdmob;
//	//	// Called when the ad is closed.
//	//	this.rewardedAd.OnAdClosed += HandleAdmobRewardedAdClosed;
//	//}
//	//private void BindBannerEvents()
//	//{
//	//	// Called when an ad request has successfully loaded.
//	//	this.bannerView.OnAdLoaded += this.HandleOnSmallBannerAdLoaded;
//	//	// Called when an ad request failed to load.
//	//	this.bannerView.OnAdFailedToLoad += this.HandleOnSmallBannerAdFailedToLoad;

//	//}
//	//private void BindMediumBannerEvents()
//	//{
//	//	// Called when an ad request has successfully loaded.
//	//	this.bannerViewMediumRectangle.OnAdLoaded += this.HandleOnMediumBannerAdLoaded;
//	//	// Called when an ad request failed to load.
//	//	this.bannerViewMediumRectangle.OnAdFailedToLoad += this.HandleOnMediumBannerAdFailedToLoad;

//	//}
//	//private void BindAdsEvents()
//	//{
//	//	BindInterstitialEvents();
//	//	BindRewardedEvents();
//	//	BindBannerEvents();
//	//}
//	//private void CreateAdsObjects()
//	//{
//	//	this.interstitial = new InterstitialAd(admobAdsKeys.InterstitionAdKey);
//	//	this.rewardedAd = new RewardedAd(admobAdsKeys.RewardedAdKey);
//	//}
//	#region AdmobAdsCallbacks
//	// Small Banner Ads Events
//	//public void HandleOnSmallBannerAdLoaded(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:Banner:Loaded");
//	//}

//	//public void HandleOnSmallBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:BannerLoadError: "
//	//						+ args.Message);
//	//}
//	//// Medium Banner Ads Events
//	//public void HandleOnMediumBannerAdLoaded(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:MediumBanner:Loaded");
//	//}

//	//public void HandleOnMediumBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:MediumBannerLoadError: "
//	//						+ args.Message);
//	//}

//	//// Interstitial Ads Events
//	//private void HandleOnIAdLoaded(object sender, EventArgs args)
//	//{
//	//	isInterstitialLoading = false;
//	//	Debug.Log("GG >> Admob:iad:Loaded");
//	//}

//	//private void HandleOnIAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//	//{
//	//	isInterstitialLoading = false;
//	//	Debug.Log("GG >> Admob:iad:NoInventory :: " + args.Message);
//	//}

//	//private void HandleOnIAdOpened(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:iad:Displayed");
//	//}

//	//private void HandleOnIAdClosed(object sender, EventArgs args)
//	//{
//	//	//this.interstitial.Destroy();
//	//	isInterstitialLoading = false;
//	//	Debug.Log("GG >> Admob:iad:Closed");
//	//	LoadInterstitial();
//	//}

//	//private void HandleOnIAdLeavingApplication(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:iad:Clicked");
//	//}

//	//// Rewarded Ads Events
//	//public void HandleRewardedAdLoaded(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:rad:Loaded");
//	//	isRewardedLoading = false;
//	//}

//	//public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:rad:LoadError: " + args.Message);
//	//	isRewardedLoading = false;
//	//}

//	//public void HandleRewardedAdOpening(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:rad:Displaying");
//	//}

//	//public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:rad:ShowError: " + args.Message);
//	//}

//	//public void HandleAdmobRewardedAdClosed(object sender, EventArgs args)
//	//{
//	//	Debug.Log("GG >> Admob:rad:Closed");
//	//	LoadRewardedVideo();
//	//}


//	//public void HandleUserEarnedRewardAdmob(object sender, Reward args)
//	//{
//	//	if (PreferenceManager.GetRewardStatus() == 0)
//	//	{
//	//		Debug.Log("GG >> STORE_COINS has been rewarded");
//	//		Toolbox.DB.Prefs.GoldCoins += Constants.rewardedVideo_StoreCoinReward;
//	//	}
//	//	else if (PreferenceManager.GetRewardStatus() == 1)
//	//	{
//	//		Debug.Log("GG >> UNLOCK_NEXT_DAY has been rewarded");
//	//	}
//	//	else if (PreferenceManager.GetRewardStatus() == 2)
//	//	{
//	//		Debug.Log("GG >> REVIVE has been rewarded");
//	//	}
//	//	else if (PreferenceManager.GetRewardStatus() == 3)
//	//	{
//	//		Debug.Log("GG >> ADD_LEVEL_TIME has been rewarded");
//	//	}
//	//	else if (PreferenceManager.GetRewardStatus() == 4)
//	//	{
//	//		Debug.Log("GG >> LEVEL_COMPLETE_2XCOINS has been rewarded");
// //           LevelCompleteListner.instance.Reward2xCoins();
// //           //HandleRewardsDoubleCoins();
// //           //Toolbox.DB.Prefs.GoldCoins += Constants.rewardedVideo_StoreCoinReward;
// //       }
//	//	else if (PreferenceManager.GetRewardStatus() == 5)
//	//	{
//	//		Debug.Log("GG >> CLAIM_NEXT_DAY_DAILYREWARD has been rewarded");
//	//		if (FindObjectOfType<DailyRewardListner>())
//	//			FindObjectOfType<DailyRewardListner>().RewardPlayerHandling();
//	//		Toolbox.GameManager.Log("Unlock All Levels");

//	//		Toolbox.DB.Prefs.UnlockAllLevels();

//	//		if (FindObjectOfType<LevelSelectionListner>())
//	//			FindObjectOfType<LevelSelectionListner>().RefreshView();
//	//	}
//	//	else if (PreferenceManager.GetRewardStatus() == 6)
//	//	{
//	//		Debug.Log("GG >> UNLOCK_NEXT_LEVEL has been rewarded");
//	//	}
//	//	Debug.Log("GG >> Admob:rad:RewardGiven");
//	//	Time.timeScale = 1;
//	//}
//	#endregion
//	private void Awake()
//    {
//        GameAnalytics.Initialize();

//		if (unityAdsKeys.testMode)
//		{
//			admobAdsKeys.BannerAdKey = admobBadTestKey;
//			admobAdsKeys.InterstitionAdKey = admobIadTestKey;
//			admobAdsKeys.RewardedAdKey = admobRadTestKey;
//		}
//	}
//    void Start()
//    {
//		Instance = this;
//        DontDestroyOnLoad(this.gameObject);
//        if (!PreferenceManager.GetAdsStatus())
//        {

//            return;
//        }
//           // Initialize();
//            Advertisement.AddListener(this);
//            Advertisement.Initialize(unityAdsKeys.gameID, unityAdsKeys.testMode);
//        //Debug.Log("Admob banner key is: " + admobAdsKeys.BannerAdKey);


//        //Debug.Log(admobAdsKeys.BannerAdKey);
//        //Debug.Log(admobAdsKeys.InterstitionAdKey);
//        //Debug.Log(admobAdsKeys.RewardedAdKey);
//    }

//   // bool isInitialized = false;
//    public void Initialize()
//    {
//		Debug.Log("GG >> Admob:Initializing...");

//		//MobileAds.Initialize(initStatus =>
//		//{

//		//	Debug.Log("GG >> Admob:Initialized");
//		//	//CreateAdsObjects();
//		//	//BindAdsEvents();
//		//	//isInitialized = true;
//		//	LoadInterstitial();
//		//	//LoadRewardedVideo();
//		//	LoadBanner(AdSize.Banner, AdPosition.Top);
//		//	LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.TopLeft);
//		//});
//	}
//	//#region AdmobBannerIntegration
//	//public void LoadBannerEvent()
//	//{
//	//	LoadBanner(AdSize.Banner, AdPosition.Top);
//	//}
//	//public void ShowBannerEvent(BannerPosition unityBannerPos)
// //   {
// //       if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
// //           return;
// //       ShowSmallAdmobBanner(AdSize.Banner, AdPosition.Top, unityBannerPos);
// //   }
//	//public void LoadBanner(AdSize adsize, AdPosition adPos)
//	//{
// //       if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
// //           return;

// //       Debug.Log("GG >> Admob:bad:small:request");
//	//	this.bannerView = new BannerView(admobAdsKeys.BannerAdKey, adsize, adPos);
//	//	BindBannerEvents();
//	//	// Create an empty ad request.
//	//	AdRequest request = new AdRequest.Builder().Build();

//	//	// Load the banner with the request.
//	//	this.bannerView.LoadAd(request);
//	//	this.bannerView.Hide();

//	//}

//	//public void LoadMediumRectangleBanner(AdSize adSize, AdPosition adPos)
//	//{
// //       if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
// //           return;
// //       Debug.Log("GG >> Admob:bad:medium:request");
//	//	this.bannerViewMediumRectangle = new BannerView(admobAdsKeys.BannerAdKey, adSize, adPos);
//	//	BindMediumBannerEvents();
//	//	// Create an empty ad request.
//	//	AdRequest request = new AdRequest.Builder().Build();

//	//	// Load the banner with the request.
//	//	this.bannerViewMediumRectangle.LoadAd(request);
//	//	this.bannerViewMediumRectangle.Hide();
//	//}

//	//private void ShowSmallAdmobBanner(AdSize _size, AdPosition _pos, BannerPosition unityBannerPos)
//	//{
		
//	//	if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//	//	{
//	//		return;
//	//	}
// //       try
// //       {
// //           HideBannerAd();
// //           if (this.bannerView != null)
// //           {
// //               //Debug.Log("printing currnt banner pos: " + _pos);
// //               this.bannerView.SetPosition(_pos);
// //               this.bannerView.Show();
// //               //Debug.Log("GG >> Admob:Banner:Small:Show");
// //               Toolbox.GameManager.Analytics_DesignEvent("Admob_SmallBanner_Shown");
// //           }
// //           else
// //           {
// //               Debug.Log("GG >> Admob:Banner:Small:NotAvalaible");
// //               if (Advertisement.IsReady(unityAdsKeys.BannerAdKey))
// //               {

// //                   // Debug.Log("GG >> Unity:Banner:Show");
// //                   Advertisement.Banner.SetPosition(unityBannerPos);
// //                   Advertisement.Banner.Show(unityAdsKeys.BannerAdKey);
// //                   Toolbox.GameManager.Analytics_DesignEvent("Unity_SmallBanner_Shown");
// //               }
// //               else
// //               {
// //                   Debug.Log("GG >> Unity:Banner:NotAvailable");
// //                   LoadBanner(AdSize.Banner, AdPosition.Top);
// //               }
// //           }
// //       }
//	//	catch (Exception)
//	//	{
// //           Debug.Log("Admob Small Banner Instance Not Found Exception");
//	//	}
//	//}

//	//public void ShowMediumBanner(AdSize _size, AdPosition _pos)
// //   {

        
//	//	if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//	//	{
//	//		return;
//	//	}
// //       try
// //       {
// //           HideBannerAd();
// //           if (this.bannerViewMediumRectangle != null)
// //           {
// //               this.bannerViewMediumRectangle.Show();
// //               Debug.Log("GG >> Admob:Banner:Medium:ShowCall");
// //               Toolbox.GameManager.Analytics_DesignEvent("Admob_Medium Banner_Shown");
// //           }
// //           else
// //           {
// //               Debug.Log("GG >> Admob:Banner:Medium:NotAvalaible");
// //               //LoadMediumRectangleBanner(AdSize.MediumRectangle, AdPosition.TopLeft);
// //           }
// //       }
//	//	catch (Exception)
//	//	{
// //           Debug.Log("Admob Medium Banner Instance Not Found Exception");
//	//	}
//	//}
//    public void HideBannerAd()
//    {
//        //if (throwLog)
        
//            //Debug.Log("GG >> Unity:Banner:Hide");

        
//		//if (this.bannerView != null)
//		//{
//		//	this.bannerView.Hide();
//		//}
//		//if (this.bannerViewMediumRectangle != null)
//		//{
//		//	this.bannerViewMediumRectangle.Hide();
//		//}
//		Advertisement.Banner.Hide();
//    }
//	//#endregion

//	#region AdmobInterstitialIntegration
//	//public void LoadInterstitial()
//	//{
// //       if (Application.internetReachability == NetworkReachability.NotReachable)
// //           return;
// //       Debug.Log("GG >> Admob:iad:loading:" + isInterstitialLoading);
// //       if (!PreferenceManager.GetAdsStatus() || isInterstitialLoading )
//	//		return;

        
// //       this.interstitial = new InterstitialAd(admobAdsKeys.InterstitionAdKey);
//	//	BindInterstitialEvents();

//	//	Debug.Log("GG >> Admob:iad:LoadRequest");
//	//	// Create an empty ad request.
//	//	AdRequest request = new AdRequest.Builder().Build();
//	//	// Load the interstitial with the request.
//	//	this.interstitial.LoadAd(request);
//	//	isInterstitialLoading = true;
//	//}
//    // <summary>
//    // Check is iAd already loaded
//    // </summary>
// //   public bool IsAdmobInterstitialAdReady()
//	//{
//	//	return this.interstitial.IsLoaded();
//	//}
//    // <summary>
//    // Show Interstitial Ad
//    // </summary>
//    private void ShowAdmobInterstitial()
//	{
//		if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//			return;
//		//this.interstitial.Show();
//		Toolbox.GameManager.Analytics_DesignEvent("Admob_Interstitial Shown");
//	}

//	#endregion

//	#region AdmobRewardedIntegration

//	public void LoadRewardedVideo()
//    {
//        if (Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//        Debug.Log("GG >> Admob:rad:loading:" + isRewardedLoading);
//        if (isRewardedLoading)
//		{
//            return;
//		}
//		//if (IsRewardedAdReady() /*|| isRadPlaying*/)
//		//{
//		//    Debug.Log("GG >> Admob:rad:AlreadyLoaded");
//		//    return;
//		//}

//		//this.rewardedAd = new RewardedAd(admobAdsKeys.RewardedAdKey);
//		//BindRewardedEvents();
//		//Debug.Log("GG >> Admob:rad:LoadRequest");
//		//// Create an empty ad request.
//		//AdRequest request = new AdRequest.Builder().Build();
//		//// Load the rewarded video ad with the request.
//		//this.rewardedAd.LoadAd(request);
//		//isRewardedLoading = true;
//	}
    
//    /// <summary>
//    /// Check is rAd already loaded
//    /// </summary>
//    public bool IsRewardedAdReady()
//    {
//        //if (this.rewardedAd != null)
//        //{
//        //    return this.rewardedAd.IsLoaded();
//        //}
            
//        //else 
//        {
//            return Advertisement.IsReady(unityAdsKeys.RewardedAdKey);
//        }
            
//    }


//	/// <summary>
//	/// Show Rewarded Ad
//	/// </summary>
//	/// 
//	//private void ShowAdmobRewarded()
//	//{
//	//	if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//	//		return;
//	//	//this.rewardedAd.Show();
//	//}

//	//private void ShowAdmobRewardedVideo(RewardType _reward)
//	//{
//	//	if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//	//		return;


//	//	Debug.Log("GG >> Admob:rad:WillDisplay");
//	//	//        Time.timeScale = 0;
//	//	//#if UNITY_EDITOR
//	//	//        Time.timeScale = 1;
//	//	//#endif
//	//	PreferenceManager.SetRewardStatus(_reward);
//	//	//this.rewardedAd.Show();

//	//  }
//		#endregion

//		#region UnityInterstitialIntegration
//		public void ShowUnityInterstitialAd()
//    {
//        if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//        if (Advertisement.IsReady())
//        {
//            Advertisement.Show();
//            Debug.Log("GG >> UnityAds:iAd:ShowCall");
//            Toolbox.GameManager.Analytics_DesignEvent("Unity_Interstitial Shown");
//        }
//        else
//        {
//            Debug.Log("GG >> UnityAds:iAd:NotAvailable");
//        }
//    }
//    #endregion

//    #region UnityBannerIntegration
//    private void ShowUnityBannerAd(UnityEngine.Advertisements.BannerPosition unityBannerPos)
//    {
//		if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//			return;
//		StartCoroutine(ShowBannerWhenInitialized(unityBannerPos));
//    }

//    private IEnumerator ShowBannerWhenInitialized(UnityEngine.Advertisements.BannerPosition unityBannerPos)
//    {
//        while (!Advertisement.isInitialized)
//        {
//            Debug.Log("GG >> UnityAds:Banner:WaitingToLoad");
//            yield return new WaitForSeconds(0.5f);
//        }
//        Advertisement.Banner.SetPosition( unityBannerPos);
//        Advertisement.Banner.Show(unityAdsKeys.BannerAdKey);
        
//        Debug.Log("GG >> UnityAds:Banner:ShowCall");
//    }
   
//    #endregion

//    #region UnityRewardedIntegration
   
//    public void ShowUnityRewardedAd(RewardType reward)
//    {
//        //Debug.LogError(unityAdsKeys.RewardedAdKey);
//        if ( Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//        Debug.Log("GG >> UnityAds:Rad:ShowCall");
//        if (Advertisement.IsReady(unityAdsKeys.RewardedAdKey))
//        {
//            PreferenceManager.SetRewardStatus(reward);
//            Debug.Log("GG >> UnityAds:Rad:WillDisplay");
//            Advertisement.Show(unityAdsKeys.RewardedAdKey);
//            Toolbox.GameManager.Analytics_DesignEvent("Unity_RewardedAd_Shown");
//        }
//        else
//            Debug.Log("GG >> UnityAds:RAd:NotAvailable");
//    }


//    // Implement IUnityAdsListener interface methods:
//    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//    {
//        if (placementId == unityAdsKeys.RewardedAdKey)
//        {
//            // Define conditional logic for each ad completion status:
//            if (showResult == ShowResult.Finished)
//            {
//                if (PreferenceManager.GetRewardStatus() == 0)
//                {
//                    Debug.Log("GG >> STORE_COINS has been rewarded");
//                    Toolbox.DB.Prefs.GoldCoins += Constants.rewardedVideo_StoreCoinReward;
//                }
//                else if (PreferenceManager.GetRewardStatus() == 1)
//                {
//                    Debug.Log("GG >> UNLOCK_NEXT_DAY has been rewarded");
//                }
//                else if (PreferenceManager.GetRewardStatus() == 2)
//                {
//                    Debug.Log("GG >> REVIVE has been rewarded");
//                }
//                else if (PreferenceManager.GetRewardStatus() == 3)
//                {
//                    Debug.Log("GG >> ADD_LEVEL_TIME has been rewarded");
//                }
//                else if (PreferenceManager.GetRewardStatus() == 4)
//                {
//                    Debug.Log("GG >> LEVEL_COMPLETE_2XCOINS has been rewarded");
//                    LevelCompleteListner.instance.Reward2xCoins();

//                    //HandleRewardsDoubleCoins();
//                    // Toolbox.DB.Prefs.GoldCoins += Constants.rewardedVideo_StoreCoinReward;
//                }
//                else if (PreferenceManager.GetRewardStatus() == 5)
//                {
//                    Debug.Log("GG >> CLAIM_NEXT_DAY_DAILYREWARD has been rewarded");
//                    //if (FindObjectOfType<DailyRewardListner>())
//                    //    FindObjectOfType<DailyRewardListner>().RewardPlayerHandling();

//                }
//                else if (PreferenceManager.GetRewardStatus() == 6)
//                {
//                    Debug.Log("GG >> UNLOCK_NEXT_LEVEL has been rewarded");
//                }
//                Debug.Log("GG >> UnityAds:rAd:RewardGiven");
//                // Reward the user for watching the ad to completion.
//            }
//            else if (showResult == ShowResult.Skipped)
//            {
//                Debug.Log("GG >> UnityAds:rAd:Skipped");
//                // Do not reward the user for skipping the ad.
//            }
//            else if (showResult == ShowResult.Failed)
//            {
//                Debug.LogWarning("GG >> UnityAds:rAd:The ad did not finish due to an error.");
//            }
//        }
//        if (placementId == unityAdsKeys.InterstitialKey)
//        {
//            if (showResult == ShowResult.Finished)
//            {
//                Debug.Log("GG >> UnityAds:iAd:Displayed");
//            }
//            else if (showResult == ShowResult.Failed)
//            {
//                Debug.LogWarning("GG >> UnityAds:iAd:The ad did not finish due to an error.");
//            }
//        }
//        if(placementId == unityAdsKeys.BannerAdKey)
//		{
//            if(showResult == ShowResult.Finished)
//			{
//                Debug.Log("Unity:bAd:Displayed");
//            }
//            else if (showResult == ShowResult.Failed)
//            {
//                Debug.LogWarning("GG >> UnityAds:bAd:The ad did not finish due to an error.");
//            }
//        }
//    }
//    public void OnUnityAdsReady(string placementId)
//    {
//        if (!PreferenceManager.GetAdsStatus())
//            return;
//        // If the ready Placement is rewarded, show the ad:
//        if (placementId == unityAdsKeys.RewardedAdKey)
//        {
//            Debug.Log("GG >> UnityAds:rAd:Loaded");
//            //Advertisement.Show(unityAdsKeys.RewardedAdKey);
//            // Show Button to play rewarded video
//        }
//        if (placementId == unityAdsKeys.InterstitialKey)
//        {
//            Debug.Log("GG >> UnityAds:iAd:Loaded");
//        }
//        if (placementId == unityAdsKeys.BannerAdKey)
//        {
//            Debug.Log("GG >> UnityAds:bAd:Loaded");
//        }
//    }

//    public void OnUnityAdsDidError(string message)
//    {
//        // Log the error.
//    }

//    public void OnUnityAdsDidStart(string placementId)
//    {
//        // Optional actions to take when the end-users triggers an ad.
//        if (placementId == unityAdsKeys.RewardedAdKey)
//            Debug.Log("GG >> UnityAds:rAd:Displaying");
//        else if (placementId == unityAdsKeys.InterstitialKey)
//            Debug.Log("GG >> UnityAds:iAd:Displaying");
//        else if (placementId == unityAdsKeys.BannerAdKey)
//            Debug.Log("GG >> UnityAds:bAd:Displaying");

//    }

//    //public GameObject DoubleCoinsPanel, doubleRewardCoinsTxt;
//    private void HandleRewardsDoubleCoins()
//    {
//      //  DoubleCoinsPanel.SetActive(true);
//      //// int  timeBonus = Mathf.RoundToInt(Toolbox.HUDListner.Get_Time()); //Should be changed to proper implementation or different attribute
//      // int stuntsBonus = Toolbox.GameplayController.LevelReward;
//      // int totalCoins = Mathf.RoundToInt(Toolbox.HUDListner.Get_Time() + stuntsBonus);

//      //  //timeBonusTxt.text = timeBonus.ToString();
//      //  //stuntsBonusTxt.text = Mathf.RoundToInt(stuntsBonus).ToString();
//      //  //totalCoinsTxt.text = Mathf.RoundToInt(totalCoins).ToString();

//      //  doubleRewardCoinsTxt.GetComponent<Text>().text = Mathf.RoundToInt(totalCoins * 2).ToString();

//      //  int coinsReward = Mathf.RoundToInt(totalCoins);
//      //  Toolbox.DB.Prefs.GoldCoins += coinsReward;
//      //  //StartCoroutine(CR_CoinsAnimation());
//    }


   
//    #endregion


//    public void ShowUnityOnly()
//	{
//        ShowUnityInterstitialAd();
//    }

//    public void ShowInterstitialAd()
//    {
//        if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//		Debug.Log("GG >> iad:ShowCall");
//		try
//		{
//            //if (this.interstitial == null)
//            //{
//            //    Debug.Log("GG >> Admob Interstitial Instance Not Found! Check Unity Ads");
//            //    ShowUnityInterstitialAd();
//            //    LoadInterstitial();
//            //}
//            //else
//            //{
//            //    Debug.Log("GG >> Admob Interstitial Instance Found! Check Availability");
//            //    if (this.interstitial.IsLoaded())
//            //    {
//            //        ShowAdmobInterstitial();
//            //    }
//            //    else
//                {
//                    Debug.Log("GG >> CheckUnityAd");
//                    ShowUnityInterstitialAd();
//                }
//            //}
//        }
//		catch (Exception)
//		{

//            Debug.Log("Unity Interstitial Not Found Exception");
//		}
        
//    }

//	public void ShowSmallBanner(UnityEngine.Advertisements.BannerPosition unityBannerPos)
//	{

//		if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//			return;
//		try
//		{
//			HideBannerAd();
//			Debug.Log("GG >> Bad:ShowCall");
//			//if (bannerView != null)
//			//{
//			//    //  Debug.Log("Going to show small banner at: " + _pos);
//			//    ShowSmallAdmobBanner(_size, _pos, unityBannerPos);
//			//}
//			//else
//			{
//				//Debug.Log("GG >> Admob:bad:NotLoaded:CheckUnitybAd");
//				//LoadBanner(_size, _pos);
//				if (Advertisement.IsReady(unityAdsKeys.BannerAdKey))
//				{
//					ShowUnityBannerAd(unityBannerPos);
//				}
//				else
//				{
//					Debug.Log("GG >> UnityAds:bad:NotLoaded");
//				}
//			}
//		}
//		catch (Exception)
//		{
//			Debug.Log("Unity Small Banner Not Found Exception");
//		}
//	}

//	public void ShowRewardedAd(RewardType reward)
//    {
//        if ( Application.internetReachability == NetworkReachability.NotReachable)
//            return;
//		//Debug.Log("GG >> Admob:rAd:ShowCall");
//		//if (this.rewardedAd.IsLoaded())
//		//{
//		//	ShowAdmobRewardedVideo(reward);
//		//	Toolbox.GameManager.Analytics_DesignEvent("Admob_RewardedAD_Shown");
//		//}
//		//else
//		{
//            Debug.Log("GG >> Admob:rAd:NotLoaded:CheckUnityRad");
//            //LoadRewardedVideo();
            
//            ShowUnityRewardedAd(reward);
            
//        }
//    }

//    #region priority

//    //public void ShowInterstitialAdWithCustomPriority()
//    //{
//    //    if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//    //        return;
//    //    if (adsPriorityManager.firstAdCount <= 0 && adsPriorityManager.secondAdCount <= 0)
//    //    {
//    //        Debug.LogError("GG >> Frequency for all Ads Networks are set 0");
//    //        return;
//    //    }
//    //    if (AdsConstants.firstiAdCounter < adsPriorityManager.firstAdCount)
//    //    {
//    //        AdsConstants.firstiAdCounter++;
//    //        Debug.Log("GG >> " + adsPriorityManager.firstAdNetwork.ToString() + " iAds Displayed: " + AdsConstants.firstiAdCounter + "/" + adsPriorityManager.firstAdCount);

//    //        if (adsPriorityManager.firstAdNetwork == AdNetwork.Admob)
//    //        {
//    //            Debug.Log("GG >> Admob:iad:ShowCall");
//    //            if (this.interstitial.IsLoaded())
//    //            {
//    //                Debug.Log("GG >> Admob:iad:WillDisplay");
//    //                ShowAdmobInterstitial();
//    //            }
//    //            else
//    //            {
//    //                Debug.Log("GG >> Admob:iad:NotAvailable:CheckUnityAd");
//    //                ShowUnityInterstitialAd();
//    //            }
//    //        }
//    //        else if (adsPriorityManager.firstAdNetwork == AdNetwork.Unity)
//    //        {
//    //            Debug.Log("GG >> UnityAds:iAd:ShowCall");
//    //            if (Advertisement.IsReady())
//    //            {
//    //                Advertisement.Show();
//    //                Debug.Log("GG >> UnityAds:iAd:WillDisplay");
//    //            }
//    //            else
//    //            {
//    //                Debug.Log("GG >> UnityAds:iAd:NotAvailable:CheckAdmob");
//    //                Debug.Log("GG >> Admob:iad:ShowCall");
//    //                if (this.interstitial.IsLoaded())
//    //                {
//    //                    Debug.Log("GG >> UnityAds:iAd:WillDisplay");
//    //                    ShowAdmobInterstitial();
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> Admob:iad:NotAvailable");
//    //                }
//    //            }
//    //        }
//    //    }
//    //    else
//    //    {
//    //        if (AdsConstants.secondiAdCounter < adsPriorityManager.secondAdCount)
//    //        {
//    //            AdsConstants.secondiAdCounter++;
//    //            Debug.Log("GG >> " + adsPriorityManager.secondAdNetwork.ToString() + " iAds Displayed: " + AdsConstants.secondiAdCounter + "/" + adsPriorityManager.secondAdCount);

//    //            if (adsPriorityManager.secondAdNetwork == AdNetwork.Unity)
//    //            {
//    //                Debug.Log("GG >> UnityAds:iAd:ShowCall");
//    //                if (Advertisement.IsReady())
//    //                {
//    //                    Advertisement.Show();
//    //                    Debug.Log("GG >> UnityAds:iAd:WillDisplay");
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> UnityAds:iAd:NotAvailable:CheckAdmob");
//    //                    if (this.interstitial.IsLoaded())
//    //                    {
//    //                        Debug.Log("GG >> UnityAds:iAd:WillDisplay");
//    //                        ShowAdmobInterstitial();
//    //                    }
//    //                    else
//    //                    {
//    //                        Debug.Log("GG >> Admob:iad:NotAvailable");
//    //                    }
//    //                }
//    //            }
//    //            else if (adsPriorityManager.secondAdNetwork == AdNetwork.Admob)
//    //            {
//    //                if (this.interstitial.IsLoaded())
//    //                {
//    //                    ShowAdmobInterstitial();
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> Admob:iad:NotAvailable:CheckUnityAd");
//    //                    ShowUnityInterstitialAd();
//    //                }
//    //            }
//    //        }
//    //        else
//    //        {
//    //            ResetiAdPriorityCounters();
//    //            ShowInterstitialAdWithCustomPriority();
//    //        }
//    //    }
//    //}



//    //public void ShowRewardedAdWithCustomPriority(RewardType _reward)
//    //{
//    //    if (!PreferenceManager.GetAdsStatus() || Application.internetReachability == NetworkReachability.NotReachable)
//    //        return;
//    //    if (adsPriorityManager.firstAdCount <= 0 && adsPriorityManager.secondAdCount <= 0)
//    //    {
//    //        Debug.LogError("GG >> Frequency for all Ads Networks are set 0");
//    //        return;
//    //    }
//    //    if (AdsConstants.firstrAdCounter < adsPriorityManager.firstAdCount)
//    //    {
//    //        AdsConstants.firstrAdCounter++;
//    //        Debug.Log("GG >> " + adsPriorityManager.firstAdNetwork.ToString() + " rAds Displayed: " + AdsConstants.firstrAdCounter + "/" + adsPriorityManager.firstAdCount);
//    //        if (adsPriorityManager.firstAdNetwork == AdNetwork.Admob)
//    //        {
//    //            if (this.rewardedAd.IsLoaded())
//    //            {
//    //                ShowAdmobRewardedVideo(_reward);
//    //            }
//    //            else
//    //            {
//    //                Debug.Log("GG >> Admob:rAd:NotLoaded:CheckUnityRad");
//    //                LoadRewardedVideo();

//    //                ShowUnityRewardedAd(_reward);
//    //            }
//    //        }
//    //        else if (adsPriorityManager.firstAdNetwork == AdNetwork.Unity)
//    //        {
//    //            Debug.Log("GG >> UnityAds:Rad:ShowCall");
//    //            if (Advertisement.IsReady(unityAdsKeys.RewardedAdKey))
//    //            {
//    //                PreferenceManager.SetRewardStatus(_reward);
//    //                Debug.Log("GG >> UnityAds:Rad:WillDisplay");
//    //                Advertisement.Show(unityAdsKeys.RewardedAdKey);
//    //            }
//    //            else
//    //            {
//    //                Debug.Log("GG >> UnityAds:RAd:NotAvailable:CheckAdmob");
//    //                if (this.rewardedAd.IsLoaded())
//    //                {
//    //                    Debug.Log("GG >> Admob:rad:WillDisplay");
//    //                    ShowAdmobRewardedVideo(_reward);
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> Admob:rad:NotAvailable");
//    //                }
//    //            }
//    //        }
//    //    }
//    //    else
//    //    {
//    //        if (AdsConstants.secondrAdCounter < adsPriorityManager.secondAdCount)
//    //        {
//    //            AdsConstants.secondrAdCounter++;
//    //            Debug.Log("GG >> " + adsPriorityManager.secondAdNetwork.ToString() + " rAds Displayed: " + AdsConstants.secondrAdCounter + "/" + adsPriorityManager.secondAdCount);
//    //            if (adsPriorityManager.secondAdNetwork == AdNetwork.Unity)
//    //            {
//    //                Debug.Log("GG >> UnityAds:Rad:ShowCall");
//    //                if (Advertisement.IsReady(unityAdsKeys.RewardedAdKey))
//    //                {
//    //                    PreferenceManager.SetRewardStatus(_reward);
//    //                    Debug.Log("GG >> UnityAds:Rad:WillDisplay");
//    //                    Advertisement.Show(unityAdsKeys.RewardedAdKey);
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> UnityAds:RAd:NotAvailable:CheckAdmob");
//    //                    if (this.rewardedAd.IsLoaded())
//    //                    {
//    //                        Debug.Log("GG >> Admob:rad:WillDisplay");
//    //                        ShowAdmobRewardedVideo(_reward);
//    //                    }
//    //                    else
//    //                    {
//    //                        Debug.Log("GG >> Admob:rad:NotAvailable");
//    //                    }
//    //                }
//    //            }
//    //            else if (adsPriorityManager.secondAdNetwork == AdNetwork.Admob)
//    //            {
//    //                if (this.rewardedAd.IsLoaded())
//    //                {
//    //                    ShowAdmobRewardedVideo(_reward);
//    //                }
//    //                else
//    //                {
//    //                    Debug.Log("GG >> Admob:rAd:NotLoaded:CheckUnityRad");
//    //                    LoadRewardedVideo();

//    //                    ShowUnityRewardedAd(_reward);
//    //                }
//    //            }
//    //        }
//    //        else
//    //        {
//    //            ResetrAdPriorityCounters();
//    //            ShowRewardedAdWithCustomPriority(_reward);
//    //        }
//    //    }
//    //}

//    //private void ResetiAdPriorityCounters()
//    //{
//    //    AdsConstants.firstiAdCounter = AdsConstants.secondiAdCounter = 0;
//    //}
//    //private void ResetrAdPriorityCounters()
//    //{
//    //    AdsConstants.firstrAdCounter = AdsConstants.secondrAdCounter = 0;
//    //}

//    #endregion

//}
