using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DroidFactory : Singleton<DroidFactory> {

	[SerializeField] private Droid[] availableDroids;
	[SerializeField] private Player player;
	[SerializeField] private float waitTime = 180.0f;
	[SerializeField] private int startingDroids = 5;
	[SerializeField] private float minRange = 5.0f;
	[SerializeField] private float maxRange = 50.0f;

	private List<Droid> liveDroids = new List<Droid>();
	private Droid selectedDroid;

	public List<Droid> LiveDroids {
		get { return liveDroids; }
	}

	public Droid SelectedDroid {
		get { return selectedDroid; }
	}

	private void Awake() {
		Assert.IsNotNull(availableDroids);
		Assert.IsNotNull(player);
	}

	public void DroidWasSelected(Droid droid) {
		selectedDroid = droid;
	}

	void Start() {

		for(int i=0; i<startingDroids; ++i) {

			InstantiateDroid();

		}

		StartCoroutine(GenerateDroids());

	}

	private void InstantiateDroid() {

		int index = Random.Range(0, availableDroids.Length);
		float x = player.transform.position.x + GenerateRandomRange();
		float y = 2.11f;
		float z = player.transform.position.z + GenerateRandomRange();

		liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), (index == 0) ? Quaternion.Euler(-90.0f, 0.0f, 0.0f) : Quaternion.identity));

	}

	private IEnumerator GenerateDroids() {
		while(true) {

			InstantiateDroid();
			yield return new WaitForSeconds(waitTime);

		}
	} 

	private float GenerateRandomRange() {

		float randomNum = Random.Range(minRange, maxRange);
		bool isPositive = Random.Range(0, 10) < 5;
		return randomNum * (isPositive ? 1 : -1);

	}
	
}
