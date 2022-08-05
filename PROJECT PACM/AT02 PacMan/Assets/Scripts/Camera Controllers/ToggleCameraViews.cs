//Dylan Mount
// 04/08/2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCameraViews : MonoBehaviour
{
    private CameraController playerView;
    private OverviewSeer topView;

    //Getting the camera compemnts 
    private void Awake()
    {
        if(TryGetComponent(out CameraController _playview) == true)
        {
            playerView = _playview;
        }
        else
        {
            Debug.Log("WHY NO CAMERA CONTROLLER");
        }

        if (TryGetComponent(out OverviewSeer _topview) == true)
        {
            topView = _topview;
        }
        else
        {
            Debug.Log("WHY NO TOPVIEW CONTROLLER");
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
        if((playerView != null)&&(topView != null))
        {
            //Toggle switch for scripts
            if (playerView.enabled == true)
            {
                playerView.enabled = false;
                topView.enabled = true;
            }
            else if (topView.enabled == true)
            {
                playerView.enabled = true;
                topView.enabled = false;
            }
        }
    }
}
