using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Wallet")]
public class Wallet : ScriptableObject
{
    [SerializeField]
    public int totalCoins;
    [SerializeField]
    public int totalGems;
}
