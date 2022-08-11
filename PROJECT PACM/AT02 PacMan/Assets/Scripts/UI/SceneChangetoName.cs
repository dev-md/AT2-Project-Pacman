using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangetoName : MonoBehaviour
{
    private List<string> listscenes = new List<string>();
    private void Awake()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            //print(scenes[i]);
        }
        foreach(string s in scenes)
        {
            listscenes.Add(s);
        }
    }

    public void DebugMessage(string msg)
    {
        foreach (string scene in listscenes)
        {
            //print(scene);
            if (msg == scene)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}