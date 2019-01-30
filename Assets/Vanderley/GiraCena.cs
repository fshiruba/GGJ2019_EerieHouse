﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GiraCena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

public void StartGame(){
        SceneManager.LoadScene(1);
    }
    public void PauseGame(){
        Time.timeScale = 0;
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Saiu");
    }

    public void ReturnToGame(){
        SceneManager.UnloadScene(2);
        Time.timeScale = 1;
    }
}
