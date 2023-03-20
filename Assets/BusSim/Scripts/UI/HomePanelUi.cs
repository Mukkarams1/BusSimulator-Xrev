using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomePanelUi : MonoBehaviour
{
    [SerializeField] Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(exitApplication);
    }

    void exitApplication()
    {
        Application.Quit();
    }
}
