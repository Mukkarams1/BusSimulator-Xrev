using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject starwiningTextPrefab;
    [SerializeField] Transform context;
    private void OnEnable()
    {
        descriptionText.text = LevelsDataManager.Instance.Objective.ToString();

        if(context.transform.childCount > 0)
        {
           for (int i = 0; i < context.transform.childCount; i++)
           {
              Destroy(context.transform.GetChild(i).gameObject);
           }
        }
        foreach (var text in LevelsDataManager.Instance.starWinningCondtions)
        {
           
            var starwinningPrefab = Instantiate(starwiningTextPrefab, context);
            starwinningPrefab.GetComponentInChildren<TMP_Text>().text ="> " + text.ToString();
        }
    }
}
