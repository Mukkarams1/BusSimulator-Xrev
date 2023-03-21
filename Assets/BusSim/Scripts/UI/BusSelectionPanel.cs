using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusSelectionPanel : MonoBehaviour
{
   
    int currentBusIndex = 0;
    GameObject currentBus;
    #region Btns
    [SerializeField]
    Button NextBtn;
    [SerializeField]
    Button CloseBtn;
    [SerializeField]
    Button PrevBtn;
    [SerializeField]
    Button Selectbtn;
    #endregion
    private void OnEnable()
    {
        currentBusIndex = 0;
        ShowBus(currentBusIndex);
    }
    private void Awake()
    {
        NextBtn.onClick.AddListener(OnNextClicked);
        PrevBtn.onClick.AddListener(OnPrevClicked);
        CloseBtn.onClick.AddListener(OnClickClose);
        Selectbtn.onClick.AddListener(OnClickSelect);
    }
    private void OnDisable()
    {
    }
    void ShowBus(int Index)
    {
        /// may do it with pooling later
        if (currentBus != null)
            DeleteBus();
        currentBus = Instantiate(LevelsDataManager.Instance.busPrefabsList[Index],Vector3.back,Quaternion.Inverse(Quaternion.identity));
        currentBus.GetComponent<Bus>().FreezeBus(true);
    }
    void DeleteBus()
    {
        Destroy(currentBus);
    }
    private void OnClickClose()
    {
        DeleteBus();
        gameObject.SetActive(false);
    }
    public void SetBusDataOnUI(int index)
    {

    }
    private void OnNextClicked()
    {
        currentBusIndex++;
        if (currentBusIndex > LevelsDataManager.Instance.busPrefabsList.Count - 1)
        {
            currentBusIndex = 0;

        }
        if (LevelsDataManager.Instance.busPrefabsList.Count > 0)
            SetBusDataOnUI(currentBusIndex);
        ShowBus(currentBusIndex);
    }

    private void OnPrevClicked()
    {
        currentBusIndex--;
        if (currentBusIndex < 0)
        {
            currentBusIndex = LevelsDataManager.Instance.busPrefabsList.Count - 1;
        }
        if (LevelsDataManager.Instance.busPrefabsList.Count > 0)
            SetBusDataOnUI(currentBusIndex);
        ShowBus(currentBusIndex);
    }
    private void OnClickSelect()
    {
        EventManager.onBusSelected(currentBusIndex);
        OnClickClose();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



}
