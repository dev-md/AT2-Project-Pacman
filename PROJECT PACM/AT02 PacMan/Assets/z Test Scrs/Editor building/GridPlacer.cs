// Dylan Mount
// 27/08/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacer : MonoBehaviour
{
    public GameObject prefab;
    //Instantiate(prefab, transform.position, Quaternion.identity);

    private void Start()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerExit(Collider other)
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
