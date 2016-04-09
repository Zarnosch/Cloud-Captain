using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IslandManager : MonoBehaviour
{
    public enum IslandType { Small, Medium, Big }

    [ReadOnly]
    public List<GameObject> containingBuildings = new List<GameObject>();

    [ReadOnly]
    public List<GameObject> buildSides = new List<GameObject>();

    public IslandType Type;
    public GameObject Nexus { get; private set; }

    private GameObject settlementSite;

    private Dictionary<GameObject, GameObject> buildingBuildSideConnection = new Dictionary<GameObject, GameObject>();

    public GameObject GetSettlementSite()
    {
        if (settlementSite)
            return settlementSite;
        else
            return null;
    }

    // Use this for initialization
    void Start()
    {
        Nexus = null;


        FindBuildSides();

        if (Type == IslandType.Big && settlementSite)
        {
      
            AddBuilding(BuildManager.Instance.TryPlaceBuildingNoCost(BuildManager.BuildingObject.Nexus, settlementSite.transform), settlementSite);
        }


    }

    public void AddBuilding(GameObject obj, GameObject buildingSide)
    {
        Debug.Assert(obj.layer == LayerMask.NameToLayer("Buildings"));

        if (!buildSides.Contains(buildingSide) || containingBuildings.Contains(obj))
            return;

        else
        {
            buildingBuildSideConnection.Add(obj, buildingSide);
            GameObjectType type = obj.GetComponent<GameObjectType>();

            if (type.ObjectType == Setting.ObjectType.Nexus)
                this.Nexus = obj;

            containingBuildings.Add(obj);

            obj.transform.SetParent(this.gameObject.transform);

            buildingSide.SetActive(false);

            IslandReference islandReference = obj.GetComponent<IslandReference>();

            if (!islandReference)
                islandReference = obj.AddComponent<IslandReference>();

            islandReference.island = this;
        }
    }
    public void RemoveBuilding(GameObject obj)
    {
        Debug.Assert(obj.layer == LayerMask.NameToLayer("Buildings"));

        GameObjectType type = obj.GetComponent<GameObjectType>();

        if (type && containingBuildings.Contains(obj))
        {
            if (type.ObjectType == Setting.ObjectType.Nexus)
                Nexus = null;

            containingBuildings.Remove(obj);

            if (obj.transform.parent == this.gameObject)
            {
                obj.transform.SetParent(null);
            }


            GameObject buildSide = buildingBuildSideConnection[obj];
            buildSide.SetActive(true);

            buildingBuildSideConnection.Remove(obj);

            obj.GetComponent<IslandReference>().island = null;
        }
    }

    private void FindBuildSides()
    {
        List<string> layers = new List<string>();
        layers.Add("Buildingsite");
        layers.Add("Towersite");
        layers.Add("SettlementSite");


        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            string layerName = LayerMask.LayerToName(gameObject.transform.GetChild(i).gameObject.layer);

            if (layers.Contains(layerName))
            {
                if(layerName == "SettlementSite")
                {
                    settlementSite = gameObject.transform.GetChild(i).gameObject;
                }


                IslandReference reference = gameObject.transform.GetChild(i).gameObject.AddComponent<IslandReference>();
                reference.island = this;

                buildSides.Add(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }

    

}
