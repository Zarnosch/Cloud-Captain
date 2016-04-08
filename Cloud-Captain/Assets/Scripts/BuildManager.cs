using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManager : MonoBehaviour
{
    public enum BuildingObject { TeslaTower, ArtilleryTower, PowerPlant, Mine, Workshop, Shipyard, Nexus, Settlement,   Count }
    public enum ShipType { Settler, Scout, Small, Medium, Big, Count}

    public static BuildManager Instance { get; private set; }

    [SerializeField]
    private bool noCostMode = false;
    [SerializeField]
    private bool instantBuild = false;
    [SerializeField]
    private bool buildAnywhere = false;

    public bool NoCostMode { get { return noCostMode; } }
    public bool InstantBuild { get { return instantBuild; } }
    public bool BuildAnywhere { get { return buildAnywhere; } }

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

    private BuildingInfo[] buildingInfos;

    #endregion

    #region Misc Prefabs

    public GameObject BuildingRepairerPrefab;

    public GameObject HealthbarPrefab;

    public GameObject ProgressbarPrefab;

    public GameObject HealthFeedbackParticlePrefab;

    public GameObject HitEffectPrefab;

    public GameObject ExplosionPrefab;

    public GameObject ShieldPrefab;

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

    private ShipInfo[] shipInfos;

    #endregion


    void Awake()
    {
        Debug.Assert(!Instance, "Only one BuildManager script is allowed in the scene!");

        Instance = this;

        buildingInfos = new BuildingInfo[(int)BuildingObject.Count];
        shipInfos = new ShipInfo[(int)ShipType.Count];
    }

    public GameObject TryPlaceBuilding(BuildingObject obj, Transform position)
    {
        BuildingInfo info = GetBuildingInfo(obj);

        if (PlayerManager.Instance.EnoughResource(info.GetPrice()))
        {

            PlayerManager.Instance.ChangeResource(info.GetPrice() * -1);

            return (GameObject)Instantiate(info.GetPrefab(), position.position, position.rotation);
        }

        return null;
    }

    public GameObject TryPlaceBuildingNoCost(BuildingObject obj, Transform position)
    {
        BuildingInfo info = GetBuildingInfo(obj);  
        return (GameObject)Instantiate(info.GetPrefab(), position.position, position.rotation);
    }

    public BuildingInfo GetBuildingInfo(BuildingObject obj)
    {
        if (buildingInfos[(int)obj] == null)
            buildingInfos[(int)obj] = CreateBuildingInfo(obj);

        return buildingInfos[(int)obj];      
    }

    private BuildingInfo CreateBuildingInfo(BuildingObject obj)
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
                Debug.Assert(false);
                return new BuildingInfo();
        }
    }

    

    public ShipInfo GetShipInfo(ShipType type)
    {
        if (shipInfos[(int)type] == null)
            shipInfos[(int)type] = CreateShipInfo(type);

        return shipInfos[(int)type];
    }

    private ShipInfo CreateShipInfo(ShipType type)
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
                Debug.Assert(false);
                return new ShipInfo();

        }
    }

 
    public class BuildingInfo
    {
        private GameObject prefab;
        private Res price;

        public GameObject GetPrefab()
        {
            return prefab;
        }

        public Res GetPrice()
        {
            return price;
        }

        public BuildingInfo()
        {
        }

        public BuildingInfo(Res price, GameObject prefab)
        {
            this.prefab = prefab;
            this.price = price;
        }
    }

    public class ShipInfo
    {
        private GameObject prefab;
        private Res price;
        private float buildTime;
        
        public GameObject GetPrefab()
        {
            return prefab;
        }

        public Res GetPrice()
        {
            return price;
        }

        public float GetBuildtime()
        {
            return buildTime;
        }

        public ShipInfo()
        {
        }

        public ShipInfo(Res price, GameObject prefab, float buildTime)
        {
            this.prefab = prefab;
            this.price = price;
            this.buildTime = buildTime;
        }
    }
}
