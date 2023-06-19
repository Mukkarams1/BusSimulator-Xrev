using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletionPanel : MonoBehaviour
{
    [SerializeField]
    Button ReplayBtn;
    [SerializeField]
    Button ContinueBtn;
    [SerializeField]
    Button MainMenuBtn;
    [SerializeField]
    TextMeshProUGUI levelCompletionText;
    [SerializeField]
    Button PauseButton;
    [SerializeField]
    GameObject stars;
    [SerializeField]
    Transform contextForStars;

   
    private void Awake()
    {
        ReplayBtn.onClick.AddListener(ReplayLevel);
        ContinueBtn.onClick.AddListener(ContinueToNextLevel);
        MainMenuBtn.onClick.AddListener(GotoMainMenu);
    }
    private void OnEnable()
    {
        DestroyStarPrefab();
        StartCoroutine(PauseGameAfterDelay());
        ShowWonStars();
    }

    private void ShowWonStars()
    {
        for(int i = 0; i < LevelsDataManager.Instance.starWon; i++)
        {
            Instantiate(stars, contextForStars);
        }
    }
   
    private void DestroyStarPrefab()
    {
        if(contextForStars.childCount >= 1)
        {
            for (int j = 0; j < contextForStars.childCount; j++)
            {
                Destroy(contextForStars.GetChild(j).gameObject);
            }
        }
        
    }
    IEnumerator PauseGameAfterDelay()
    {
       
        if(FindObjectOfType<CarrierModeManager>() != null)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return null;
        }
        EventManager.PauseGame();
    }
    private void OnDisable()
    {
        EventManager.ResumeGame();
    }
   public  void SetCompletionPanelUI(bool isSuccessful)
    {
        if (isSuccessful)
        {
            levelCompletionText.text = "Level Completed";
            //levelComplete.gameObject.SetActive(true);
            ContinueBtn.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(false);
        }
        else
        {
            levelCompletionText.text = "Level Failed";
            //levelFail.gameObject.SetActive(true);
            ContinueBtn.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }
    }
    void ReplayLevel()
    {
        PauseButton.gameObject.SetActive(true);
        EventManager.ReplayLevel();
        DestroyStarPrefab();
        this.gameObject.SetActive(false);

    }
    void ContinueToNextLevel()
    {
        PauseButton.gameObject.SetActive(true);
        EventManager.ContinueToNextLevel();
        DestroyStarPrefab();
        this.gameObject.SetActive(false);

    }
    void GotoMainMenu()
    {
        PauseButton.gameObject.SetActive(true);
        EventManager.GotoMainMenu();
        DestroyStarPrefab();
        this.gameObject.SetActive(false);

    }

}
