using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartImagecontroller : MonoBehaviour {
	public GameObject startimage,PrivacyObj, assetloading;
	string filePath = "",imagePath="";
	Sprite tempaddsprite;
	public Sprite AccImg, GameImg;
	public static int firstcount = 0;
	public MediationHandler mediationHandler;
	// Use this for initialization
	void Awake(){
//		PlayerPrefs.DeleteAll ();
		if (firstcount == 0) {
			firstcount++;
			if (PlayerPrefs.GetInt ("GDPRConsentAd", 0) == 0) {

			} else if (PlayerPrefs.GetInt ("GDPRConsentAd") == 1) {
				PrivacyObj.SetActive (false);
				startimage.SetActive (true);
				startimage.GetComponent<Animator> ().SetTrigger ("fadeout");
				Invoke("companyimagefadein",4f);
				
			}
		} else
		{
			SceneManager.LoadScene (1);
		}
		if (!mediationHandler)
		{
			mediationHandler = FindObjectOfType<MediationHandler>();
		}

	}
	void Start () {

		//		imagePath=Application.streamingAssetsPath + "/AccImg.jpg";
		//		if (Application.platform == RuntimePlatform.Android) {
		//			StartCoroutine (load_Image ()); 
		//		} else {
		//			imagePath="file:///"+ Application.streamingAssetsPath + "/AccImg.jpg";
		//			StartCoroutine (load_Image ()); 
		//		}
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
  
	public void On_AgreeButton(){
		PlayerPrefs.SetInt ("GDPRConsentAd",1);
		PrivacyObj.SetActive (false);
		startimage.SetActive (true);
		startimage.GetComponent<Animator> ().SetTrigger ("fadeout");

		Invoke("companyimagefadein",3f);
	}

	////////////////////////////// ******** Privacy Policy ********** ////////////////////////////////////

	public void On_PrivacyButton(){
		Application.OpenURL("https://xingsgames.blogspot.com/2019/01/privacy-policy.html");
	}

	////////////////////////////// ******** Privacy Policy ********** ////////////////////////////////////

	void companyimagefadein(){
		startimage.GetComponent<Animator> ().SetTrigger ("fadein");
		Invoke ("load_gameimage",3f);
	}
	void load_gameimage()
	{
		startimage.GetComponent<Image> ().sprite = GameImg;
		startimage.GetComponent<Animator> ().SetTrigger ("fadeout");
		assetloading.SetActive(true);
		Invoke("gameimagefadein",3f);
		mediationHandler.ShowMbanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
	}
	void gameimagefadein()
	{
		startimage.GetComponent<Animator> ().SetTrigger ("fadein");
		SceneManager.LoadScene (1);
	}
		
}
