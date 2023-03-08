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

    public void AddCoins(int coin)
    {
        wallet.totalCoins += coin;
        this.coins += coin;
    }

    public void RemoveCoins(int coin)
    {
        wallet.totalCoins -= coin;
        this.coins -= coin;
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
        coins = wallet.totalCoins;
        gems = wallet.totalGems;
    }
}
