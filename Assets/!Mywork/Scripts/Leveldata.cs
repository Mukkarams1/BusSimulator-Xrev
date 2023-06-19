using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Scriptable Objects/level")]
public class Leveldata : ScriptableObject
{
    [SerializeField]
    public int levelNumber;
    [SerializeField]
    public string objective;
    [SerializeField]
    public gameModesEnum levelMode;
    [SerializeField]
    public GameObject LevelDataGameObject;
    [SerializeField]
    public int timeInSec;
    [SerializeField]
    public int allowedHits;
    [SerializeField]
    public int hitSpeed;
    [SerializeField]
    public int starWinTimer;
    [SerializeField]
    public List<String> StarWinningConditions;
    [SerializeField]
    public WeatherEnum levelWeather;
}
