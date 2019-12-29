using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OverrideOrb : MonoBehaviour {

	[SerializeField] private float throwSpeed = 30.0f;
	[SerializeField] private float collisionStallTime = 2.0f;
	[SerializeField] private float stallTime = 5.0f;
	[SerializeField] private AudioClip dropSound;
	[SerializeField] private AudioClip successSound;
	[SerializeField] private AudioClip throwSound;

	private float lastX;
	private float lastY;
	private bool released;
	private bool holding;
	private bool trackingCollisions = false;
	private Rigidbody rigidbody;
	private AudioSource audioSource;

	private InputStatus inputStatus;

	private enum InputStatus {
		Grabbing, 
		Holding, 
		Releasing,
		None
	}

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		rigidbody = GetComponent<Rigidbody>();

		Assert.IsNotNull(audioSource);
		Assert.IsNotNull(rigidbody);
		Assert.IsNotNull(dropSound);
		Assert.IsNotNull(successSound);
		Assert.IsNotNull(throwSound);
	}

	private void Update() {

		if (released)
			return;

		if (holding) {
			followInput();
		}

		updateInputStatus();

		switch(inputStatus) {
			case InputStatus.Grabbing:
				grab();
				break;
			case InputStatus.Holding:
				drag();
				break;
			case InputStatus.Releasing:
				release();
				break;
			default:
				break;
			
		}

	}

	private void updateInputStatus() {
		#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0)) {
			inputStatus = InputStatus.Grabbing;
		} else if(Input.GetMouseButton(0)) {
			inputStatus = InputStatus.Holding;
		} else if(Input.GetMouseButtonUp(0)) {
			inputStatus = InputStatus.Releasing;
		} else {
			inputStatus = InputStatus.None;
		}
		#endif
		#if NOT_UNITY_EDITOR
		if (Input.GetTouch(0).phase == TouchPhase.Began) {
			inputStatus = InputStatus.Grabbing;
		} else if(Input.GetTouch(0).phase == TouchPhase.Ended) {
			inputStatus = InputStatus.Releasing;
		} else if(Input.touchCount == 1) {
			inputStatus = InputStatus.Holding;
		} else {
			inputStatus = InputStatus.None;
		}
		#endif
	}

	private void followInput() {
		Vector3 input = getInputPosition();
		input.z = Camera.main.nearClipPlane * 7.5f;
		Vector3 pos = Camera.main.ScreenToWorldPoint(input);

		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, 50.0f * Time.deltaTime);
	}

	private void grab() {
		Ray ray = Camera.main.ScreenPointToRay(getInputPosition());
		RaycastHit point;

		if(Physics.Raycast(ray, out point, 100.0f) && point.transform == transform) {
			holding = true;
			transform.parent = null;
		}
	}

	private void drag() {
		lastX = getInputPosition().x;
		lastY = getInputPosition().y;

	}

	private void release() {
		if(lastY < getInputPosition().y) {
			throwBall(getInputPosition());
		}
	}

	private Vector2 getInputPosition() {
		Vector2 result = new Vector2();

		#if UNITY_EDITOR
		result = Input.mousePosition;
		#endif
		#if NOT_UNITY_EDITOR
		result = Input.GetTouch(0).position;
		#endif

		return result;
	}

	private void throwBall(Vector2 targetPosition) {
		rigidbody.useGravity = true;
		trackingCollisions = true;

		float yDiff = (targetPosition.y - lastY) / Screen.height * 100.0f;
		float speed = throwSpeed * yDiff;

		float x = (targetPosition.x - lastX) / Screen.width;
		x = Mathf.Abs(getInputPosition().x - lastX) / Screen.width * 100.0f * x;

		Vector3 direction = new Vector3(x, 0.0f, 1.0f);
		direction = Camera.main.transform.TransformDirection(direction);
		rigidbody.AddForce((direction* speed)/2.0f + Vector3.up * speed);

		audioSource.PlayOneShot(throwSound);
		released = true;
		holding = false;

		Invoke("powerDown", stallTime);
	}

	private void powerDown() {
		CaptureSceneManager manager = FindObjectOfType<CaptureSceneManager>();
		if(manager != null)
			manager.OrbDestroy();
			
		Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision other) {

		if(!trackingCollisions)
			return;

		trackingCollisions = false;
		if(other.gameObject.CompareTag(PocketDroidConstants.TAG_DROID)) {
			audioSource.PlayOneShot(successSound);
		} else {
			audioSource.PlayOneShot(dropSound);
		}

		Invoke("powerDown", collisionStallTime);

	}


}
