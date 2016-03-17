using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipBuilder : MonoBehaviour
{
    public BuildManager.ShipType[] BuildableShips;
    public Transform SpawnPosition;


    [ReadOnly]
    public float curBuildCooldown;
    [ReadOnly]
    public float buildReduction = 1.0f;

    private bool isBuilding = false;
 
    public Queue<BuildManager.ShipInfo> enqueuedShips = new Queue<BuildManager.ShipInfo>();

    void Start()
    {
        if (SpawnPosition == null)
            SpawnPosition = gameObject.transform;
    }

    public void BuildShip(BuildManager.ShipType type)
    {
        if (CanBuild(type))
        {
            BuildManager.ShipInfo info = BuildManager.Instance.GetShipInfo(type);

            if(PlayerManager.Instance.GetResources().IsEnough(info.price))
            {
                PlayerManager.Instance.ChangeResource(info.price * -1);
                enqueuedShips.Enqueue(info);
            }
        }
    }


	
    public bool CanBuild(BuildManager.ShipType type)
    {
        for (int i = 0; i < BuildableShips.Length; i++)
        {
            if (BuildableShips[i] == type)
                return true;
        }

        return false;
    }


    void Update()
    {
        if (isBuilding)
        {
            curBuildCooldown -= Time.deltaTime;

            if(curBuildCooldown <= 0.0f)
            {
                Instantiate(enqueuedShips.Dequeue().prefab, SpawnPosition.transform.position, Quaternion.identity);
                isBuilding = false;
            }
        }

        else if(enqueuedShips.Count > 0)
        {
            isBuilding = true;

            float reduction = 1.0f - buildReduction;
            curBuildCooldown = enqueuedShips.Peek().buildTime - (enqueuedShips.Peek().buildTime * reduction);
        }
    }
}
