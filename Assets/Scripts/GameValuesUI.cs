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

    public PlayerController playerController;

    [SerializeField]
    private GameObject[] lives;

    private float distance;
    private int score;

    // Start is called before the first frame update
    void Start() {
        distance = 0;
        score = 0;
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

        distanceText.text = "Distance: " + Math.Round(distance);

    }

    void CalculateDistance() {
        distance += playerController.GetSpeed() * Time.deltaTime;
    }

    void CalculateScore() {

    }
}