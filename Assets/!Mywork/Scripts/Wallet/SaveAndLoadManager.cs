using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadManager : GenericSingletonClass<SaveAndLoadManager>
{
    public int totalCoins;

    // Start is called before the first frame update
    void Start()
    {
        totalCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    public void addCoins(int amount)
    {
        totalCoins += amount;
        PlayerPrefs.SetInt("Coins", totalCoins);
        setCoins();
    }
    private void setCoins()
    {
        totalCoins = PlayerPrefs.GetInt("Coins");
    }
    public void DeductCoins(int amount)
    {
        totalCoins -= amount;
        PlayerPrefs.SetInt("Coins", totalCoins);
        setCoins();
    }
    public void setStarWon(string mode,int level , int stars)
    {
        PlayerPrefs.SetInt(mode+level,stars);
    }
    public int getstar(string mode, int level)
    {
        int stars = PlayerPrefs.GetInt(mode+level);
        return stars;
    }
}
