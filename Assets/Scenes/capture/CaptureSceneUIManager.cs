using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class CaptureSceneUIManager : MonoBehaviour {

	[SerializeField] private CaptureSceneManager sceneManager;
	[SerializeField] private GameObject successScreen;
	[SerializeField] private GameObject failScreen;
	[SerializeField] private GameObject gameScreen;
	[SerializeField] private Text orbCountText;

	// Use this for initialization
	private void Awake () {
		Assert.IsNotNull(sceneManager);
		Assert.IsNotNull(successScreen);
		Assert.IsNotNull(failScreen);
		Assert.IsNotNull(gameScreen);
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(sceneManager.Status) {
			case CaptureScreenStatus.InProgress:
				handleInProgress();
			break;
			case CaptureScreenStatus.Successful:
				handleSuccess();
			break;
			case CaptureScreenStatus.Failed:
				handleFailure();
			break;
			default:
			break;
			
		}
	}

	private void handleInProgress() {
		updateVisibleScreen();
		orbCountText.text = sceneManager.CurrentThrowAttempts.ToString();
	}

	private void handleSuccess() {
		updateVisibleScreen();
	}

	private void handleFailure() {
		updateVisibleScreen();
	}

	private void updateVisibleScreen() {
		successScreen.SetActive(sceneManager.Status == CaptureScreenStatus.Successful);
		failScreen.SetActive(sceneManager.Status == CaptureScreenStatus.Failed);
		gameScreen.SetActive(sceneManager.Status == CaptureScreenStatus.InProgress);
	}

}
