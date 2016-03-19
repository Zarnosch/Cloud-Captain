using UnityEngine;
using System.Collections;

public class testMoving : MonoBehaviour {

	public bool once = false;
	public Vector3 target = new Vector3 (-30,0,-11);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!once) {
			gameObject.GetComponent<ShipMove> ().moveShip (target);
			once = true;
		}
	}
}
