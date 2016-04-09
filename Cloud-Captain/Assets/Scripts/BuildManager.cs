using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildManager : MonoBehaviour
{
    public enum BuildingObject { TeslaTower, ArtilleryTower, PowerPlant, Mine, Workshop, Shipyard, Nexus, Settlement }
    public enum ShipType { SettleShip, Scouter, SmallShip, MediumShip, BigShip }

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

    [Header("Building Prefabs")]

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


    private UnitBuildInfo[] unitInfos;

    #endregion

    #region Misc Prefabs

    [Header("Misc Prefabs")]

    public GameObject BuildingRepairerPrefab;

    public GameObject HealthbarPrefab;

    public GameObject ProgressbarPrefab;

    public GameObject HealthFeedbackParticlePrefab;

    public GameObject HitEffectPrefab;

    public GameObject ExplosionPrefab;

    public GameObject ShieldPrefab;

    #endregion

    #region Ships

    [Header("Ship Prefab")]

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

        unitInfos = new UnitBuildInfo[Enum.GetNames(typeof(Setting.ObjectType)).Length];
    }

    public GameObject TryPlaceBuilding(BuildingObject obj, Transform position)
    {
        UnitBuildInfo info = GetUnitInfo(obj.ConvertToObjectType());

        if (PlayerManager.Instance.EnoughResource(info.Price))
        {
            PlayerManager.Instance.ChangeResource(info.Price * -1);
            return (GameObject)Instantiate(info.Prefab, position.position, position.rotation);
        }

        return null;
    }

    public GameObject TryPlaceBuildingNoCost(BuildingObject obj, Transform position)
    {
        UnitBuildInfo info = GetUnitInfo(obj.ConvertToObjectType());
        return (GameObject)Instantiate(info.Prefab, position.position, position.rotation);
    }


    public UnitBuildInfo GetUnitInfo(Setting.ObjectType type)
    {
        if (unitInfos[(int)type] == null)
            unitInfos[(int)type] = CreateUnitInfo(type);

        return unitInfos[(int)type];
    }

    private UnitBuildInfo CreateUnitInfo(Setting.ObjectType type)
    {
        switch (type)
        {
            //Buildings:

            case Setting.ObjectType.Nexus:
                return new UnitBuildInfo(Setting.NEXUS_RES_COS, NexusPrefab, type);

            case Setting.ObjectType.Settlement:
                return new UnitBuildInfo(Setting.SETTLEMENT_RES_COST, SettlementPrefab, type);

            case Setting.ObjectType.PowerPlant:
                return new UnitBuildInfo(Setting.POWERPLANT_REST_COST, PowerPlantPrefab, type);

            case Setting.ObjectType.Mine:
                return new UnitBuildInfo(Setting.MINE_RES_COST, MinePrefab, type);

            case Setting.ObjectType.Shipyard:
                return new UnitBuildInfo(Setting.SHIPYARD_RES_COST, ShipyardPrefab, type);

            case Setting.ObjectType.Workshop:
                return new UnitBuildInfo(Setting.WORKSHOP_RES_COST, WorkshopPrefab, type);

            case Setting.ObjectType.TeslaTower:
                return new UnitBuildInfo(Setting.TESLA_TOWER_RES_COST, TeslaTowerPrefab, type);

            case Setting.ObjectType.ArtilleryTower:
                return new UnitBuildInfo(Setting.ARTILLERY_TOWER_RES_COST, ArtilleryTowerPrefab, type);
            
            //Ships:

            case Setting.ObjectType.Scouter:
                return new UnitBuildInfo(Setting.COST_RES_SCOUTER, ScoutShipPrefab, Setting.SCOUTER_BUILD_TIME, Setting.COST_SUPPLY_SCOUTER, type);

            case Setting.ObjectType.SettleShip:
                return new UnitBuildInfo(Setting.COST_RES_SETTLESHIP, SettleShipPrefab, Setting.SETTLESHIP_BUILD_TIME, Setting.COST_SUPPLY_SETTLESHIP, type);

            case Setting.ObjectType.SmallShip:
                return new UnitBuildInfo(Setting.COST_RES_SMALLSHIP, SmallShipPrefab, Setting.SMALLSHIP_BUILD_TIME, Setting.COST_SUPPLY_SMALLSHIP, type);

            case Setting.ObjectType.MediumShip:
                return new UnitBuildInfo(Setting.COST_RES_MEDIUMSHIP, MediumShipPrefab, Setting.MEDIUMSHIP_BUILD_TIME, Setting.COST_SUPPLY_MEDIUMSHIP, type);

            case Setting.ObjectType.BigShip:
                return new UnitBuildInfo(Setting.COST_RES_BIGSHIP, BigShipPrefab, Setting.BIGSHIP_BUILD_TIME, Setting.COST_SUPPLY_BIGSHIP, type);

            default:
                Debug.Assert(false);
                return new UnitBuildInfo();
        }

    }
    
    public class UnitBuildInfo
    {
        public Setting.ObjectType ObjType { get; private set; }
        public GameObject Prefab { get; private set; }
        public float BuildTime { get; private set; }
        public int SupplyCost { get; private set; }
        public Res Price { get; private set; }

        public UnitBuildInfo()
        {
            this.Prefab = null;
            this.Price = new Res(0, 0, 0);
            this.BuildTime = 0.0f;
            this.SupplyCost = 0;
        }

        public UnitBuildInfo(Res price, GameObject prefab, float buildTime, int supplyCost, Setting.ObjectType type)
        {
            this.Prefab = prefab;
            this.Price = price;
            this.BuildTime = buildTime;
            this.SupplyCost = supplyCost;
            this.ObjType = type;
        }

        public UnitBuildInfo(Res price, GameObject prefab, float buildTime, Setting.ObjectType type)
        {
            this.Prefab = prefab;
            this.Price = price;
            this.BuildTime = buildTime;
            this.SupplyCost = 0;
            this.ObjType = type;
        }

        public UnitBuildInfo(Res price, GameObject prefab, Setting.ObjectType type)
        {
            this.Prefab = prefab;
            this.Price = price;
            this.SupplyCost = 0;
            this.BuildTime = 0.0f;
               this.ObjType = type;
        }
    }
}
