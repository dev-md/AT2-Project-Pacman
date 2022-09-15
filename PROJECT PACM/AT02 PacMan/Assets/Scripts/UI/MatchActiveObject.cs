//Dylan Mount
//15/09/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchActiveObject : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;

    private void Update()
    {
        if(targetObject != null)
        {
            if (transform.GetChild(0).gameObject.activeSelf != targetObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(targetObject.activeSelf);
            }
        }
    }
}
