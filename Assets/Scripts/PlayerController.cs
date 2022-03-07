using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	private CharacterController controller;
	private Vector3 moveVector;

	[SerializeField]
	private float speed = 8.0f;
	[SerializeField]
	private float verticalVelocity = 0.0f;
	[SerializeField]
	private float gravity = 9.81f;
	[SerializeField]
	private float jumpVelocity = 5.0f;

	[SerializeField]
	private string obstacleName = "Obstacle";
	[SerializeField]
	private string collectableName = "Collectable";
	[SerializeField]
	private string endName = "End";

	private bool canJump;

	private int lives;
	private int presents;

	private float trueDistance;
	private int distance;
	private int score;

	private float leftPos = -1.5f;
	private float rightPos = 1.5f;

	private int startTimerValue;

	// Start is called before the first frame update
	void Start() {
		startTimerValue = 3;
		canJump = true;
		lives = 3;
		presents = 0;
		trueDistance = 0;
		distance = 0;
		score = 0;
		controller = GetComponent<CharacterController>();
		StartCoroutine(StartTimer());
	}

	// Update is called once per frame
	void Update() {
		if(Time.timeScale != 0f) {
			CalculateDistance();
			CalculateScore();

			CheckGrounded();
			CheckInput();

			MoveCharacter();
		}
		moveVector = Vector3.zero;
	}

	private IEnumerator StartTimer() {
		Time.timeScale = 0;
		startTimerValue = 3;
		yield return new WaitForSecondsRealtime(1f);
		startTimerValue = 2;
		yield return new WaitForSecondsRealtime(1f);
		startTimerValue = 1;
		yield return new WaitForSecondsRealtime(1f);
		Time.timeScale = 1.0f;
		startTimerValue = 0;
		yield return new WaitForSecondsRealtime(0.5f);
		startTimerValue = -1;
	}

	public int GetStartTimerValue() {
		return startTimerValue;
	}

	private void CheckGrounded() {
		if(controller.isGrounded) {
			verticalVelocity = -0.5f;
			canJump = true;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}
	}

	private void CheckInput() {
		//X = Left/Right
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			MoveToLeft();
		} else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			MoveToRight();
		}

		if(Input.GetKeyDown(KeyCode.Space) && canJump) {
			verticalVelocity += jumpVelocity;
			canJump = false;
		}
	}

	private void MoveCharacter() {
		//Y = Up/Down
		moveVector.y = verticalVelocity * Time.deltaTime;

		//Z = Forward
		moveVector.z = speed * Time.deltaTime;

		controller.Move(moveVector);
		CheckWalls();
	}

	public void MoveToLeft() {
		moveVector.x = leftPos;
	}

	public void MoveToRight() {
		moveVector.x = rightPos;
	}


	public int GetLives() {
		return lives;
	}

	public int GetPresents() {
		return presents;
	}

	public int GetScore() {
		return score;
	}

	public int GetDistance() {
		return distance;
	}

	private void CheckWalls() {
		if (transform.position.x > leftPos && transform.position.x <= leftPos / 2) {
			transform.position = new Vector3(leftPos, transform.position.y, transform.position.z);
		} else if (transform.position.x > leftPos / 2 && transform.position.x < 0) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		} else if (transform.position.x > 0 && transform.position.x < rightPos / 2) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		} else if (transform.position.x >= rightPos / 2 && transform.position.x < rightPos) {
			transform.position = new Vector3(rightPos, transform.position.y, transform.position.z);
		} else if (transform.position.x > rightPos) {
			transform.position = new Vector3(rightPos, transform.position.y, transform.position.z);
		} else if (transform.position.x < leftPos) {
			transform.position = new Vector3(leftPos, transform.position.y, transform.position.z);
		}
	}

	private IEnumerator SlowMotion() {
		Time.timeScale = 0.15f;
		yield return new WaitForSecondsRealtime(0.6f);
		Time.timeScale = 0.6f;
		yield return new WaitForSecondsRealtime(0.4f);
		Time.timeScale = 1.0f;
	}

	private void GameOver() {
		Time.timeScale = 0f;
		startTimerValue = -5;
		StartCoroutine(Respawn());
	}

	private IEnumerator Respawn() {
		yield return new WaitForSecondsRealtime(0.5f);
		SceneManager.LoadScene(0);
	}

	public void LoseLife() {
		lives--;

		/*Debug.Log("Lives:" + lives);*/

		if(lives <= 0) {
			GameOver();
		} else {
			StartCoroutine(SlowMotion());
		}
	}

	void CalculateDistance() {
		trueDistance += speed * Time.deltaTime;
		distance = (int) Math.Round(trueDistance);
	}

	void CalculateScore() {
		score = distance + presents * 50;
	}

	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag(obstacleName)) {
			GameObject.Destroy(other.gameObject);
			LoseLife();
		} else if(other.CompareTag(collectableName)) {
			GameObject.Destroy(other.gameObject);
			presents++;
			/*Debug.Log("Presents: " + presents);*/
		} else if(other.CompareTag(endName)) {
			SceneManager.LoadScene(0);
		}
	}
}