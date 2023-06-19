using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSelectionManager : MonoBehaviour
{
    public List<int> UnlockedBusses;
    public BusSelectionPanel selectionPanel;

    private void Awake()
    {
        LevelsDataManager.Instance.BusSelectionManager = this;
        selectionPanel = GetComponent<BusSelectionPanel>();
        GetAllUnlockedBussed();
    }
    private void OnEnable()
    {
        selectionPanel = GetComponent<BusSelectionPanel>();
        GetAllUnlockedBussed();
    }

    public void GetAllUnlockedBussed()
    {
        PlayerPrefs.SetInt("BusNo0", 1);
        foreach (var bus in LevelsDataManager.Instance.busPrefabsList) {
            if (PlayerPrefs.HasKey("BusNo" + LevelsDataManager.Instance.busPrefabsList.IndexOf(bus)))
            {
                if (UnlockedBusses.Count-1 != LevelsDataManager.Instance.busPrefabsList.IndexOf(bus))
                UnlockedBusses.Add(LevelsDataManager.Instance.busPrefabsList.IndexOf(bus));
            }
        }
    }

    public void setBussesToBeUnloacked(int busindex)
    {
        PlayerPrefs.SetInt("BusNo"+ busindex,1);
        GetAllUnlockedBussed();
    }
    public void BuyBus()
    {
        if(selectionPanel.currentBusSpec.coinsRequireToUnlock <= WalletDataManager.Instance.coins)
        {
            WalletDataManager.Instance.RemoveCoins((int)selectionPanel.currentBusSpec.coinsRequireToUnlock);
            setBussesToBeUnloacked(selectionPanel.currentBusSpec.busId);
            selectionPanel.buyButton.gameObject.SetActive(false);
            selectionPanel.Selectbtn.gameObject.SetActive(true);
        }
        
    }
}
