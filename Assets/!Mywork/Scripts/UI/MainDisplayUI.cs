using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainDisplayUI : MonoBehaviour
{
    [SerializeField] Text coinsText;
    // Update is called once per frame
    void Update()
    {
        coinsText.text = WalletDataManager.Instance.coins.ToString();
    }
}
