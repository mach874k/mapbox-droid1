using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager {
	public override void droidTapped(GameObject droid)
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(droid);
		SceneTransitionManager.Instance.goToScene(PocketDroidConstants.SCENE_CAPTURE, list);
	}

	public override void playerTapped(GameObject player)
	{
		throw new System.NotImplementedException();
	}

}
