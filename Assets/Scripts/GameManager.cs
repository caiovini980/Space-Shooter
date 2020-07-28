﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool _isGameOver = false;

    private void Update()
    {
        //if the R key is pressed
        //load the current scene
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //current game scene
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}