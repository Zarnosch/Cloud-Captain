using UnityEngine;
using System.Collections;

public class IslandRotation : MonoBehaviour {

	public float rotationSpeed;
	public float maxRotationSpeed;

	private float currentRotation;
	private float wheelMove;
	private Vector3 moveRotation;
	private Rigidbody rigBody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate (){

		islandRotate ();

	}

	void islandRotate() {

		rigBody = gameObject.GetComponent<Rigidbody> ();

		if (Input.GetKey ("left ctrl")) {

			moveRotation = gameObject.transform.eulerAngles;

			wheelMove = Input.GetAxis ("Mouse ScrollWheel");


			currentRotation += wheelMove;

			// regulate max speed
			if (currentRotation > maxRotationSpeed) {
				currentRotation = maxRotationSpeed;
			}
			if (currentRotation < -maxRotationSpeed) {
				currentRotation = -maxRotationSpeed;
			}

			// outfading
			if (wheelMove == 0 ) {
				currentRotation = currentRotation * 0.9f;
			}

			moveRotation.y += currentRotation * rotationSpeed;

			gameObject.transform.eulerAngles = moveRotation;

		}

	}
}
