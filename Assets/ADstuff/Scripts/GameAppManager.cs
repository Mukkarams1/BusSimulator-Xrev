using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Purchasing;
[Serializable]
public class InAppItem{
	public string iapItem_Name;
	public ProductType producttype;

}
public class GameAppManager : MonoBehaviour, IStoreListener
{
	public static GameAppManager instance_;
	public static GameAppManager instance
    {
		get{
			if (!instance_)
				instance_ = GameObject.FindObjectOfType<GameAppManager> ();

			return instance_;
		}
	}
	public MediationHandler mediationhandler;
	public InAppItem[] iapitems=null;
	public static event EventHandler consumable_events;
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
	public static bool check_Unlockall=false;
	public static string remove_AdsString = "remove_adds";  
	public static string UnlockAll="unlockall";
	//public static string UnlockCars="unlockcars";
	private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";
	private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original";
	public GameObject adloadingPanel, rewardedvideocanvas;
	public delegate void RewardReceived();
	public static RewardReceived _rewardReceived;
	void Awake()
	{
		mediationhandler = FindObjectOfType<MediationHandler>();
		DontDestroyOnLoad (instance);
	}
	void Start()
	{
		Debug.Log(GameAppManager.instance.mediationhandler);

		if (m_StoreController == null)
		{
			print("dasda");
			InitializePurchasing();
		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	public void Buy_RemoveAds()
	{
		Buy_Product(0);
	}
	public void Buy_UnlockAllLevels()
	{
		Buy_Product(1);
	}
	public void Buy_UnlockAll_Arms()
	{
		Buy_Product(2);
	}
	public void Buy_UnlockAll_Products()
	{
		Buy_Product(3);
	}
	public void InitializePurchasing() 
	{
		if (IsInitialized())
		{
			return;
		}
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct(remove_AdsString, ProductType.NonConsumable);
		for (int i = 0; i < GameAppManager.instance.iapitems.Length; i++)
		{
			builder.AddProduct(GameAppManager.instance.iapitems[i].iapItem_Name, GameAppManager.instance.iapitems[i].producttype);
		}
		UnityPurchasing.Initialize(this, builder);
	}
	public bool IsInitialized()
	{
		print ("Pass");
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}
	public void Buy_noAds()
	{
		print ("Buy_noAds");
		if (IsInitialized()) {
			print ("IsInitialized*****************");

			if (!CheckProductID_Status (remove_AdsString)) {
				BuyProductID (remove_AdsString);
			}
		}
	}
	public void Buy_unlockall()
	{
		if (IsInitialized()) {
			if (!CheckProductID_Status (UnlockAll)) {
				BuyProductID (UnlockAll);
			}
		}
	}
	public void Buy_Product(int iapID){
		if (IsInitialized ()) {
			if (GameAppManager.instance.iapitems [iapID].producttype == ProductType.NonConsumable) {
				if (!CheckProductID_Status (GameAppManager.instance.iapitems [iapID].iapItem_Name)) {
					BuyProductID (GameAppManager.instance.iapitems [iapID].iapItem_Name);
				}
			} else {
				BuyProductID (GameAppManager.instance.iapitems [iapID].iapItem_Name);
			}
		}
	}
	public bool CheckProductID_Status(string productId){
		Product product = m_StoreController.products.WithID(productId);
		if (product != null && product.hasReceipt) {

			return true;
		} else {
			return false;
		}
	}
	void BuyProductID(string productId)
	{
		if (IsInitialized())
		{
			Product product = m_StoreController.products.WithID(productId);
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}
	public void RestorePurchases()
	{
		if (!IsInitialized())
		{
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			Debug.Log("RestorePurchases started ...");
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			apple.RestoreTransactions((result) => {
			Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}

		else
		{
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		m_StoreController = controller;

		m_StoreExtensionProvider = extensions;
		if (IsInitialized ()) {
			if (CheckProductID_Status (remove_AdsString)) {
				//Tenlogiclocal.Ads_purchase = true;
				//	Debug.Log ("ads are purchase");
			} 

			if (CheckProductID_Status (UnlockAll)) {
				check_Unlockall = true;
				//	Debug.Log ("ads are purchase");
			} 
		}
	}
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{

		if (String.Equals(args.purchasedProduct.definition.id, remove_AdsString, StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		}
		else	if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems [0].iapItem_Name, StringComparison.Ordinal))//unlock_all
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefs.SetInt("RemoveAds", 1);
		}
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[1].iapItem_Name, StringComparison.Ordinal))//unlock_player
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefs.SetInt("Unlocked", 29);
			PlayerPrefs.SetInt("Unlocked1", 14);
			PlayerPrefs.SetInt("Unlocked2", 14);
			PlayerPrefs.SetInt("AllLevels", 1);
		}
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[2].iapItem_Name, StringComparison.Ordinal))//unlock_levels
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			PlayerPrefs.SetInt("Gun1", 1);
			PlayerPrefs.SetInt("Gun2", 1);
			PlayerPrefs.SetInt("Gun3", 1);
			PlayerPrefs.SetInt("Gun4", 1);
			PlayerPrefs.SetInt("Gun5", 1);
			PlayerPrefs.SetInt("AllWeapons", 1);
		}
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[3].iapItem_Name, StringComparison.Ordinal))//cars
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));   
        }
        else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}
		return PurchaseProcessingResult.Complete;
	}
	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
	public void give_CosumeEvent(){
		if (consumable_events != null)
			consumable_events (null, null);
	}
	public void removeall_ConsumeEvent(){
		consumable_events = null;
	}
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}