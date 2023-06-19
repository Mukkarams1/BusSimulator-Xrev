using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/Bus")]
public class Bus : ScriptableObject
{
    public string busName;
    public int busId;
    public float maxSpeed;
    public float coinsRequireToUnlock;
    public float handlingInPercentage;
    public float accelaration;
}
