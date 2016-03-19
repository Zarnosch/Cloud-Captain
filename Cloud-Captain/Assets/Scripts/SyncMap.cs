using UnityEngine;
using System.Collections;


public class SyncMap : MonoBehaviour
{
    public float MapBorderIncrease = 0.33f;

    public MapGeneration Generation;
    public CameraMovement Movement;
    public GameObject Background;
    public GameObject Plane;

    void Awake()
    {
        Sync();
    }

	public void Sync ()
    {
       if(Generation)
        {
            Vector3 center = new Vector3(Generation.MapWidth / 2, 0.0f, Generation.MapHeight / 2);

            if (Movement)
            {
                Movement.worldEdgeLeft = -Generation.MapWidth * MapBorderIncrease;
                Movement.worldEdgeRight = Generation.MapWidth * (1.0f + MapBorderIncrease);
                Movement.worldEdgeBottom = -Generation.MapHeight * MapBorderIncrease;
                Movement.worldEdgeTop = Generation.MapHeight * (1.0f + MapBorderIncrease);

                float movementHeight = Movement.gameObject.transform.position.y;


                Vector3 startPos = Generation.bigIslandPos;
                startPos.y = Movement.gameObject.transform.position.y;
                startPos.z -= Camera.main.transform.forward.z * ;
                Movement.gameObject.transform.position = startPos; // new Vector3(center.x, movementHeight, center.z);
            }


            if (Background)
            {
                float backgroundHeight = Background.transform.position.y;
                Background.transform.position = new Vector3(center.x, backgroundHeight, center.z);
            }

            if (Plane)
            {
                Plane.transform.localScale = new Vector3(Generation.MapWidth * 0.1f, 1.0f, Generation.MapHeight * 0.1f);
            }
   
        }
	}
	

}
