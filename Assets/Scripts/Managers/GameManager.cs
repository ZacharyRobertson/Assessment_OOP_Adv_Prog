﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameActive;
    private float timer;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isGameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetSceneByName("Online").isLoaded)
        {
            isGameActive = true;
        }
        if(isGameActive)
        {
            timer += Time.deltaTime;

            if(timer >= 90)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        //END THE GAME
        isGameActive = false;

        //Go to the results page
        SceneManager.LoadScene("Results");
    }
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        if(isGameActive ==false && SceneManager.GetSceneByName("Results").isLoaded)
        {
            GUI.Box(new Rect(), "");
        }
    }
}
