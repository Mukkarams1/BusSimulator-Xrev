using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class pausePanelUi : MonoBehaviour
{

    [SerializeField]Button ReplayButton;
    [SerializeField]Button mainmenuBtn;
    [SerializeField]Button ResumeBtn;

    private void Start()
    {
        ReplayButton.onClick.AddListener(ReplayLevel);
        mainmenuBtn.onClick.AddListener(GotoMainMenu);
        ResumeBtn.onClick.AddListener(Resume);

    }
    private void OnEnable()
    {
        EventManager.PauseGame();
    }
    private void OnDisable()
    {
        EventManager.ResumeGame();
    }
    void GotoMainMenu()
    {
        EventManager.GotoMainMenu();
        this.gameObject.SetActive(false);

    }
    void ReplayLevel()
    {
        
        EventManager.ReplayLevel();
        this.gameObject.SetActive(false);

    }
    void Resume()
    {
        this.gameObject.SetActive(false);
        EventManager.ResumeGame();
    }
}
