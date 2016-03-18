using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BuildManager : MonoBehaviour
{
    public enum BuildingObject { TeslaTower, ArtilleryTower, PowerPlant, Mine, Workshop, Shipyard, Nexus, Settlement }
    public enum ShipType { Settler, Scout, Small, Medium, Big }

    public static BuildManager Instance { get; private set; }

    #region Building prefabs

    [SerializeField]
    private GameObject TeslaTowerPrefab;
    [SerializeField]
    private GameObject ArtilleryTowerPrefab;
    [SerializeField]
    private GameObject PowerPlantPrefab;
    [SerializeField]
    private GameObject MinePrefab;
    [SerializeField]
    private GameObject WorkshopPrefab;
    [SerializeField]
    private GameObject ShipyardPrefab;
    [SerializeField]
    private GameObject NexusPrefab;
    [SerializeField]
    private GameObject SettlementPrefab;

    #endregion

    #region Misc Prefabs

    public GameObject BuildingRepairerPrefab;

    public GameObject HealthbarPrefab;

    public GameObject ProgressbarPrefab;

    #endregion

    #region Ships

    [SerializeField]
    private GameObject SettleShipPrefab;
    [SerializeField]
    private GameObject ScoutShipPrefab;
    [SerializeField]
    private GameObject SmallShipPrefab;
    [SerializeField]
    private GameObject MediumShipPrefab;
    [SerializeField]
    private GameObject BigShipPrefab;

    #endregion


    void Awake()
    {
        Debug.Assert(!Instance, "Only one BuildManager script is allowed in the scene!");

        Instance = this;
    }

    public bool TryPlaceBuilding(BuildingObject obj, Transform position)
    {
        BuildingInfo info = GetBuildingInfo(obj);

        if (PlayerManager.Instance.GetResources().IsEnough(info.price))
        {
            Instantiate(info.prefab, position.position, position.rotation);
            PlayerManager.Instance.ChangeResource(info.price * -1);

            return true;
        }

        return false;
    }

    public bool TryPlaceBuildingNoCost(BuildingObject obj, Transform position)
    {
        BuildingInfo info = GetBuildingInfo(obj);
        Instantiate(info.prefab, position.position, position.rotation);
        return true;
    }

    public BuildingInfo GetBuildingInfo(BuildingObject obj)
    {
        switch (obj)
        {

            case BuildingObject.ArtilleryTower:
                return new BuildingInfo(Setting.ARTILLERY_TOWER_RES_COST, ArtilleryTowerPrefab);

            case BuildingObject.Mine:
                return new BuildingInfo(Setting.MINE_RES_COST, MinePrefab);

            case BuildingObject.PowerPlant:
                return new BuildingInfo(Setting.POWERPLANT_REST_COST, PowerPlantPrefab);

            case BuildingObject.TeslaTower:
                return new BuildingInfo(Setting.TESLA_TOWER_RES_COST, TeslaTowerPrefab);

            case BuildingObject.Workshop:
                return new BuildingInfo(Setting.WORKSHOP_RES_COST, WorkshopPrefab);

            case BuildingObject.Shipyard:
                return new BuildingInfo(Setting.SHIPYARD_RES_COST, ShipyardPrefab);

            case BuildingObject.Nexus:
                return new BuildingInfo(Setting.NEXUS_RES_COS, NexusPrefab);

            case BuildingObject.Settlement:
                return new BuildingInfo(Setting.SETTLEMENT_RES_COST, SettlementPrefab);

            default:
                return new BuildingInfo();

        }
    }

    public ShipInfo GetShipInfo(ShipType type)
    {
        switch (type)
        {
            case ShipType.Scout:
                return new ShipInfo(Setting.COST_RES_SCOUTER, ScoutShipPrefab, Setting.SCOUTER_BUILD_TIME);
   
            case ShipType.Small:
                return new ShipInfo(Setting.COST_RES_SMALLSHIP, SmallShipPrefab, Setting.SMALLSHIP_BUILD_TIME);
   
            case ShipType.Medium:
                return new ShipInfo(Setting.COST_RES_MEDIUMSHIP, MediumShipPrefab, Setting.MEDIUMSHIP_BUILD_TIME);

            case ShipType.Big:
                return new ShipInfo(Setting.COST_RES_BIGSHIP, BigShipPrefab, Setting.BIGSHIP_BUILD_TIME);

            case ShipType.Settler:
                return new ShipInfo(Setting.COST_RES_SETTLESHIP, SettleShipPrefab, Setting.SETTLESHIP_BUILD_TIME);

            default:
                return new ShipInfo();

        }
    }

 

    public struct BuildingInfo
    {
        public GameObject prefab;
        public Res price;

        public BuildingInfo(Res price, GameObject prefab)
        {
            this.prefab = prefab;
            this.price = price;
        }
    }

    public struct ShipInfo
    {
        public GameObject prefab;
        public Res price;
        public float buildTime;
        
        public ShipInfo(Res price, GameObject prefab, float buildTime)
        {
            this.prefab = prefab;
            this.price = price;
            this.buildTime = buildTime;
        }
    }
}
