using System.Collections;
using UnityEngine;

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

	private bool canJump = true;

	private int lives = 3;
	private int presents = 0;

	private float leftPos = -1.5f;
	private float rightPos = 1.5f;

	// Start is called before the first frame update
	void Start() {
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		CheckGrounded();

		CheckInput();

		MoveCharacter();
		moveVector = Vector3.zero;
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

	public float GetSpeed() {
		return speed;
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

	public IEnumerator SlowMotion() {
		Time.timeScale = 0.15f;
		yield return new WaitForSecondsRealtime(0.6f);
		Time.timeScale = 0.6f;
		yield return new WaitForSecondsRealtime(0.4f);
		Time.timeScale = 1.0f;
	}


	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag(obstacleName)) {
			GameObject.Destroy(other.gameObject);
			lives--;
			/*Debug.Log("Lives:" + lives);*/
			if(lives <= 0) {
				Time.timeScale = 0f;
			} else {
				StartCoroutine(SlowMotion());
			}
			
		} else if(other.CompareTag(collectableName)) {
			GameObject.Destroy(other.gameObject);
			presents++;
			Debug.Log("Presents: " + presents);
		}
	}
}