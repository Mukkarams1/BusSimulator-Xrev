using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{

    public static Action<gameModesEnum> onGameModeSelected;
    public static Action<int> onLevelSelected;
    public static Action<int> onBusSelected;
    public static Action<Transform> onBusStopReach;
    public static Action onNewSceneLoadComplete;
    public static Action onLevelComplete;
    public static Action onReplayLevel;
    public static Action onContinueToNextLevel;
    public static Action onGotoMainMenu;
    public static Action onPauseGame;
    public static Action onResumeGame;
    public static Action onShowLevelSelectionPanel;
    public static Action onNewLevelLoaded;
    public static Action onBusCollision;
    public static Action onStarWinningSpeed;
    public static Action onStarWinningTimer;
    #region UIEvents
    public static void SelectGameMode(gameModesEnum gameMode)
    {
        onGameModeSelected?.Invoke(gameMode);
    }
    public static void SelectLevel(int level)
    {
        onLevelSelected?.Invoke(level);
    }

    public static void SelectBus(int bus)
    {
        onBusSelected?.Invoke(bus);
    }
    #endregion

    public static void ReachedBusStop(Transform obj)
    {
        onBusStopReach?.Invoke(obj);
       
    }
    public static void StarWinningSpeedReached()
    {
        onStarWinningSpeed?.Invoke();
    }
    public static void starWinningTimer()
    {
        onStarWinningTimer?.Invoke();
    }
    public static void NewSceneLoaded()
    {
        onNewSceneLoadComplete?.Invoke();
    }
    public static void LevelCompleted()
    {
        onLevelComplete?.Invoke();
    }
    public static void ReplayLevel()
    {
        onReplayLevel?.Invoke();
    }
    public static void GotoMainMenu()
    {
        onGotoMainMenu?.Invoke();
    }
    public static void ContinueToNextLevel()
    {
        onContinueToNextLevel?.Invoke();
    }
    public static void PauseGame()
    {
        onPauseGame?.Invoke();
    }
    public static void ResumeGame()
    {
        onResumeGame?.Invoke();
    }
    public static void ShowLevelSelectionPanel()
    {
        onShowLevelSelectionPanel?.Invoke();
    }
    public static void LoadNewLevel()
    {
        onNewLevelLoaded?.Invoke();
    }
    public static void BusCollision()
    {
        onBusCollision?.Invoke();
    }
}

