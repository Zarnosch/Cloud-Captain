using UnityEngine;
using System.Collections;


public class SyncMap : MonoBehaviour {


    public MapGeneration Generation;
    public CameraMovement Movement;


	void Awake ()
    {
       if(Generation && Movement)
        {
            Movement.worldEdgeLeft = 1;
            Movement.worldEdgeRight = Generation.MapWidth;
            Movement.worldEdgeBottom = 1;
            Movement.worldEdgeTop = Generation.MapHeight;

            Movement.gameObject.transform.position = new Vector3(Generation.MapWidth / 2, Movement.gameObject.transform.position.y, Generation.MapHeight / 2);
        }
	}
	

}
