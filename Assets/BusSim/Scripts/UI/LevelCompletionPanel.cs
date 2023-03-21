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

   
    private void Awake()
    {
        ReplayBtn.onClick.AddListener(ReplayLevel);
        ContinueBtn.onClick.AddListener(ContinueToNextLevel);
        MainMenuBtn.onClick.AddListener(GotoMainMenu);
    }
    private void OnEnable()
    {
        StartCoroutine(PauseGameAfterDelay());
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
        EventManager.ReplayLevel();
        this.gameObject.SetActive(false);

    }
    void ContinueToNextLevel()
    {
        EventManager.ContinueToNextLevel();
        this.gameObject.SetActive(false);

    }
    void GotoMainMenu()
    {
        EventManager.GotoMainMenu();
        this.gameObject.SetActive(false);

    }

}
