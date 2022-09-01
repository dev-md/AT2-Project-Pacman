using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitApp : MonoBehaviour
{
    public void DebugMessage(string msg)
    {
        Application.Quit();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
