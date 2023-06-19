using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionBtn : MonoBehaviour
{
    [SerializeField]Transform content;
    [SerializeField] GameObject star;
    public void SetStars(int starsToenable)
    {
        for(int i = 0; i < starsToenable; i++) {

            Instantiate(star, content);
        }
    }
}
