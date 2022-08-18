//Dylan Mount
//04/08/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewSeer : MonoBehaviour
{
    //Pos of the top camera
    [SerializeField] private GameObject gameUI;

    //Checking if the script is disable
    private void OnDisable()
    {
        //Checking Game object existing
        if(gameUI != null)
        {
            //If so disable the map
            gameUI.SetActive(false);
        }
    }

    //Checking to see if script is enable.
    private void OnEnable()
    {
        //Checking Game object existing
        if(gameUI == null)
        {
            //Turn on the map.
            gameUI.SetActive(true);
        }
    }
}
