using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleModeManager : MonoBehaviour
{
    public static ObstacleModeManager Instance;
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
    TextMeshProUGUI collosionCounterText;

    [SerializeField]
    GameObject[] Riders;


    List<int> pickedRiderNo = new List<int>();
    int pickedRiderCount;

    int currenttime;
    private float delay_in_Collosion_Timer;

    public int currentStopNumber = 0;
    int CollisionCounter = 0;
    bool isLevelComplete;
    int ridertobepicked = 0;
    int RandomValue;
    public GameObject[] CheckPointParticles;
    public RCC_CarControllerV3 carController;
    public GameObject firework;
    public GameObject SuccessAnimation;
    public bool ResetTraffic;
    private GameObject activeCheckpointParticle;

    public GameObject LoadingBar;
    public Image LoadingBarFill;
    public float loadingTime;
    public GameObject RCC_Camera;
    private void Awake()
    {
        LevelsDataManager.Instance.LoadLevel();
    }
    private void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }

        timerText.gameObject.SetActive(false);
        TimerCheck();
        Debug.Log("Startttt");
        //EventManager.onReplayLevel += stopallCourtine;
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
        //EventManager.onReplayLevel -= stopallCourtine;
    }
    private void Update()
    {
        if (carController == null)
        {
            carController = FindObjectOfType<RCC_CarControllerV3>();
        }
        starsText.text = LevelsDataManager.Instance.starWon.ToString();
        coinText.text = WalletDataManager.Instance.coins.ToString();
        delay_in_Collosion_Timer += Time.deltaTime;
        // gemText.text = "Gems = " + WalletDataManager.Instance.gems;

    }
    void stopallCourtine()
    {
        StopAllCoroutines();
    }
    void levelCompleted()
    {
        SetStarsWon();
        var lefttime = LevelsDataManager.Instance.starWinningTime - currenttime;
        if (lefttime > 0)
        {
            SetStarsWon();
        }
        delay_in_Collosion_Timer += Time.deltaTime;
    }
    void BusCollision()
    {
        if (delay_in_Collosion_Timer >= 2)
        {
            delay_in_Collosion_Timer = 0;
            CollisionCounter++;
            collosionCounterText.text = "Hit Count : " + CollisionCounter.ToString() + "/" + LevelsDataManager.Instance.AllowedHits;

        }

        if (CollisionCounter == LevelsDataManager.Instance.AllowedHits)
        {
            ShowLevelCompletionPanel(false);
        }

    }


    void BusStopReached(Transform obj)
    {
        currentStopNumber++;
        Debug.Log("Current Bus Stop: " + currentStopNumber);

        if (currentStopNumber == LevelsDataManager.Instance.totalStopsInLevel)
        {
            RCCCanvas.SetActive(false);
            Debug.Log("stop number: " + currentStopNumber);
            Debug.Log("total stop number: " + LevelsDataManager.Instance.totalStopsInLevel);
            LevelComplete();
            //     var stop = obj.transform.GetChild(0).transform;

            // PickUp(stop);

        }

        //    if (currentStopNumber == LevelsDataManager.Instance.totalStopsInLevel)
        //    {
        //    var stop = obj.transform.GetChild(0).transform;
        //        DropOff(stop);
        //  //  StartCoroutine(SuccessAnmationhActivator(true,2f));
        //   // StartCoroutine(SuccessAnmationhActivator(false, 6f));
        //    //SuccessAnimation.SetActive(true);
        //    //Invoke("DisableSuccessAnmation", 6f);
        //     DoFireWorks(obj);

        //}

    }
    void DeactivateSuccessAnmation()
    {
        SuccessAnimation.SetActive(false);
    }

    //IEnumerator SuccessAnmationhActivator(bool isActivated, float checktime)
    //{
    //    yield return new WaitForSeconds(checktime);
    //    SuccessAnimation.SetActive(isActivated);
    //}
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
        carController.rigid.isKinematic = true;

        for (int i = 0; i < pickedRiderCount; i++)
        {
            var riderGameObject = Instantiate(Riders[pickedRiderNo[i]]);
            riderGameObject.transform.position = carController.transform.position;
            int randomSpread = UnityEngine.Random.Range(0, 5);
            riderGameObject.transform.position += new Vector3(randomSpread, 0, 0);
            StartCoroutine(movetobus(obj, riderGameObject.transform, false));
        }
    }

    private void PickUp(Transform obj)
    {
        // do pick up

        carController.rigid.isKinematic = true;

        int spawnCount = UnityEngine.Random.Range(1, 4);
        pickedRiderCount = spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            int randomrider = UnityEngine.Random.Range(0, Riders.Length - 1);
            pickedRiderNo.Add(randomrider);
            var riderGameObject = Instantiate(Riders[randomrider]);
            riderGameObject.transform.position = obj.transform.position;
            int randomSpread = UnityEngine.Random.Range(0, 10);
            riderGameObject.transform.position += new Vector3(randomSpread, 0, 0);
            StartCoroutine(movetobus(carController.gameObject.transform, riderGameObject.transform, true));
        }
        RCCCanvas.SetActive(true);
    }
    IEnumerator movetobus(Transform buspos, Transform objToMove, bool ispick)
    {

        int randomspeed = UnityEngine.Random.Range(2, 5);
        objToMove.transform.LookAt(buspos);
        objToMove.transform.DOMove(buspos.transform.position, randomspeed).OnComplete(() => {
            Destroy(objToMove.gameObject);
            if (ispick)
            {
                ridertobepicked++;
                if (ridertobepicked == pickedRiderCount)
                {
                    carController.rigid.isKinematic = false;
                }

            }
            else if (!ispick)
            {
                pickedRiderCount--;
                if (pickedRiderCount == 0)
                {
                    carController.rigid.isKinematic = false;
                    EventManager.LevelCompleted();
                    isLevelComplete = true;
                    SuccessAnimation.SetActive(true);
                    Invoke("DeactivateSuccessAnmation", 4f);
                    //level completed successfully
                    ShowAds();
                    StartCoroutine(showlevelCompletionAfterDelay());
                }

            }
        });
        yield return null;
    }
    IEnumerator showlevelCompletionAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        ShowLevelCompletionPanel(true);
    }
    private void LevelComplete()
    {

        EventManager.LevelCompleted();
        isLevelComplete = true;
        SuccessAnimation.SetActive(true);
        Invoke("DeactivateSuccessAnmation", 4f);
        //level completed successfully
        StartCoroutine(showlevelCompletionAfterDelay());
    }
    private void ResetVariables()
    {
        //LevelsDataManager.Instance.starWon = 0;
        currentStopNumber = 0;
        CollisionCounter = 0;
        isLevelComplete = false;
        pickedRiderCount = 0;
        ridertobepicked = 0;
        pickedRiderNo.Clear();
        Debug.Log("Variables Reset");



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
       // Time.timeScale = 0f;
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
        Debug.Log("TimerCheck");
        if (LevelsDataManager.Instance.getCurrentLevelData().timeInSec > 0)
        {
            timerText.gameObject.SetActive(true);
            Debug.Log("TimerCheck true");
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
        collosionCounterText.text = "Hit Count : " + "0/" + LevelsDataManager.Instance.AllowedHits;
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
        currenttime = (int)remainingTime;
        timerText.text = remainingTime.ToString() + "s";
    }
    public void ActivateCheckpointParticles()
    {
        // 
        if ((CheckPointParticles[0].activeSelf == true) || (CheckPointParticles[1].activeSelf == true))
        {
            CheckPointParticles[2].SetActive(true);
        }
        if ((CheckPointParticles[0].activeSelf == true) || (CheckPointParticles[2].activeSelf == true))
        {
            CheckPointParticles[1].SetActive(true);
        }
        if ((CheckPointParticles[1].activeSelf == true) || (CheckPointParticles[2].activeSelf == true))
        {
            CheckPointParticles[0].SetActive(true);
        }
        else
        {
            RandomValue = UnityEngine.Random.Range(0, CheckPointParticles.Length);
            CheckPointParticles[RandomValue].SetActive(true);
        }
        //  CheckPointParticles[RandomValue].SetActive(true);
        Invoke("DeActivateCheckpointParticles", 2f);
    }
    void DeActivateCheckpointParticles()
    {
        if (CheckPointParticles[0].activeSelf == true)
        {
            CheckPointParticles[0].SetActive(false);
        }
        if (CheckPointParticles[1].activeSelf == true)
        {
            CheckPointParticles[1].SetActive(false);
        }
        if (CheckPointParticles[2].activeSelf == true)
        {
            CheckPointParticles[2].SetActive(false);
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
