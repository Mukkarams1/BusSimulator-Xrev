using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePanel : MonoBehaviour
{
    [SerializeField] Text descriptionText;
    private void OnEnable()
    {
        descriptionText.text = LevelsDataManager.Instance.Objective.ToString();
    }
}
