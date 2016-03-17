using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour
{
    public GameObject SmallIsland;
    public GameObject BigIsland;
    public GameObject MidIsland1;
    public GameObject MidIsland2;

    //Lists with positions of allready placed Islands
    List<Vector3> smallIslands;
    List<Vector3> midIslands1;
    List<Vector3> midIslands2;
    List<Vector3> bigIslands;
    // Array with radius from all islands
    float[] r;

    // Use this for initialization
    void Start()
    {
        smallIslands = new List<Vector3>();
        midIslands1 = new List<Vector3>();
        midIslands2 = new List<Vector3>();
        bigIslands = new List<Vector3>();

        r = new float[]
            {
            SmallIsland.transform.lossyScale.x * Mathf.Max(SmallIsland.GetComponent<BoxCollider>().size.x, SmallIsland.GetComponent<BoxCollider>().size.z),
            BigIsland.transform.lossyScale.x * Mathf.Max(BigIsland.GetComponent<BoxCollider>().size.x, BigIsland.GetComponent<BoxCollider>().size.z),
            MidIsland1.transform.lossyScale.x * Mathf.Max(MidIsland1.GetComponent<BoxCollider>().size.x, MidIsland1.GetComponent<BoxCollider>().size.z),
            MidIsland2.transform.lossyScale.x * Mathf.Max(MidIsland2.GetComponent<BoxCollider>().size.x, MidIsland2.GetComponent<BoxCollider>().size.z)
            };

        generateIslands(20, 1, 10, 10, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Generates Islands by giving them random positions and testing if there allready exists an island <para/>
    /// Islands are placed an the positive side of x-axis and then mirrored to negative side -> fair placing <para/>
    /// Few midislands are then placed on the mirror edge
    /// </summary>
    /// <param name="smallAmount"></param>
    /// <param name="bigAmount"></param>
    /// <param name="mid1Amount"></param>
    /// <param name="mid2Amount"></param>
    /// <param name="betweenAmount"> Amount of mid1 and 2 Islands on the mirror edge </param>
    void generateIslands(int smallAmount, int bigAmount, int mid1Amount, int mid2Amount, int betweenAmount)
    {
        //Generate Islands on one side
        for (int i = 0; i < smallAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(500, 10000), 0, Random.Range(-10000, 10000));
            if (!islandCollision(pos, r[0]))
            {
                Instantiate(SmallIsland, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                smallIslands.Add(pos);
            }
            else i--;
        }
        for (int i = 0; i < bigAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(5000, 10000), 0, Random.Range(-5000, 5000));
            if (!islandCollision(pos, r[1]))
            {
                Instantiate(BigIsland, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                bigIslands.Add(pos);
            }
            else i--;
        }
        for (int i = 0; i < mid1Amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(500, 10000), 0, Random.Range(-10000, 10000));
            if (!islandCollision(pos, r[2]))
            {
                Instantiate(MidIsland1, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                midIslands1.Add(pos);
            }
            else i--;
        }
        for (int i = 0; i < mid2Amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(500, 10000), 0, Random.Range(-10000, 10000));
            if (!islandCollision(pos, r[3]))
            {
                Instantiate(MidIsland2, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                midIslands2.Add(pos);
            }
            else i--;
        }

        //Mirror all islands
        foreach (Vector3 island in smallIslands)
        {
            Instantiate(SmallIsland, island * -1, Quaternion.Euler(270, Random.Range(0, 360), 0));
        }
        foreach (Vector3 island in bigIslands)
        {
            Instantiate(BigIsland, island * -1, Quaternion.Euler(270, Random.Range(0, 360), 0));
        }
        foreach (Vector3 island in midIslands1)
        {
            Instantiate(MidIsland1, island * -1, Quaternion.Euler(270, Random.Range(0, 360), 0));
        }
        foreach (Vector3 island in midIslands2)
        {
            Instantiate(MidIsland2, island * -1, Quaternion.Euler(270, Random.Range(0, 360), 0));
        }

        //Generate Islands on the mirror edge
        for (int i = 0; i < betweenAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-400, 400), 0, Random.Range(-10000, 10000));
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                if (!islandCollision(pos, r[2]))
                {
                    Instantiate(MidIsland1, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                    midIslands1.Add(pos);
                }
                else i--;
            }
            else
            {
                if (!islandCollision(pos, r[3]))
                {
                    Instantiate(MidIsland2, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                    midIslands2.Add(pos);
                }
                else i--;
            }
        }
    }

    /// <summary>
    /// Checking collision with all other allready placed islands
    /// </summary>
    /// <param name="pos"> Position of current island </param>
    /// <param name="r1"> Radius of current island</param>
    /// <returns></returns>
    bool islandCollision(Vector3 pos, float r1)
    {
        foreach (Vector3 island in smallIslands)
        {
            if (circleCollision(pos, r1, island, r[0])) return true;
        }
        foreach (Vector3 island in bigIslands)
        {
            if (circleCollision(pos, r1, island, r[1])) return true;
        }
        foreach (Vector3 island in midIslands1)
        {
            if (circleCollision(pos, r1, island, r[2])) return true;
        }
        foreach (Vector3 island in midIslands2)
        {
            if (circleCollision(pos, r1, island, r[3])) return true;
        }

        return false;
    }

    /// <summary>
    /// Circle collision between 2 objects
    /// </summary>
    /// <param name="pos1"></param>
    /// <param name="r1"></param>
    /// <param name="pos2"></param>
    /// <param name="r2"></param>
    /// <returns></returns>
    bool circleCollision(Vector3 pos1, float r1, Vector3 pos2, float r2)
    {
        float dx = pos1.x - pos2.x;
        float dz = pos1.z - pos2.z;

        float radiusSum = r1 + r2;
        return dx * dx + dz * dz <= radiusSum * radiusSum;
    }
}
