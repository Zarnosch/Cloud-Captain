using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour
{
    public bool generateLevelOnStart = false;
    public bool appendToGameObject = false;

    public GameObject SmallIsland;
    public GameObject BigIsland;
    public GameObject MidIsland1;
    public GameObject MidIsland2;

    public float MapHeight;
    public float MapWidth;
    public float MirrorEdgeWidth;
    public int SmallIslandAmount;
    public int MidIsland1Amount;
    public int MidIsland2Amount;
    public int MirrorEdgeIslandsAmount;
    private int BigIslandAmount = 1; // only one big island per side

    // to prevent endless loops
    public int MaxTries;
    bool notWholeMapCreated = false;

    //Lists with positions of allready placed Islands
    List<Vector3> smallIslands;
    List<Vector3> midIslands1;
    List<Vector3> midIslands2;
    List<Vector3> bigIslands;
    // Array with radius from all islands
    float[] r;

    private GameObject rootObject;

    // To check if Map is allready generated
    bool allreadyGenerated;

    void Start()
    {
        if (generateLevelOnStart)
            GenerateWorld();
    }

    public void DestroyWorld()
    {
        while(gameObject.transform.childCount > 0)
        {
            #if UNITY_EDITOR
                DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
            #else
                Destroy(gameObject.transform.GetChild(i)); 
            #endif
        }
    }

    public void GenerateWorld()
    {
        // To check if Map is allready generated
        if (!allreadyGenerated)
        {
            DestroyWorld();
        }
        else
            allreadyGenerated = true;

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

        generateIslands(SmallIslandAmount, BigIslandAmount, MidIsland1Amount, MidIsland2Amount, MirrorEdgeIslandsAmount);
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
        int maxTries = 0;
        //Generate Islands on one side
        for (int i = 0; i < smallAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(MapWidth / 2 + MirrorEdgeWidth / 2, MapWidth), 0, Random.Range(0, MapHeight));
            if (!islandCollision(pos, r[0]))
            {
                GameObject newObj = (GameObject)Instantiate(SmallIsland, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                smallIslands.Add(pos);

                if (appendToGameObject)
                    newObj.transform.SetParent(gameObject.transform);

            }
            else
            {
                i--;
                maxTries++;
                if(maxTries >= MaxTries)
                {
                    maxTries = 0;
                    notWholeMapCreated = true;
                    break;
                }
            }
        }
        for (int i = 0; i < bigAmount; i++)
        {
            // Positions in the middle 1/2 of the map
            Vector3 pos = new Vector3(Random.Range(MapWidth * 0.75f, MapWidth), 0, Random.Range(MapHeight / 4, MapHeight * 0.75f));
            if (!islandCollision(pos, r[1]))
            {
                GameObject newObj = (GameObject)Instantiate(BigIsland, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                bigIslands.Add(pos);

                if (appendToGameObject)
                    newObj.transform.SetParent(gameObject.transform);
            }
            else
            {
                i--;
                maxTries++;
                if (maxTries >= MaxTries)
                {
                    maxTries = 0;
                    notWholeMapCreated = true;
                    break;
                }
            }
        }
        for (int i = 0; i < mid1Amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(MapWidth / 2 + MirrorEdgeWidth / 2, MapWidth), 0, Random.Range(0, MapHeight));
            if (!islandCollision(pos, r[2]))
            {
                GameObject newObj = (GameObject)Instantiate(MidIsland1, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                midIslands1.Add(pos);


                if (appendToGameObject)
                    newObj.transform.SetParent(gameObject.transform);
            }
            else
            {
                i--;
                maxTries++;
                if (maxTries >= MaxTries)
                {
                    maxTries = 0;
                    notWholeMapCreated = true;
                    break;
                }
            }
        }
        for (int i = 0; i < mid2Amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(MapWidth / 2 + MirrorEdgeWidth / 2, MapWidth), 0, Random.Range(0, MapHeight));
            if (!islandCollision(pos, r[3]))
            {
                GameObject newObj = (GameObject)Instantiate(MidIsland2, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                midIslands2.Add(pos);


                if (appendToGameObject)
                    newObj.transform.SetParent(gameObject.transform);
            }
            else
            {
                i--;
                maxTries++;
                if (maxTries >= MaxTries)
                {
                    maxTries = 0;
                    notWholeMapCreated = true;
                    break;
                }
            }
        }

        //Mirror all islands
        foreach (Vector3 island in smallIslands)
        {
            GameObject newObj = (GameObject)Instantiate(SmallIsland, new Vector3(MapWidth - island.x, island.y, MapHeight - island.z), Quaternion.Euler(270, Random.Range(0, 360), 0));

            if (appendToGameObject)
                newObj.transform.SetParent(gameObject.transform);
        }
        foreach (Vector3 island in bigIslands)
        {
            GameObject newObj = (GameObject)Instantiate(BigIsland, new Vector3(MapWidth - island.x, island.y, MapHeight - island.z), Quaternion.Euler(270, Random.Range(0, 360), 0));

            if (appendToGameObject)
                newObj.transform.SetParent(gameObject.transform);
        }
        foreach (Vector3 island in midIslands1)
        {
            GameObject newObj = (GameObject)Instantiate(MidIsland1, new Vector3(MapWidth - island.x, island.y, MapHeight - island.z), Quaternion.Euler(270, Random.Range(0, 360), 0));

            if (appendToGameObject)
                newObj.transform.SetParent(gameObject.transform);
        }
        foreach (Vector3 island in midIslands2)
        {
            GameObject newObj = (GameObject)Instantiate(MidIsland2, new Vector3(MapWidth - island.x, island.y, MapHeight - island.z), Quaternion.Euler(270, Random.Range(0, 360), 0));

            if (appendToGameObject)
                newObj.transform.SetParent(gameObject.transform);
        }
        
        //Generate Islands on the mirror edge
        for (int i = 0; i < betweenAmount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(MapWidth/2 - MirrorEdgeWidth/2, MapWidth / 2 + MirrorEdgeWidth / 2), 0, Random.Range(0, MapHeight));
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                if (!islandCollision(pos, r[2]))
                {
                    GameObject newObj = (GameObject)Instantiate(MidIsland1, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                    midIslands1.Add(pos);

                    if (appendToGameObject)
                        newObj.transform.SetParent(gameObject.transform);
                }
                else
                {
                    i--;
                    maxTries++;
                    if (maxTries >= MaxTries)
                    {
                        maxTries = 0;
                        notWholeMapCreated = true;
                        break;
                    }
                }
            }
            else
            {
                if (!islandCollision(pos, r[3]))
                {
                    GameObject newObj = (GameObject)Instantiate(MidIsland2, pos, Quaternion.Euler(270, Random.Range(0, 360), 0));
                    midIslands2.Add(pos);

                    if (appendToGameObject)
                        newObj.transform.SetParent(gameObject.transform);
                }
                else
                {
                    i--;
                    maxTries++;
                    if (maxTries >= MaxTries)
                    {
                        maxTries = 0;
                        notWholeMapCreated = true;
                        break;
                    }
                }
            }
        }

        if(notWholeMapCreated) Debug.LogError("Could not create whole map. Check wether there is enough space for all islands.");
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
