using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {

	public float speed;
	public float range;
	public Vector3 targetPosition;

	private Vector3 minRange;
	private Vector3 maxRange;
	private Vector3 oldTarget;
	private bool reachedTarget;

	private Rigidbody rigBody;

	// values from settings
	private Vector3 m_currentPosition;
	private Vector3 m_targetPosition;
	private float m_range;
	private float m_speed;

	// properties
	public Vector3 M_currentPosition { get; set; }
	public Vector3 M_targetPosition { get; set; }
	public float M_range {
		get {
			return m_range;
		}
		set {
			m_range = value;
			minRange = new Vector3 (m_targetPosition.x - m_range, 0, m_targetPosition.z - m_range);
			maxRange = new Vector3 (m_targetPosition.x + m_range, 0, m_targetPosition.z + m_range);
		}
	}
	public float M_speed{ get; set; }


	// Use this for initialization
	void Start () {

		m_speed = speed;
		m_targetPosition = new Vector3(0,0,0);
		m_range = range;

		minRange = new Vector3 (m_targetPosition.x - m_range, 0, m_targetPosition.z - m_range);
		maxRange = new Vector3 (m_targetPosition.x + m_range, 0, m_targetPosition.z + m_range);

		oldTarget = m_targetPosition;
		reachedTarget = true;

		rigBody = gameObject.GetComponent<Rigidbody> ();
		rigBody.velocity = new Vector3(0,0,0);
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate (){

		moveShip (targetPosition);

	}

	public void moveShip(Vector3 targetPosition){

		rigBody.isKinematic = false;
		m_targetPosition = targetPosition;
		m_currentPosition = rigBody.transform.position;

		if (!reachedTarget) {
			// currentPosition is NOT in the range of the targetPositon
			if (!isInRangeX () || !isInRangeZ ()) {

				gameObject.transform.LookAt (m_targetPosition);
				rigBody.MovePosition (transform.position + transform.forward * Time.deltaTime * m_speed);

			}

			// currentPosition is in the range of the targetPosition
			if (isInRangeX () && isInRangeZ ()) { 

				reachedTarget = true;
				oldTarget = m_targetPosition;

				rigBody.angularVelocity = new Vector3 (0, 0, 0);
				rigBody.isKinematic = true;

			}
		}

		if (reachedTarget) {
			if (!oldTarget.Equals(m_targetPosition)) {
				reachedTarget = false;
			} 
		}
	}

	bool isInRangeX (){
		return ( (m_currentPosition.x > minRange.x) && (m_currentPosition.x < maxRange.x) );
	}

	bool isInRangeZ (){
		return ( (m_currentPosition.z > minRange.z) && (m_currentPosition.z < maxRange.z) );
	}


	void OnCollisionEnter(Collision collision) {

		rigBody.MovePosition(transform.position + transform.right * Time.deltaTime * m_speed );

	}

	void OnCollisionExit(Collision collision) {

		rigBody.velocity = new Vector3(0,0,0);

	}

}
