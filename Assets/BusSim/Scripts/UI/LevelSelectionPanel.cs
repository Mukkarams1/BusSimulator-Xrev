using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MonoBehaviour
{
    [SerializeField]
    GameObject LevelSelectionBtnPrefab;
    [SerializeField]
    Transform LevelSelectionContent;
    private void OnEnable()
    {
        InstantiateBtns();
    }

    private void OnDisable()
    {
        Clear();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InstantiateBtns()
    {
        foreach (var item in LevelsDataManager.Instance.levelData.Where(i => i.levelMode.Equals(LevelsDataManager.Instance. currentGameMode))) 
        {
            var levelNo = (LevelsDataManager.Instance.levelData.IndexOf(item) + 1);
            var btnObj = Instantiate(LevelSelectionBtnPrefab,LevelSelectionContent);
            btnObj.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + levelNo;
            btnObj.GetComponent<Button>().onClick.AddListener(()=>SetLevel(levelNo));
        }
    }
    void SetLevel(int level)
    {
        EventManager.SelectLevel(level);
    }
    void Clear()
    {
        
        foreach (Transform child in LevelSelectionContent)
        {
            Destroy(child.gameObject);
        }
    }
}
