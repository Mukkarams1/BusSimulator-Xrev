using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletDataManager : GenericSingletonClass<WalletDataManager>
{
    [SerializeField]
    public Wallet wallet;

    [SerializeField]
    public int coins { get;  set; }

    [SerializeField]
    public int gems { get;  set; }

    private void Start()
    {
        GetCoinsAndGems();
    }

    public void AddCoins(int Amount)
    {
        wallet.totalCoins += Amount;
        this.coins += Amount;
        SaveAndLoadManager.Instance.addCoins(Amount);
    }

    public void RemoveCoins(int coin)
    {
        wallet.totalCoins -= coin;
        this.coins -= coin;
        SaveAndLoadManager.Instance.DeductCoins(coin);


    }

    public void AddGems(int Gem)
    {
        wallet.totalGems += Gem;
        this.gems += Gem;
    }
    public void RemoveGems(int Gem)
    {
        wallet.totalGems -= Gem;
        this.gems -= Gem;
    }

    public void GetCoinsAndGems()
    {
        coins = PlayerPrefs.GetInt("Coins");
        // gems = wallet.totalGems;
    }
}
