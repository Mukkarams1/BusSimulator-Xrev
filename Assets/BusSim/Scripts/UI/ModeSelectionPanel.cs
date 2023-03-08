using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelectionPanel : MonoBehaviour
{
    [SerializeField]
    Button ModeBtn;
    [SerializeField]
    Transform ModeBtnsContent;
    private void Start()
    {
        foreach (var mod in Enum.GetNames(typeof(gameModesEnum)))
        {
            var btn = Instantiate(ModeBtn,ModeBtnsContent);
            var modeEnm = (gameModesEnum)Enum.Parse(typeof(gameModesEnum), mod);
            btn.GetComponentInChildren<TextMeshProUGUI>().text=mod;
            btn.onClick.AddListener(() => GameModeSelected(modeEnm));
        }

    }
    void GameModeSelected(gameModesEnum levelMode)
    {
        switch (levelMode)
        {
            case gameModesEnum.careerMode:
                EventManager.SelectGameMode(gameModesEnum.careerMode);
                break;
            case gameModesEnum.highwayMode:
                EventManager.SelectGameMode(gameModesEnum.highwayMode);

                break;
            case gameModesEnum.offroadMode:
                EventManager.SelectGameMode(gameModesEnum.offroadMode);

                break;
            case gameModesEnum.parkingMode:
                EventManager.SelectGameMode(gameModesEnum.parkingMode);

                break;
            case gameModesEnum.obstacleMode:
                EventManager.SelectGameMode(gameModesEnum.obstacleMode);

                break;
            case gameModesEnum.racingMode:
                EventManager.SelectGameMode(gameModesEnum.racingMode);

                break;
            case gameModesEnum.freeMode:
                EventManager.SelectGameMode(gameModesEnum.freeMode);

                break;
            default:
                break;
        }
    }
}
public enum gameModesEnum
{
    careerMode = 1,
    highwayMode = 2,
    offroadMode = 3,
    parkingMode = 4,
    obstacleMode = 5,
    racingMode = 6,
    freeMode = 7,
    None
}