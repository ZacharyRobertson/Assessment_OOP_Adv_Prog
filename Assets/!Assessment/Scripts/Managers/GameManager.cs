using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameActive, showResults;
    private float timer;
    public List<Player> playerList = new List<Player>();
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isGameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetSceneByName("Online").isLoaded && timer <= 90)
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
        showResults = true;
    }
    void OnGUI()
    {
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;
        if(showResults == true)
        {
            GUI.Box(new Rect(scrW * 2.5f, scrH * 1.5f,scrW * 10, scrH * 7 ), "You Survived!" + "\nScore: ");
        }
    }
}
