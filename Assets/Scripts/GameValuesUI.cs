using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameValuesUI : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text distanceText;
    /*[SerializeField]
    private Text livesText;*/
    [SerializeField]
    private Text presentsText;
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GameObject[] lives;

    private float trueDistance;
    private int distance;
    private int score;

    // Start is called before the first frame update
    void Start() {
        trueDistance = 0;
        distance = 0;
        score = 0;
    }

    public int GetScore() {
        return score;
    }

    public int GetDistance() {
        return distance;
    }

    // Update is called once per frame
    void Update() {
        CalculateDistance();
        CalculateScore();

        //livesText.text = "Lives: " + playerController.GetLives();
        for(int i = 0; i < lives.Length; i++) {
            if(i >= playerController.GetLives()) {
                lives[i].SetActive(false);
            } else {
                lives[i].SetActive(true);
            }
        }
        presentsText.text = /*"Presents: " + */playerController.GetPresents().ToString();

        distanceText.text = "Distance: " + distance;
        scoreText.text = "Score: " + score;
    }

    void CalculateDistance() {
        trueDistance += playerController.GetSpeed() * Time.deltaTime;
        distance = (int) Math.Round(trueDistance);
    }

    void CalculateScore() {
        score = distance + playerController.GetPresents() * 50;
    }
}