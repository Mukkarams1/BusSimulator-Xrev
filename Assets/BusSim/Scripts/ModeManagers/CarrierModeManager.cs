using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CarrierModeManager : MonoBehaviour
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

    List<int> pickedRiderNo = new List<int>();
    int pickedRiderCount;

    int currenttime;

    int currentStopNumber = 0;
    int CollisionCounter=0;
    bool isLevelComplete;
    int ridertobepicked = 0;

    public RCC_CarControllerV3 carController;
    public GameObject firework;
    private void Start()
    {
     timerText.gameObject.SetActive(false);


        //EventManager.onReplayLevel += stopallCourtine;
        EventManager.onBusStopReach += BusStopReached;
        EventManager.onPauseGame += PauseGame;
        EventManager.onResumeGame += ResumeGame;
      //EventManager.onLevelComplete += ShowLevelCompletionPanel;
        EventManager.onNewLevelLoaded += NewLevelLoaded;
        EventManager.onBusCollision += BusCollision;
        EventManager.onStarWinningSpeed += SetStarsWon;
        EventManager.onLevelComplete += levelCompleted;
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
        if(carController == null)
        {
            carController = FindObjectOfType<RCC_CarControllerV3>();
        }
        starsText.text =  LevelsDataManager.Instance.starWon.ToString();
        coinText.text = WalletDataManager.Instance.coins.ToString();
       // gemText.text = "Gems = " + WalletDataManager.Instance.gems;

    }
    void stopallCourtine()
    {
        StopAllCoroutines();
    }
    void levelCompleted()
    {
        SetStarsWon();
        var lefttime = currenttime - LevelsDataManager.Instance.starWinningTime;
        if(lefttime > 0)
        {
            SetStarsWon();
        }
        
    }
    void BusCollision()
    {
        CollisionCounter++;
    }


    void BusStopReached(Transform obj)
    {
        currentStopNumber++;
        if (currentStopNumber < LevelsDataManager.Instance.totalStopsInLevel)
        {
            
            Debug.Log("stop number: " + currentStopNumber);
            Debug.Log("total stop number: " + LevelsDataManager.Instance.totalStopsInLevel);
            var stop = obj.transform.GetChild(0).transform;

            PickUp(stop);

        }
        
            if (currentStopNumber == LevelsDataManager.Instance.totalStopsInLevel)
            {
            var stop = obj.transform.GetChild(0).transform;
                DropOff(stop);
                DoFireWorks(obj);

            }
         
    }

    private void DoFireWorks(Transform parent)
    {
       var fireworkobj = Instantiate(firework, parent.position , Quaternion.identity);
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
            StartCoroutine(movetobus(obj, riderGameObject.transform,false));
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
            riderGameObject.transform.position += new Vector3(randomSpread, 0, randomSpread);
            StartCoroutine(movetobus(carController.gameObject.transform, riderGameObject.transform,true));
        }

    }
    IEnumerator movetobus(Transform buspos , Transform objToMove , bool ispick)
    {
        
        int randomspeed = UnityEngine.Random.Range(2, 7);
        objToMove.transform.LookAt(buspos);
        objToMove.transform.DOMove(buspos.transform.position, randomspeed).OnComplete(() => {
            Destroy(objToMove.gameObject);
            if(ispick)
            {
                ridertobepicked++;
                if(ridertobepicked == pickedRiderCount)
                {
                    carController.rigid.isKinematic = false;
                }
                
            }
            else if(!ispick)
            {
                pickedRiderCount--;
                if(pickedRiderCount == 0)
                {
                    carController.rigid.isKinematic = false;
                    EventManager.LevelCompleted();
                    isLevelComplete = true;
                    //level completed successfully
                    StartCoroutine(showlevelCompletionAfterDelay());
                }
              
            }
        });
        yield return null;
    }
    IEnumerator showlevelCompletionAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        ShowLevelCompletionPanel(true);
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
        Time.timeScale = 0f;
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
        timerText.text = remainingTime.ToString()+"s";
    }
}
