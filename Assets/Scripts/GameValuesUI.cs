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

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        //livesText.text = "Lives: " + playerController.GetLives();
        for(int i = 0; i < lives.Length; i++) {
            if(i >= playerController.GetLives()) {
                lives[i].SetActive(false);
            } else {
                lives[i].SetActive(true);
            }
        }
        presentsText.text = /*"Presents: " + */playerController.GetPresents().ToString();

    }
}