using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCameraViews : MonoBehaviour
{
    private CameraController playerView;
    private OverviewSeer topView;

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


    private void ToggleViewAngle()
    {
        if(playerView != null)
        {
            if(topView != null)
            {
                Debug.Log("bruj");
            }
        }
    }
}
