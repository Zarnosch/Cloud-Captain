using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


	//-----------static
	//Edges for the world
	public float worldEdgeLeft;
	public float worldEdgeRight;
	public float worldEdgeTop;
	public float worldEdgeBottom;

	//Edges for starting movement
	private float _moveEdgeRight;
	private float _moveEdgeLeft;
	private float _moveEdgeTop;
	private float _moveEdgeBottom;

	private Vector2 _cameraRect; // x = Width, y = Height
	private Vector3 _cameraStartPos;
	private float edgeWidth;
	public float cameraMoveSpeed = 2f;	//maxspeed for camera movement
	public float cameraEdgeIncreaseSpeed = 1f; //how fast the speed increases at an edge
	private float cameraHeight;
	public float cameraHeightInfluence = 1f;
	private float changeX;
	private float changeY;
	public float maxSpeed = 4f ;

	public bool moveByTouchingEdges = true;
	public bool allowScrolling = true;
	public bool moveByDragAndDrop = false;

	private float wheelMove;
	private float rangeY;
	private float minYRange;
	private float maxYRange;
	private float moveX;

	public float currentScroll;
	public float scrollSpeed = 1f;
	public float maxScrollSpeed;
	public float minY;
	public float maxY;

	//------------variable
	private Vector3 _mouseCurrentPos;
	private Vector3 _mouseStartPos;
	private Vector3 _cameraCurrentPos;
	private Vector3 _cameraMovePos;
	private Vector3 _cameraScrollPos;

	private float _smoothSpeedMove;
	private float _smoothSpeedScroll;

	Camera camera;


	void Start () {

		camera = GetComponent<Camera> ();

		_cameraRect.x = camera.pixelWidth;
		_cameraRect.y = camera.pixelHeight;

		//set edges starting movement
		edgeWidth = _cameraRect.x * 0.05f;
		_moveEdgeRight = _cameraRect.x - edgeWidth;
		_moveEdgeLeft = edgeWidth;
		_moveEdgeTop = _cameraRect.y -edgeWidth;
		_moveEdgeBottom = edgeWidth;

		rangeY = scrollSpeed;
		maxYRange = maxY + rangeY;
		minYRange = minY - rangeY;

	}

	void Update () {

		_cameraCurrentPos = camera.transform.position;

		if( Input.GetKeyDown("p")){
			moveByTouchingEdges = !moveByTouchingEdges;
		}

		if (allowScrolling) {
			scroll ();
		}
			
		if (moveByTouchingEdges) {
			move ();
		}

		if (moveByDragAndDrop) {
			movingByWheel ();
		}


	}

	void scroll(){
		//-----------------------scroll in and out
		_cameraScrollPos = _cameraCurrentPos;
		wheelMove = Input.GetAxis ("Mouse ScrollWheel");

		// change zoom direction
		if ( (wheelMove < 0 && currentScroll > 0) || (wheelMove > 0 && currentScroll < 0) ) {
			currentScroll = 0;
		}

		currentScroll += wheelMove;

		// regulate max speed
		if (currentScroll > maxScrollSpeed) {
			currentScroll = maxScrollSpeed;
		}
		if (currentScroll < -maxScrollSpeed) {
			currentScroll = -maxScrollSpeed;
		}

		// outfading
		if (wheelMove == 0 ) {
			currentScroll = currentScroll * 0.9f;
		}

		// edges for zooming
		if ((isInRangeMinY() && wheelMove >= 0) || (isInRangeMaxY() && wheelMove <= 0)){
			currentScroll = 0;
		}

		gameObject.transform.Translate(0,0,currentScroll * Time.deltaTime * scrollSpeed);

	}

	void move(){

		//------------Edge Movement


		_mouseCurrentPos = Input.mousePosition;
		_cameraMovePos = camera.transform.position;

		cameraHeight = _cameraMovePos.y * cameraHeightInfluence;
		//cameraHeight = (camera.nearClipPlane * cameraHeightInfluence - camera.farClipPlane * cameraHeightInfluence);
		//cameraHeight = 1 +  (maxY - minY) / (_cameraMovePos.y - minY);
		//cameraHeight = _cameraMovePos;
		//move right
		if (_mouseCurrentPos.x >= _moveEdgeRight) {
			_smoothSpeedMove = (_moveEdgeRight - _mouseCurrentPos.x) * (-cameraEdgeIncreaseSpeed) * cameraHeight;
			_cameraMovePos.x += Mathf.Sqrt(_smoothSpeedMove) * cameraMoveSpeed * Time.deltaTime;
		}

		//move left
		if (_mouseCurrentPos.x <= _moveEdgeLeft) {
			//_smoothSpeedMove = (_mouseCurrentPos.x - _moveEdgeLeft) *(-cameraEdgeIncreaseSpeed);
			_smoothSpeedMove = (_mouseCurrentPos.x - _moveEdgeLeft) *(-cameraEdgeIncreaseSpeed) * cameraHeight;
			_cameraMovePos.x -= Mathf.Sqrt(_smoothSpeedMove) * cameraMoveSpeed * Time.deltaTime;
		}

		//move up
		if (_mouseCurrentPos.y >= _moveEdgeTop) {
			_smoothSpeedMove = (_moveEdgeTop - _mouseCurrentPos.y) *(-cameraEdgeIncreaseSpeed) * cameraHeight;
			_cameraMovePos.z += Mathf.Sqrt(_smoothSpeedMove) * cameraMoveSpeed * Time.deltaTime;
		}

		//move down
		if (_mouseCurrentPos.y <= _moveEdgeBottom) {
			_smoothSpeedMove = (_mouseCurrentPos.y - _moveEdgeBottom) *(-cameraEdgeIncreaseSpeed) * cameraHeight;
			_cameraMovePos.z -= Mathf.Sqrt(_smoothSpeedMove) * cameraMoveSpeed * Time.deltaTime;
		}

		// proof worldedges
		if (_cameraMovePos.x <= worldEdgeRight && _cameraMovePos.x >= worldEdgeLeft && _cameraMovePos.z <= worldEdgeTop && _cameraMovePos.z >= worldEdgeBottom) {
			camera.transform.position = _cameraMovePos;
		}
			

	}

	void movingByWheel(){

		if (Input.GetMouseButtonDown (2)) {
			_mouseStartPos = Input.mousePosition;
		}
			
		if (Input.GetMouseButton(2)) {
			_mouseCurrentPos = Input.mousePosition;
			_cameraMovePos = _cameraCurrentPos;
			changeX = (_mouseStartPos.x - _mouseCurrentPos.x) * cameraMoveSpeed * 0.01f;
			changeY = (_mouseStartPos.y - _mouseCurrentPos.y) * cameraMoveSpeed * 0.01f;

			if (changeX > maxSpeed){
				changeX = maxSpeed;
			}
			if (changeY > maxSpeed) {
				changeY = maxSpeed;
			}

			if (changeX < -maxSpeed){
				changeX = -maxSpeed;
			}
			if (changeY < -maxSpeed) {
				changeY = -maxSpeed;
			}



			_cameraMovePos.x -= changeX;
			_cameraMovePos.z -= changeY;
			camera.transform.position = _cameraMovePos;
		}
			

	}

	bool isInRangeMinY (){
		return ( (_cameraCurrentPos.y > minYRange - rangeY ) && (_cameraCurrentPos.y < minYRange + rangeY) );
	}

	bool isInRangeMaxY (){
		return ( (_cameraCurrentPos.y > maxYRange - rangeY ) && (_cameraCurrentPos.y < maxYRange + rangeY ) );
	}


}
