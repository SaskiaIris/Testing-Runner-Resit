using UnityEngine;
using UnityEngine.UI;

public class GameValuesUI : MonoBehaviour {
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text distanceText;
    [SerializeField]
    private Text presentsText;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GameObject[] lives;

    [SerializeField]
    private Text startTimerText;

    [SerializeField]
    private string startText = "Start";

    [SerializeField]
    private string gameOverText = "Game Over";

    private bool checkStart = true;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(checkStart) {
            if(playerController.GetStartTimerValue() > 0) {
                startTimerText.text = playerController.GetStartTimerValue().ToString();
            } else if(playerController.GetStartTimerValue() == 0) {
                startTimerText.text = startText;
            } else {
                startTimerText.text = "";
                checkStart = false;
            }
        } else if(playerController.GetStartTimerValue() <= -5) {
            startTimerText.text = gameOverText;
        }

        for(int i = 0; i < lives.Length; i++) {
            if(i >= playerController.GetLives()) {
                lives[i].SetActive(false);
            } else {
                lives[i].SetActive(true);
            }
        }
        presentsText.text = playerController.GetPresents().ToString();

        distanceText.text = "Distance: " + playerController.GetDistance();
        scoreText.text = "Score: " + playerController.GetScore();
    }
}