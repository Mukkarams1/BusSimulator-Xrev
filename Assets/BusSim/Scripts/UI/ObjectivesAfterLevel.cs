using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ObjectivesAfterLevel : MonoBehaviour
{
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject starwiningTextPrefab;
    [SerializeField] Transform context;

    [SerializeField] Button Close;
    // Start is called before the first frame update
    void Start()
    {
        Close.onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        ShowObjectiveForLevel();
        StartCoroutine(pauseaftersometime());
    }
    IEnumerator pauseaftersometime()
    {
        yield return new WaitForSeconds(0.5f);
        EventManager.PauseGame();
    }
    private void OnDisable()
    {
        EventManager.ResumeGame();
    }

    private void ShowObjectiveForLevel()
    {
        gameObject.SetActive(true);
        descriptionText.text = LevelsDataManager.Instance.Objective.ToString();

        if (context.transform.childCount > 0)
        {
            for (int i = 0; i < context.transform.childCount; i++)
            {
                Destroy(context.transform.GetChild(i).gameObject);
            }
        }
        foreach (var text in LevelsDataManager.Instance.starWinningCondtions)
        {

            var starwinningPrefab = Instantiate(starwiningTextPrefab, context);
            starwinningPrefab.GetComponentInChildren<TMP_Text>().text = "> " + text.ToString();
        }
    }
}
