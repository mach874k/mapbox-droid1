using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : PocketDroidsSceneManager {

	[SerializeField] private int maxThrowAttempts = 3;
	[SerializeField] private GameObject orb;
	[SerializeField] private Vector3 spawnPoint;
	private int currentThrowAttempts = 0;
	private CaptureScreenStatus status = CaptureScreenStatus.InProgress;

	public int MaxThrowAttempts {
		get { return maxThrowAttempts; }
	}

	public int CurrentThrowAttempts {
		get { return currentThrowAttempts; }
	}

	public CaptureScreenStatus Status {
		get { return status; }
	}
	private void Start() {
		calculateMaxThrows();
		currentThrowAttempts = maxThrowAttempts;
	}

	private void calculateMaxThrows() {
		
	}

	public void OrbDestroy() {
		currentThrowAttempts--;

		if (currentThrowAttempts <= 0) {
			if(status != CaptureScreenStatus.Successful) {
				status = CaptureScreenStatus.Failed;
				Invoke("moveToWorldScene", 2.0f);
			}
		} else {
			Instantiate(orb, spawnPoint, Quaternion.identity);
		}
	}

	public override void playerTapped(GameObject player) {

	}

	public override void droidTapped(GameObject droid) {

	}

	public override void droidCollision(GameObject droid, Collision other) {

		status = CaptureScreenStatus.Successful;
		Invoke("moveToWorldScene", 2.0f);
		
	}

	private void moveToWorldScene() {
		SceneTransitionManager.Instance.goToScene(PocketDroidConstants.SCENE_WORLD, new List<GameObject>());
	}

}
