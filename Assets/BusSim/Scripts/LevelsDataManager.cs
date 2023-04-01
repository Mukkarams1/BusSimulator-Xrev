using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsDataManager : GenericSingletonClass<LevelsDataManager>
{
    public BusSelectionManager BusSelectionManager;
    [SerializeField]
    public List<Leveldata> levelData;
    [SerializeField]
    public List<GameObject> busPrefabsList = new List<GameObject>();

    public int currentLevel { get; private set; }
    public int totalStopsInLevel { get; private set; } = 0;
    public int starWinningTime { get; private set; } = 0;
    public int starWinningSpeed { get; private set; } = 0;
    public int starWon { get; set; }
    public int LastWonStars { get; set; }
    public int AllowedHits { get; set; }
    public string Objective { get; set; }


    public gameModesEnum currentGameMode { get; set; }
    public WeatherEnum weather { get; set; }

    public List<string> starWinningCondtions = new List<string>();
    int currentBusModelIndex;
    public GameObject currentLevelObject;
    GameObject currentBusObject;

    public  void OnEnable()
    {

        EventManager.onLevelSelected += SetLevel;
        EventManager.onGameModeSelected += SetGameMode;
        EventManager.onBusSelected += SelectBusModel;
        EventManager.onLevelComplete += LevelCompleted;
        EventManager.onContinueToNextLevel += LoadNextLevel;
        EventManager.onGotoMainMenu += GoToMainMenu;
        EventManager.onReplayLevel += RestartLevel;

    }
    private void OnDisable()
    {
        EventManager.onBusSelected -= SelectBusModel;
        EventManager.onGameModeSelected -= SetGameMode;
        EventManager.onLevelSelected -= SetLevel;
        EventManager.onLevelComplete -= LevelCompleted;
        EventManager.onContinueToNextLevel -= LoadNextLevel;
        EventManager.onContinueToNextLevel -= GoToMainMenu;
        EventManager.onReplayLevel -= RestartLevel;

    }
    void RestartLevel()
    {
        SetLevel(currentLevel);
    }
    void GoToMainMenu()
    {
        currentGameMode = gameModesEnum.None;
        LoadScene();
    }
    private void LoadNextLevel()
    {
        if (currentLevel < levelData.Where(i => i.levelMode.Equals(currentGameMode))?.ToList()?.Count)
        {
            
            currentLevel++;
            // load next level
            //show loader 
            //load nextLevelDatafrom list.
            SetLevel(currentLevel);
        }
        else
        {
            Debug.Log("no next level");
            //mode ended
            //GotoMain menu
            GoToMainMenu();
        }
    }
    public void SelectBusModel(int model)
    {
        currentBusModelIndex = model;
    }
    void SetGameMode(gameModesEnum levelMode)
    {
        currentGameMode = levelMode;
        Debug.Log("gamemode Selected" + levelMode);
        EventManager.ShowLevelSelectionPanel();
    }
    public void SetLevel(int level)
    {
        currentLevel = level;
        var currentlevelData = getLevelData(currentGameMode, currentLevel);
        weather = currentlevelData.levelWeather;
        Objective = currentlevelData.objective;
        //starWinningCondtions.Clear();
        starWinningCondtions = currentlevelData.StarWinningConditions;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            LoadLevel();
        }
    }
    void LoadLevel()
    {
        var currentlevelData = getLevelData(currentGameMode, currentLevel);
        AllowedHits = currentlevelData.allowedHits;
        starWinningTime = currentlevelData.starWinTimer;
        starWinningSpeed = currentlevelData.hitSpeed;
        InstantiateLevel(currentlevelData.LevelDataGameObject);
        InstantiateBus();
        totalStopsInLevel = currentLevelObject.transform.Find("BusStops").transform.childCount;
        
        
        GetSetStars();
        starWon = 0;
        


        EventManager.LoadNewLevel();
    }
    void InstantiateBus()
    {
        currentBusObject = Instantiate(busPrefabsList[currentBusModelIndex], currentLevelObject.transform.Find("busSpawnPos"));
    }
    void LevelCompleted()
    {
        totalStopsInLevel = 0;
       
       
        
        Debug.Log("levelCompleted: ");

        // show level completed Screen


    }
    public void setLevelsStars()
    {
        if(starWon > LastWonStars) {
            string modename = Enum.GetName(typeof(gameModesEnum), currentGameMode);
            SaveAndLoadManager.Instance.setStarWon(modename, currentLevel, starWon);
        }
        
       
    }
    public void GetSetStars()
    {
        string modename = Enum.GetName(typeof(gameModesEnum), currentGameMode);
        starWon = SaveAndLoadManager.Instance.getstar(modename, currentLevel);
        LastWonStars = starWon;
    }
    public void InstantiateLevel(GameObject levelObj)
    {
        if (currentLevelObject != null)
        {
            Destroy(currentLevelObject);
        }
        currentLevelObject = Instantiate(levelObj);
    }
    public Leveldata getLevelData(gameModesEnum levelmode, int levelNum)
    {
        return levelData.FirstOrDefault(i => i.levelMode.Equals(levelmode) && i.levelNumber.Equals(levelNum));
    }
    public Leveldata getCurrentLevelData()
    {
        return levelData.FirstOrDefault(i => i.levelMode.Equals(currentGameMode) && i.levelNumber.Equals(currentLevel));
    }
    public void LoadScene()
    {
        switch (currentGameMode)
        {
            case gameModesEnum.Career:
                StartCoroutine(LoadYourAsyncScene(1));
                break;
            case gameModesEnum.highwayMode:
                StartCoroutine(LoadYourAsyncScene(2));

                break;
            case gameModesEnum.offroadMode:
                StartCoroutine(LoadYourAsyncScene(3));

                break;
            case gameModesEnum.Parking:
                StartCoroutine(LoadYourAsyncScene(4));

                break;
            case gameModesEnum.Obstacle:
                StartCoroutine(LoadYourAsyncScene(5));

                break;
            case gameModesEnum.racingMode:
                StartCoroutine(LoadYourAsyncScene(6));

                break;
            case gameModesEnum.Free:
                StartCoroutine(LoadYourAsyncScene(7));

                break;
            case gameModesEnum.None:
                StartCoroutine(LoadYourAsyncScene(0));
                break;
            default:
                break;
        }
    }
    IEnumerator LoadYourAsyncScene(int sceneBuildIndex)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("code is here");
        //EventManager.NewSceneLoaded();
        //IF NOT MAIN MENU THEN LOAD LEVEL OTHER WISE DONT LOAD LEVEL
        if (sceneBuildIndex != 0)
        {
            LoadLevel();
        }

    }
}



