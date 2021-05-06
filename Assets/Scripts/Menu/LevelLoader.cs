using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private bool pressedStart = false;
    private void Start()
    {
        Cursor.visible = false;
        StartCoroutine(LoadAsyncScene());
        pressedStart = false;
    }

    IEnumerator LoadAsyncScene()
    {
       
        while (!pressedStart)
        {
            yield return null;
        }
        AsyncOperation loadScene = SceneManager.LoadSceneAsync("ItamarLevel");
        while (!loadScene.isDone )
        {
            yield return null;
        }
    }
    public void LoadNextScene()
    {
        pressedStart = true;
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
