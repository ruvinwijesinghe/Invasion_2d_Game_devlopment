using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject titleScreen;
    public Sprite[] lives; // 0, 1, 2, 3
    public Image livesImageDisplay;
    public Text scoreText;

    public int score;


    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }// UpdateLives

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score " + score;
    }// UpdateScore

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }// ShowTitleScreen

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: 0";

    }// HideTitleScreen

}// class
