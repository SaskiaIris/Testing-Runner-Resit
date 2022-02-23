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

	private bool canJump = true;

	// Start is called before the first frame update
	void Start() {
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update() {
		moveVector = Vector3.zero;

		if (controller.isGrounded) {
			verticalVelocity = -0.5f;
			canJump = true;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
		}

		//X = Left/Right
		if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			moveVector.x = -1;
		} else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			moveVector.x = 1;
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

	public void CheckWalls() {
		if (transform.position.x > -1 && transform.position.x <= -0.5) {
			transform.position = new Vector3(-1, transform.position.y, transform.position.z);
		} else if (transform.position.x > -0.5 && transform.position.x < 0) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		} else if (transform.position.x > 0 && transform.position.x < 0.5) {
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
		} else if (transform.position.x >= 0.5 && transform.position.x < 1) {
			transform.position = new Vector3(1, transform.position.y, transform.position.z);
		} else if (transform.position.x > 1) {
			transform.position = new Vector3(1, transform.position.y, transform.position.z);
		} else if (transform.position.x < -1) {
			transform.position = new Vector3(-1, transform.position.y, transform.position.z);
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
}