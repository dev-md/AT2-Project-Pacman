// Dylan Mount
// 27/08/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public GameObject prefab;
    //Instantiate(prefab, transform.position, Quaternion.identity);

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Grid Tile")
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
