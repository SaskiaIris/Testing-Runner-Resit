using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameValuesUI : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text presentsText;

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        livesText.text = "Lives: " + playerController.GetLives();
        presentsText.text = "Presents: " + playerController.GetPresents();

    }
}