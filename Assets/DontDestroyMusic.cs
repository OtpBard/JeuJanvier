﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyMusic : MonoBehaviour
{
    public string[] destroyOnScene;
    public string musicTag;

     void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(musicTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
             SceneManager.activeSceneChanged += OnChangeScene;
             DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnChangeScene(Scene current, Scene next)
    {
       foreach (string name in destroyOnScene)
       {
           if (name == next.name)
           {
               SceneManager.activeSceneChanged -= OnChangeScene;
               Destroy(this.gameObject);
               break;
           }
       }
    }
}
