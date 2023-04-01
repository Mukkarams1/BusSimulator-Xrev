using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherManager : MonoBehaviour
{
    public GameObject rainyWeatherPrefab;
    public GameObject somkeyEffect;
    public Image weatherEffect;
    private Transform spawnPos;

    private void Start()
    {
        EventManager.onNewLevelLoaded += SetWeather;
    }
    private void OnDestroy()
    {
        EventManager.onNewLevelLoaded -= SetWeather;
    }
    private void OnEnable()
    {
        spawnPos = Camera.main.gameObject.transform;
        SetWeather();
    }

    private void SetWeather()
    {
        var currentWeather = LevelsDataManager.Instance.weather;
        SetWeather(currentWeather);
    }

    private void SetWeather(WeatherEnum weather)
    {
        //var randomWeather = UnityEngine.Random.Range(0, 3);
        //Debug.Log(randomWeather.ToString());
        //var modeEnm = (WeatherEnum)randomWeather;
        switch (weather)
        {
            case WeatherEnum.Rainy:
                clearOldWeather();
                Instantiate(rainyWeatherPrefab, spawnPos);
                weatherEffect.color = new Color(weatherEffect.color.r, weatherEffect.color.g, weatherEffect.color.b, 0.35f);
                break;
            case WeatherEnum.sunny:
                clearOldWeather();
                weatherEffect.color = new Color(weatherEffect.color.r, weatherEffect.color.g, weatherEffect.color.b, 0f);
                //Sunny Weather
                break;
            case WeatherEnum.Smokey:
                clearOldWeather();
                Instantiate(somkeyEffect, spawnPos);
                weatherEffect.color = new Color(weatherEffect.color.r, weatherEffect.color.g, weatherEffect.color.b, 0.3f);
                break;
            default:
                break;
        }
    }
   void clearOldWeather()
    {
        if(spawnPos.childCount > 0)
        {
            for (int i = 0; i < spawnPos.childCount; i++)
            {
                Destroy(spawnPos.GetChild(i).gameObject);
            }
        }
    }
}


public enum WeatherEnum
{
    sunny = 0,
    Rainy = 1,
    Smokey = 2,
}
