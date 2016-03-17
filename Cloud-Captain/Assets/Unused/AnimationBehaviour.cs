using UnityEngine;
using System.Collections;

public class AnimationBehaviour : MonoBehaviour {

	public bool shaking = false;
	Animator animator;
	Rigidbody rigid;
	public float movementSpeed = 40;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator> ();
		rigid = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			shaking = !shaking;
			animator.SetBool ("Shaking", shaking);  
		}
			
	}

	void FixedUpdate(){

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		horizontal = horizontal * Time.deltaTime * movementSpeed;
		vertical = vertical * Time.deltaTime * movementSpeed;

		gameObject.transform.Translate (-horizontal, 0, -vertical);
		if (Input.GetKeyDown ("space"))
			rigid.AddForce (0, 500, 0);
	}
}
