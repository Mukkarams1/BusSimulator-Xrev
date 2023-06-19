using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{
    public void ModeSelect(int levelMode)
    {
        switch (levelMode)
        {
            case 1:
                EventManager.SelectGameMode(gameModesEnum.Career);
                break;
            case 2:
                EventManager.SelectGameMode(gameModesEnum.Parking);

                break;
            case 3:
                EventManager.SelectGameMode(gameModesEnum.Obstacle);

                break;
            case 4:
                {
                    LevelsDataManager.Instance.currentGameMode = gameModesEnum.Free;
                    EventManager.SelectLevel(1);
                    LevelsDataManager.Instance.LoadScene();
                }


                break;
            default:
                break;
        }

    }
}
