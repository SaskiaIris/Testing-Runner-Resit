using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class TestScript {
	private PlayerController player;
	private GameValuesUI gameValues;

	private string playerGameObjectName = "Player";
	private string canvasName = "Canvas";

	private int expectedEndScore = 340;

	[SetUp]
	public void LoadScene() {
		SceneManager.LoadScene(0);
	}

	[UnityTest]
	public IEnumerator EndScore() {
		player = GameObject.Find(playerGameObjectName).GetComponent<PlayerController>();
		gameValues = GameObject.Find(canvasName).GetComponent<GameValuesUI>();

		Assert.That(gameValues.GetScore() == 0);

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

		Assert.That(gameValues.GetScore() >= expectedEndScore);
	}
}