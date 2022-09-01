using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletAmountCheck : MonoBehaviour
{
    private GameObject[] pelletList;

    private void Awake()
    {
        pelletList = GameObject.FindGameObjectsWithTag("Pellet");
        Debug.Log(pelletList.Length);
    }
}
