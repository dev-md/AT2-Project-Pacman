//Dylan Mount
// 04/08/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCameraViews : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Pacman playerScr;
    [SerializeField] private GameObject topView;

    //Getting the camera compemnts 
    private void Awake()
    {
        if(player.GetComponent<Pacman>() != null)
        {
            playerScr = player.GetComponent<Pacman>();
        }
        else
        {
            Debug.Log("WHERE PACMAN ?!?!");
        }

        if (topView == null)
        {
            Debug.Log("WHERE IS THE THING");
        }
    }

    //Checking key press to toggle view
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            ToggleViewAngle();
        }
    }


    //The function for the camera
    private void ToggleViewAngle()
    {
        //Check to see if they have the compenets
        if((player != null)&&(topView != null)&&(playerScr != null))
        {
            //Toggle switch for scripts
            if (topView.activeSelf == false)
            {
                playerScr.toggleSpeed(false);
                topView.SetActive(true);
            }
            else if (topView.activeSelf == true)
            {
                playerScr.toggleSpeed(true);
                topView.SetActive(false);
            }
        }
    }
}
