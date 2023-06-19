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

    int currenttime;

    int currentStopNumber = 0;
    int CollisionCounter=0;
    bool isLevelComplete;

    public RCC_CarControllerV3 carController;
<<<<<<< Updated upstream
=======
    public GameObject firework;
    public GameObject SuccessAnimation;
>>>>>>> Stashed changes
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
    }
    private void OnDisable()
    {
        EventManager.onBusStopReach -= BusStopReached;
        EventManager.onPauseGame -= PauseGame;
        EventManager.onResumeGame -= ResumeGame;
    //  EventManager.onLevelComplete -= ShowLevelCompletionPanel;
        EventManager.onNewLevelLoaded -= NewLevelLoaded;
        EventManager.onBusCollision -= BusCollision;

    }
    private void Update()
    {
        if(carController == null)
        {
            carController = FindObjectOfType<RCC_CarControllerV3>();
        }
        starsText.text = "StarsWon = " + LevelsDataManager.Instance.starWon;
        coinText.text = "Coins = " + WalletDataManager.Instance.coins;
        gemText.text = "Gems = " + WalletDataManager.Instance.gems;

    }
    void levelCompleted()
    {
        SetStarsWon();
        var lefttime = LevelsDataManager.Instance.starWinningTime - currenttime;
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
        
        if (currentStopNumber < LevelsDataManager.Instance.totalStopsInLevel)
        {
            currentStopNumber++;
            Debug.Log("stop number: " + currentStopNumber);
            Debug.Log("total stop number: " + LevelsDataManager.Instance.totalStopsInLevel);
            var stop = obj.transform.GetChild(0).transform;

            PickUp(stop);

        }
        
            if (currentStopNumber == LevelsDataManager.Instance.totalStopsInLevel)
            {
            var stop = obj.transform.GetChild(0).transform;
                DropOff(stop);
<<<<<<< Updated upstream
                EventManager.LevelCompleted();
                isLevelComplete = true;
                //level completed successfully
                ShowLevelCompletionPanel(true);
=======
          //  StartCoroutine(SuccessAnmationhActivator(true,2f));
           // StartCoroutine(SuccessAnmationhActivator(false, 6f));
            //SuccessAnimation.SetActive(true);
            //Invoke("DisableSuccessAnmation", 6f);
             DoFireWorks(obj);
>>>>>>> Stashed changes

        }
         
    }
    void DeactivateSuccessAnmation()
    {
        SuccessAnimation.SetActive(false);
    }

<<<<<<< Updated upstream
=======
    //IEnumerator SuccessAnmationhActivator(bool isActivated, float checktime)
    //{
    //    yield return new WaitForSeconds(checktime);
    //    SuccessAnimation.SetActive(isActivated);
    //}
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

>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            riderGameObject.transform.position += new Vector3(randomSpread, 0, randomSpread);
            StartCoroutine(movetobus(carController.gameObject.transform, riderGameObject.transform));
=======
            riderGameObject.transform.position += new Vector3(randomSpread, 0, 0);
            StartCoroutine(movetobus(carController.gameObject.transform, riderGameObject.transform,true));
>>>>>>> Stashed changes
        }

    }
    IEnumerator movetobus(Transform buspos , Transform objToMove)
    {
        
<<<<<<< Updated upstream
        objToMove.transform.DOMove(buspos.transform.position, 5f).OnComplete(() => {
            Destroy(objToMove.gameObject);
            carController.rigid.isKinematic = false;
        });
        yield return null;
    }
=======
        int randomspeed = UnityEngine.Random.Range(2, 5);
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
                    SuccessAnimation.SetActive(true);
                    Invoke("DeactivateSuccessAnmation", 4f);
                    //level completed successfully
                    StartCoroutine(showlevelCompletionAfterDelay());
                }
              
            }
        });
        yield return null;
    }
    IEnumerator showlevelCompletionAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        ShowLevelCompletionPanel(true);
    }
>>>>>>> Stashed changes
    private void ResetVariables()
    {
        currentStopNumber = 0;
        CollisionCounter = 0;
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
