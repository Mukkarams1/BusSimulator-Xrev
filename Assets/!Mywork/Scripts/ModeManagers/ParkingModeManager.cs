using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParkingModeManager: MonoBehaviour
{
    [SerializeField]
    LevelCompletionPanel levelCompletionPanel;
    [SerializeField]
    GameObject RCCCanvas;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI starsText;
    [SerializeField]
    TextMeshProUGUI coinText;
    [SerializeField]
    TextMeshProUGUI gemText;

    [SerializeField]
    GameObject[] Riders;

    int currenttime;

    int currentStopNumber = 0;
    int CollisionCounter=0;
    bool isLevelComplete;

    float time_Before_Collision;

    public RCC_CarControllerV3 carController;
    public GameObject firework;

    public GameObject LoadingBar;
    public Image LoadingBarFill;
    public float loadingTime;
    public GameObject[] WinningParticles;
    private void Start()
    {
     timerText.gameObject.SetActive(false);

        

        EventManager.onBusStopReach += BusStopReached;
        EventManager.onPauseGame += PauseGame;
        EventManager.onResumeGame += ResumeGame;
      //EventManager.onLevelComplete += ShowLevelCompletionPanel;
        EventManager.onNewLevelLoaded += NewLevelLoaded;
        EventManager.onBusCollision += BusCollision;
        EventManager.onStarWinningSpeed += SetStarsWon;
        EventManager.onLevelComplete += levelCompleted;
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.hideSmallBanner();
        }
    }
    private void OnDisable()
    {
        EventManager.onStarWinningSpeed -= SetStarsWon;
        EventManager.onBusStopReach -= BusStopReached;
        EventManager.onPauseGame -= PauseGame;
        EventManager.onResumeGame -= ResumeGame;
    //  EventManager.onLevelComplete -= ShowLevelCompletionPanel;
        EventManager.onNewLevelLoaded -= NewLevelLoaded;
        EventManager.onBusCollision -= BusCollision;
        EventManager.onLevelComplete -= levelCompleted;

    }
    private void Update()
    {
        if(carController == null)
        {
            carController = FindObjectOfType<RCC_CarControllerV3>();
        }
        starsText.text = LevelsDataManager.Instance.starWon.ToString();
        coinText.text = WalletDataManager.Instance.coins.ToString();
        //gemText.text = "Gems = " + WalletDataManager.Instance.gems;
        time_Before_Collision += Time.deltaTime;
    }
    void levelCompleted()
    {
        SetStarsWon();
        var lefttime = currenttime - LevelsDataManager.Instance.starWinningTime ;
        if(lefttime + 5 > 0)
        {
            SetStarsWon();
        }
        
    }
    void BusCollision()
    {
        ShowLevelCompletionPanel(false);
        if (time_Before_Collision >= 1)
        {
            time_Before_Collision = 0;
            CollisionCounter++;
        }


    }


    void BusStopReached(Transform obj)
    {
        
        if (currentStopNumber < LevelsDataManager.Instance.totalStopsInLevel)
        {
            currentStopNumber++;
            Debug.Log("stop number: " + currentStopNumber);
            Debug.Log("total stop number: " + LevelsDataManager.Instance.totalStopsInLevel);
    
        }
        
            if (currentStopNumber == LevelsDataManager.Instance.totalStopsInLevel)
            {
            carController.rigid.isKinematic = true;
            EventManager.LevelCompleted();
                isLevelComplete = true;
            //level completed successfully
            WinningParticlesActivate();
            Invoke("WinningParticlesDeActivate", 2f);
            Invoke("WinningParticlesActivate", 2.5f);
            DoFireWorks(obj);
            StartCoroutine(showlevelCompletionAfterDelay());

            }
         
    }
    IEnumerator showlevelCompletionAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        ShowAds();
        ShowLevelCompletionPanel(true);
    }
    private void DoFireWorks(Transform parent)
    {
        var fireworkobj = Instantiate(firework, parent.position, Quaternion.identity);
        StartCoroutine(destroyFireWorks(fireworkobj));

    }
    IEnumerator destroyFireWorks(GameObject firework)
    {
        yield return new WaitForSeconds(5f);
        Destroy(firework);
    }

    private void DropOff(Transform obj)
    {
        int spawnCount = UnityEngine.Random.Range(1, 3);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomrider = UnityEngine.Random.Range(0, Riders.Length);
            var riderGameObject = Instantiate(Riders[randomrider]);
            riderGameObject.transform.position = carController.transform.position;
            int randomSpread = UnityEngine.Random.Range(0, 5);
            riderGameObject.transform.position += new Vector3(randomSpread, 0, randomSpread);
            StartCoroutine(movetobus(obj, riderGameObject.transform));
        }
    }

    private void PickUp(Transform obj)
    {
        // do pick up

        carController.rigid.isKinematic = true;

        int spawnCount = UnityEngine.Random.Range(1, 3);

        for (int i = 0; i < spawnCount; i++)
        {
            int randomrider = UnityEngine.Random.Range(0, Riders.Length - 1);
            var riderGameObject = Instantiate(Riders[randomrider]);
            riderGameObject.transform.position = obj.transform.position;
            int randomSpread = UnityEngine.Random.Range(0, 10);
            riderGameObject.transform.position += new Vector3(randomSpread, 0, randomSpread);
            StartCoroutine(movetobus(carController.gameObject.transform, riderGameObject.transform));
        }

    }
    IEnumerator movetobus(Transform buspos , Transform objToMove)
    {
        
        objToMove.transform.DOMove(buspos.transform.position, 5f).OnComplete(() => {
            Destroy(objToMove.gameObject);
            carController.rigid.isKinematic = false;
        });
        yield return null;
    }
    private void ResetVariables()
    {
        currentStopNumber = 0;
        CollisionCounter = 0;
        //LevelsDataManager.Instance.starWon = 0;
        isLevelComplete = false;
    }



    // Properties
    //public Bus bus;
    //public List<Passenger> passengers;
    //public float score;

    // Methods
    public void StartGame()
    {
        // Implement game start logic here
    }

    public void EndGame()
    {
        // Implement game end logic here
    }

    public void PauseGame()
    {
      //  Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void UpdateScore(float value)
    {
        //   score += value;
    }

    public void SetStarsWon()
    {
        if (LevelsDataManager.Instance.starWon < 3)
        {
            LevelsDataManager.Instance.starWon++;
            WalletDataManager.Instance.AddCoins(1000);
            WalletDataManager.Instance.AddGems(1000);
            LevelsDataManager.Instance.setLevelsStars();
        }
        
        Debug.Log(LevelsDataManager.Instance.starWon);
    }
    void TimerCheck()
    {
        if (LevelsDataManager.Instance.getCurrentLevelData().timeInSec>0)
        {
            timerText.gameObject.SetActive(true);
            StartCoroutine(StartCountdown(LevelsDataManager.Instance.getCurrentLevelData().timeInSec));
        }
    }
    public void CheckLoseCondition()
    {
        // Implement lose condition checking logic here
    }
    public void ShowLevelCompletionPanel(bool isSuccessful)
    {
        RCCCanvas.SetActive(false);
        levelCompletionPanel.gameObject.SetActive(true);
        levelCompletionPanel.SetCompletionPanelUI(isSuccessful);
        StopAllCoroutines();
    }
    public void NewLevelLoaded()
    {
        //carController.rigid.isKinematic = false;
        StopAllCoroutines();
        ResetVariables();
        RCCCanvas.SetActive(true);
        levelCompletionPanel.gameObject.SetActive(false);
        // need to change according to mode
        TimerCheck();
    }
    public IEnumerator StartCountdown(float countdownValue)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            ShowTime(currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        //Game Over
        ShowLevelCompletionPanel(false);
    }

    void ShowTime(float remainingTime)
    {
        currenttime =(int)remainingTime;
        timerText.text =remainingTime.ToString() +"s";
    }
    void WinningParticlesDeActivate()
    {
        for(int i=0; i<WinningParticles.Length; i++)
        {
            WinningParticles[i].SetActive(false);
        }
    }
    void WinningParticlesActivate()
    {
        for (int i = 0; i < WinningParticles.Length; i++)
        {
            WinningParticles[i].SetActive(true);
        }
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
        Loading();
        Invoke("ShowIntersitial", 2f);
        Invoke("ShowMediumBanner", 2.5f);
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
    public void hideMediumBanner()
    {
        if (AdmobAdsManager.Instance != null)
        {
            AdmobAdsManager.Instance.hideMediumBanner();
        }
    }

}