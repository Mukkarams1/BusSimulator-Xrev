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
            var modeEnm = (gameModesEnum)Enum.Parse(typeof(gameModesEnum), mod);
            if (modeEnm!= gameModesEnum.None && modeEnm != gameModesEnum.racingMode && modeEnm != gameModesEnum.highwayMode && modeEnm != gameModesEnum.offroadMode )
            {
            var btn = Instantiate(ModeBtn,ModeBtnsContent);
            btn.GetComponentInChildren<TextMeshProUGUI>().text=mod + " Mode";
                btn.GetComponent<ModeSelectionBtn>().SetImage(mod);
            btn.onClick.AddListener(() => GameModeSelected(modeEnm));
            }
        }

    }
    void GameModeSelected(gameModesEnum levelMode)
    {
        switch (levelMode)
        {
            case gameModesEnum.Career:
                EventManager.SelectGameMode(gameModesEnum.Career);
                break;
            case gameModesEnum.highwayMode:
                EventManager.SelectGameMode(gameModesEnum.highwayMode);

                break;
            case gameModesEnum.offroadMode:
                EventManager.SelectGameMode(gameModesEnum.offroadMode);

                break;
            case gameModesEnum.Parking:
                EventManager.SelectGameMode(gameModesEnum.Parking);

                break;
            case gameModesEnum.Obstacle:
                EventManager.SelectGameMode(gameModesEnum.Obstacle);

                break;
            case gameModesEnum.racingMode:
                EventManager.SelectGameMode(gameModesEnum.racingMode);

                break;
            case gameModesEnum.Free:
                EventManager.SelectGameMode(gameModesEnum.Free);

                break;
            default:
                break;
        }
    }
}
public enum gameModesEnum
{
    Career = 1,
    highwayMode = 2,
    offroadMode = 3,
    Parking = 4,
    Obstacle = 5,
    racingMode = 6,
    Free = 7,
    None
}