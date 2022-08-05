//Dylan Mount
//04/08/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewSeer : MonoBehaviour
{
    //Pos of the top camera
    [SerializeField] private GameObject tarPos;

    private void Update()
    {
        //if the target pos is not the same as our pos.
        if(tarPos.transform.position != transform.position)
        {
            //Then goto the pos of the target with rotation
            transform.position = tarPos.transform.position;
            transform.rotation = tarPos.transform.rotation;
            
        }
    }
}
