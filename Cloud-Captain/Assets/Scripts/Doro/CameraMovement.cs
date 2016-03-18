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
	public float cameraMoveMaxSpeed;	//maxspeed for camera movement
	public float cameraEdgeIncreaseSpeed; //how fast the speed increases at an edge
	private float cameraHeight;
	public float cameraHeightInfluence;

	private float wheelMove;
	public float currentScroll;
	public float scrollSpeed;
	public float maxScrollSpeed;
	public float minY;
	public float maxY;
	public float rangeY;

	//------------variable
	private Vector3 _mouseCurrentPos;
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
		_moveEdgeRight = _cameraRect.x * 0.95f;
		_moveEdgeLeft = _cameraRect.x * 0.05f;
		_moveEdgeTop = _cameraRect.y * 0.95f;
		_moveEdgeBottom = _cameraRect.y * 0.05f;


	}

	void Update () {

		_mouseCurrentPos = Input.mousePosition;
		_cameraCurrentPos = camera.transform.position;

		_cameraScrollPos = _cameraCurrentPos;

		//-----------------------scroll in and out

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


	

		//------------Edge Movement
		_cameraMovePos = camera.transform.position;
		cameraHeightInfluence = 0.1f;
		cameraHeight = (camera.farClipPlane * cameraHeightInfluence - camera.nearClipPlane * cameraHeightInfluence);
		//cameraHeight = 1 +  (maxY - minY) / (_cameraMovePos.y - minY);
		//cameraHeight = _cameraMovePos;
		//move right
		if (_mouseCurrentPos.x >= _moveEdgeRight) {
			_smoothSpeedMove = (_moveEdgeRight - _mouseCurrentPos.x) *(-cameraEdgeIncreaseSpeed);
			_cameraMovePos.x += _smoothSpeedMove * cameraMoveMaxSpeed * Time.deltaTime * cameraHeight;
		}

		//move left
		if (_mouseCurrentPos.x <= _moveEdgeLeft) {
			//_smoothSpeedMove = (_mouseCurrentPos.x - _moveEdgeLeft) *(-cameraEdgeIncreaseSpeed);
			_smoothSpeedMove = (_mouseCurrentPos.x - _moveEdgeLeft) *(-cameraEdgeIncreaseSpeed);
			_cameraMovePos.x -= _smoothSpeedMove * cameraMoveMaxSpeed * Time.deltaTime * cameraHeight;
		}

		//move up
		if (_mouseCurrentPos.y >= _moveEdgeTop) {
			_smoothSpeedMove = (_moveEdgeTop - _mouseCurrentPos.y) *(-cameraEdgeIncreaseSpeed);
			_cameraMovePos.z += _smoothSpeedMove * cameraMoveMaxSpeed * Time.deltaTime;
		}

		//move down
		if (_mouseCurrentPos.y <= _moveEdgeBottom) {
			_smoothSpeedMove = (_mouseCurrentPos.y - _moveEdgeBottom) *(-cameraEdgeIncreaseSpeed);
			_cameraMovePos.z -= _smoothSpeedMove * cameraMoveMaxSpeed * Time.deltaTime;
		}

		// proof worldedges
		if (_cameraMovePos.x <= worldEdgeRight && _cameraMovePos.x >= worldEdgeLeft && _cameraMovePos.z <= worldEdgeTop && _cameraMovePos.z >= worldEdgeBottom) {
			camera.transform.position = _cameraMovePos;
		}


	}


	bool isInRangeMinY (){
		return ( (_cameraCurrentPos.y > minY - rangeY ) && (_cameraCurrentPos.y < minY + rangeY) );
	}

	bool isInRangeMaxY (){
		return ( (_cameraCurrentPos.y > maxY - rangeY ) && (_cameraCurrentPos.y < maxY + rangeY ) );
	}


}
