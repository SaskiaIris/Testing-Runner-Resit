using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestScript {
	private PlayerController player;
	private ReindeerWalk reindeer;

	private string playerGameObjectName = "Player";
	private string reindeerGameObjectName = "Reindeer Obstacle";

	private int expectedEndScore = 340;

	private int startLives = 3;
	private int startTimerStartValue = 3;
	private int expectedEndLives = 0;
	private int expectedEndPresents = 6;
	private int startTimerGameOverValue = -5;

	private float reindeerStartPosition = 1.5f;

	[SetUp]
	public void LoadScene() {
		SceneManager.LoadScene(0);
	}

	[UnityTest]
	//This test makes the player character run a perfect course and checks at the end if the score is as expected and if the player still has three lives.
	//It also checks if all presents have been collected.
	//At the beginning it checks if every value mentioned above starts at zero.
	public IEnumerator EndScore() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();

		Assert.That(player.GetScore() == 0);
		Assert.That(player.GetPresents() == 0);
		Assert.That(player.GetLives() == startLives);

		//Wait for countdown
		yield return new WaitForSecondsRealtime(3f);

		yield return new WaitForSecondsRealtime(2.7f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(0.9f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(0.05f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(0.6f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(1.7f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(3.5f);

		Assert.That(player.GetScore() >= expectedEndScore);
		Assert.That(player.GetPresents() == expectedEndPresents);
		Assert.That(player.GetLives() == startLives);
	}

	[UnityTest]
	//This test tests whether the game goes to the game over screen (and freezes) when the player has hit three obstacles (and thus lost three lives)
	public IEnumerator PlayerGameOver() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();
		Assert.That(player.GetStartTimerValue() == startTimerStartValue);

		//Wait for countdown
		yield return new WaitForSecondsRealtime(3f);

		Assert.That(player.GetLives() == startLives);
		Assert.That(Time.timeScale == 1.0);

		yield return new WaitForSecondsRealtime(0.5f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(10f);

		Assert.That(player.GetLives() == expectedEndLives);
		Assert.That(Time.timeScale == 0f);
		Assert.That(player.GetStartTimerValue() == startTimerGameOverValue);
	}

	[UnityTest]
	//This test tests whether the player slows down when they lose a life
	public IEnumerator SlowDownWhenLifeLost() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();

		//Wait for countdown
		yield return new WaitForSecondsRealtime(3f);

		Assert.That(player.GetLives() == startLives);
		Assert.That(Time.timeScale == 1.0);

		yield return new WaitForSecondsRealtime(0.5f);
		player.LoseLife();
		Assert.That(player.GetLives() == startLives - 1);
		Assert.That(Time.timeScale < 1.0f && Time.timeScale > 0.0f);
	}

	[UnityTest]
	//This test tests whether the game stays freezed during the count down and then checks if the game is unpaused
	public IEnumerator GameFreezeDuringCountDown() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();

		for(int time = 0; time < 3; time++) {
			Assert.That(Time.timeScale == 0.0f);
			Assert.That(player.GetScore() == 0);
			Assert.That(player.GetPresents() == 0);
			Assert.That(player.GetLives() == startLives);
			Assert.That(player.GetDistance() == 0);
			yield return new WaitForSecondsRealtime(1f);
		}
		Assert.That(Time.timeScale == 1.0f);
		yield return new WaitForSecondsRealtime(2f);
	}

	[UnityTest]
	//This test checks whether the reindeer moves by checking its start position and comparing to when the game has been running for a bit more than a second
	//(so it can't already be back at its start position).
	public IEnumerator WalkingReindeer() {
		reindeer = GameObject.Find(reindeerGameObjectName).GetComponent<ReindeerWalk>();
		reindeerStartPosition = reindeer.gameObject.transform.position.x;
		yield return new WaitForSecondsRealtime(4.5f);
		Assert.AreNotEqual(reindeerStartPosition, reindeer.gameObject.transform.position.x);
	}
}