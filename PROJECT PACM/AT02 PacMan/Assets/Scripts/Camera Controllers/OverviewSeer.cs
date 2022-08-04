using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewSeer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject tarPos;

    private void Update()
    {
        if(tarPos.transform.position != transform.position)
        {
            transform.position = tarPos.transform.position;
            transform.rotation = tarPos.transform.rotation;
            
        }
    }
}
