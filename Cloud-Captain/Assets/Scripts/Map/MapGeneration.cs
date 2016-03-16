using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// In Progress
public class MapGeneration : MonoBehaviour {
    GameObject island;
    public float gridX = 5f;
    public float gridY = 5f;
    public float spacing = 2f;

    List<GameObject> smallIslands;
    // Use this for initialization
    void Start () {
        island = GameObject.Find("smalIsland1");

        smallIslands = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {

        }

        for(int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y) * spacing;
                Instantiate(island, pos, Quaternion.Euler(270, Random.Range(10, 180), 0));
                
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("w"))
        {
            
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "smalIsland1")
        {
            Destroy(col.gameObject);
        }
    }

}
