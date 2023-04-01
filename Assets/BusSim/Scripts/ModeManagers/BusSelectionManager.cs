using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusSelectionManager : MonoBehaviour
{
    public List<int> UnlockedBusses;

    private void OnEnable()
    {
        GetAllUnlockedBussed();
    }

    private void GetAllUnlockedBussed()
    {
        foreach (var bus in LevelsDataManager.Instance.busPrefabsList) {
            if (PlayerPrefs.HasKey("BusNo" + LevelsDataManager.Instance.busPrefabsList.IndexOf(bus)))
            {
                UnlockedBusses.Add(LevelsDataManager.Instance.busPrefabsList.IndexOf(bus));
            }
        }
    }

    public void setBussesToBeUnloacked(int busindex)
    {
        PlayerPrefs.SetInt("BusNo"+ busindex,1);
    }
   
}
