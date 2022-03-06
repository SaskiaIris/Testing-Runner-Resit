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
		Debug.Log("grounded: " + controller.isGrounded);

		moveVector = Vector3.zero;

		if(controller.isGrounded) {
			verticalVelocity = -0.5f;
			canJump = true;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}

		//X = Left/Right
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			moveVector.x = leftPos;
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			moveVector.x = rightPos;
		}

		if(Input.GetKeyDown(KeyCode.Space) && canJump) {
			verticalVelocity += jumpVelocity;
			canJump = false;
		}

		//Y = Up/Down
		moveVector.y = verticalVelocity * Time.deltaTime;

		//Z = Forward
		moveVector.z = speed * Time.deltaTime;

		controller.Move(moveVector);

		CheckWalls();
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

	public void CheckWalls() {
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

	public Vector3 MoveLeft() {
		float left = this.transform.position.x;

		left--;

		return new Vector3(left, transform.position.y, transform.position.z);
	}

	public Vector3 MoveRight() {
		float right = transform.position.x;

		right++;

		return new Vector3(right, transform.position.y, transform.position.z);
	}

	public void OnTriggerEnter(Collider other) {
		if(other.CompareTag(obstacleName)) {
			GameObject.Destroy(other.gameObject);
			lives--;
			Debug.Log("Lives:" + lives);
			if(lives <= 0) {
				Time.timeScale = 0;
			}
			
		} else if(other.CompareTag(collectableName)) {
			GameObject.Destroy(other.gameObject);
			presents++;
			Debug.Log("Presents: " + presents);
		}
	}
}