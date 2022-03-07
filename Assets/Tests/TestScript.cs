using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestScript {
	private PlayerController player;

	private string playerGameObjectName = "Player";

	private int expectedEndScore = 340;

	private int startLives = 3;
	private int expectedEndLives = 0;
	private int expectedEndPresents = 6;

	[SetUp]
	public void LoadScene() {
		SceneManager.LoadScene(0);
	}

	[UnityTest]
	public IEnumerator EndScore() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();

		Assert.That(player.GetScore() == 0);
		Assert.That(player.GetPresents() == 0);

		//Wait for countdown
		yield return new WaitForSecondsRealtime(3f);

		yield return new WaitForSecondsRealtime(3f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(0.5f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(0.05f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(0.6f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(1.8f);
		player.MoveToRight();
		yield return new WaitForSecondsRealtime(3.5f);

		Assert.That(player.GetScore() >= expectedEndScore);
		Assert.That(player.GetPresents() == expectedEndPresents);
	}

	[UnityTest]
	public IEnumerator PlayerGameOver() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();

		//Wait for countdown
		yield return new WaitForSecondsRealtime(3f);

		Assert.That(player.GetLives() == startLives);
		Assert.That(Time.timeScale == 1.0);

		yield return new WaitForSecondsRealtime(0.5f);
		player.MoveToLeft();
		yield return new WaitForSecondsRealtime(10f);

		Assert.That(player.GetLives() == expectedEndLives);
		Assert.That(Time.timeScale == 0f);
	}

	[UnityTest]
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
}