﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : Singleton<SceneTransitionManager> {

	private AsyncOperation sceneAsync;

	public void goToScene(string sceneName, List<GameObject> objectsToMove) {

		StartCoroutine(loadScene(sceneName, objectsToMove));

	}

	private IEnumerator loadScene(string sceneName, List<GameObject> objectsToMove) {

		SceneManager.LoadSceneAsync(sceneName);
		SceneManager.sceneLoaded += (newScene, mode) => {
			SceneManager.SetActiveScene(newScene);
		};

		Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
		foreach(GameObject obj in objectsToMove) {
			SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
		}

		yield return null;

	}

}
