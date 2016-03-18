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
        InternBuildShip(type, true);
    }

    public void BuildShipNoCost(BuildManager.ShipType type)
    {
        InternBuildShip(type, false);
    }

    private void InternBuildShip(BuildManager.ShipType type, bool cost)
    {
        if (CanBuild(type))
        {
            BuildManager.ShipInfo info = BuildManager.Instance.GetShipInfo(type);

            Res price = new Res(0, 0, 0);

            if (cost)
                price = info.price;

            if (PlayerManager.Instance.GetResources().IsEnough(price))
            {
                PlayerManager.Instance.ChangeResource(price * -1);
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
        if(Input.GetKeyDown(KeyCode.P))
        {
            BuildShipNoCost(BuildManager.ShipType.Scout);
        }

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
