using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public bool isGameOver = true;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    // if game over is true
    // if space key pressed
    // spawn the player
    // isGameOver is false
    // hide the title screen

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    
    private void Update()
    {
        if(isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);

                isGameOver = false;

                _spawnManager.StartSpawnRoutines();

                // hide the title screen
                _uiManager.HideTitleScreen();

                _uiManager.score = 0;

            }
        }
    }// Update

}// class
