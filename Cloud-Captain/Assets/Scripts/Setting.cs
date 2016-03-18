using UnityEngine;
using System.Collections;

public static class Setting
{

	public enum ObjectType 
	{
		// eco
		Nexus, Settlement, PowerPlant, Mine, Shipyard, Workshop,
		// tower
		TeslaTower,ArtilleryTower,
		// ships
		Scouter, SettleShip, SmallShip, MediumShip, BigShip
	}

    // Buildings buildable on building area
	public static readonly BuildManager.BuildingObject[] BUILDABLE_BUILDINGS = new BuildManager.BuildingObject[] { 
		BuildManager.BuildingObject.PowerPlant, 
		BuildManager.BuildingObject.Mine,
		BuildManager.BuildingObject.Shipyard, 
		BuildManager.BuildingObject.Workshop 
	};

	// Buildings buildable on tower
	public static readonly BuildManager.BuildingObject[] BUILDABLE_TOWER = new BuildManager.BuildingObject[] { 
		BuildManager.BuildingObject.TeslaTower, 
		BuildManager.BuildingObject.ArtilleryTower 
	};
    
    /**
    #### Max upgrade slots for Buildings
    **/
    public const int MAX_SLOTS_TOWER = 4;

    public const int MAX_SLOTS_NEXUS = 2;

    public const int MAX_SLOTS_WORKSHOP = 2;

    public const int MAX_SLOTS_SHIPYARD = 2;

    public const int MAX_SLOTS_MINE_POWER_PLANT = 2;

    public const int MAX_SLOTS_SETTLEMENT = 0;

    #region SHIPS
    /********************************************* Ships */


    #region COMMON

    public const float SHIP_FLIGHT_HEIGHT = 10.0f;

    public const float SHIP_BULLET_SPEED = 25.0f;
    public const float SHIP_RANGE_INCREASE = 1.1f;
    public const float SHIP_DAMAGE_INCREASE = 1.05f;
    public const float SHIP_HEALTH_INCREASE = 1.05f;
    public const float SHIP_SPEED_INCREASE = 1.1f;
    public const float SHIP_SHILD_INCREASE = 1.1f;


    public const float SHIP_DEFAULT_REPAIR_COOLDOWN             = 5.0f;
    public const int SHIP_DEFAULT_REPAIR_AMOUNT                 = 1;
    public const float SHIP_UPGRADE_REPAIR_COOLDOWN_INCREASE    = 1.05f;
    public const float SHIP_UPGRADE_REPAIR_AMOUNT_INCREASE      = 1.05f;

    #endregion

    #region SETTLESHIP

    public const int MAX_SLOTS_SETTLESHIP = 0;

    public const int MAX_HEALTH_SETTLESHIP = 750;
    public const float MAX_SPEED_SETTLESHIP = 9f;
    public const float MAX_RANGE_SETTLESHIP = 0f;
    public const int MAX_DMG_SETTLESHIP = 0;
    public const float SETTLESHIP_BUILD_TIME = 100.0f;

    #endregion


    #region SCOUTER

    public const int MAX_SLOTS_SCOUTER = 1;

    public const int MAX_HEALTH_SCOUTER = 250;
    public const float MAX_SPEED_SCOUTER = 7f;
    public const float MAX_RANGE_SCOUTER = 50f;
    public const int MAX_DMG_SCOUTER = 10;
    public const float SCOUTER_BUILD_TIME = 2.0f;

    #endregion

    #region SMALLSHIP

    public const int MAX_SLOTS_SMALLSHIP = 2;

    public const int MAX_HEALTH_SMALLSHIP = 400;
    public const float MAX_SPEED_SMALLSHIP = 5f;
    public const float MAX_RANGE_SMALLSHIP = 60f;
    public const int MAX_DMG_SMALLSHIP = 40;
    public const float SMALLSHIP_BUILD_TIME = 100.0f;

    #endregion

    #region MEDIUMSHIP

    public const int MAX_SLOTS_MEDIUMSHIP = 4;

    public const int MAX_HEALTH_MEDIUMSHIP = 800;
    public const float MAX_SPEED_MEDIUMSHIP = 3.5f;
    public const float MAX_RANGE_MEDIUMSHIP = 60f;
    public const int MAX_DMG_MEDIUMSHIP = 60;
    public const float MEDIUMSHIP_BUILD_TIME = 100.0f;

    #endregion

    #region BISSHIP

    public const int MAX_SLOTS_BIGSHIP = 6;

    public const int MAX_HEALTH_BIGSHIP = 1200;
    public const float MAX_SPEED_BIGSHIP = 2f;
    public const float MAX_RANGE_BIGSHIP = 90f;
    public const int MAX_DMG_BIGSHIP = 100;
    public const float BIGSHIP_BUILD_TIME = 100.0f;

    #endregion

    #endregion

    /********************************************* Islands */
    /**
    #### Max ressources for the different Islands
    **/
    public const int MAX_ENERGY_SMALL_ISLAND = 7000;

    public const int MAX_ENERGY_MEDIUM_ISLAND = 10000;

    public const int MAX_ENERGY_BIG_ISLAND = 15000;

    public const int MAX_MATTER_SMALL_ISLAND = 7000;

    public const int MAX_MATTER_MEDIUM_ISLAND = 10000;

    public const int MAX_MATTER_BIG_ISLAND = 15000;



    /********************************************* Upgrades */
    /// <summary>
    /// Amount the ship or building gets with this upgrade
    /// </summary>
    public const int UPGARDE_PLUS_SHIELD = 400;
    /// <summary>
    /// amount of shieldpoints, which the shield regenerate per second
    /// </summary>
    public const int UPGARDE_REG_SHIELD_PER_SEC = 5;


    /********************************************* Costs - Eco */
    /// <summary>
    /// maximum amount of ressources, that can be hold in the bank
    /// </summary>
    public const int MAX_RES = 9999;

    /**
    ### Ressource costs of ships
    **/
    public static readonly Res COST_RES_SETTLESHIP = new Res(750, 750, 2);

    public static readonly Res COST_RES_SCOUTER = new Res(250, 250, 0);

    public static readonly Res COST_RES_SMALLSHIP = new Res(400, 400, 1);

    public static readonly Res COST_RES_MEDIUMSHIP = new Res(800, 800, 2);

    public static readonly Res COST_RES_BIGSHIP = new Res(1200, 1200, 3);

    /**
    ### Supply costs of ships
    **/
    public const int COST_SUPPLY_SETTLESHIP = 1;

    public const int COST_SUPPLY_SCOUTER = 2;

    public const int COST_SUPPLY_SMALLSHIP = 4;

    public const int COST_SUPPLY_MEDIUMSHIP = 6;

    public const int COST_SUPPLY_BIGSHIP = 8;

    /**
    ### Costs to produce one engine
    **/
    public static readonly Res COST_RES_ENGINE = new Res(250, 750, 0);
    public const float MACHINE_PRODUCE_TIME = 10.0f;

    /********************************************* Supply */

    public const int SUPPLY_MAX_OVERALL = 500;

    public const int SUPPLY_MAX_START = 100;

    public const int SUPPLY_PLUS_PER_NEXUS = 100;


    public const int START_ENERGY_AMOUNT = 10;
    public const int START_MATTER_AMOUNT = 10;
    public const int START_ENGINE_AMOUNT = 10;

    #region BUILDINGS

    #region POWER_PLANT
    /********************************************* POWER_PLANT */

    public static readonly Res POWERPLANT_REST_COST                     = new Res(200, 200, 0);

    public const int POWER_PLANT_DEFAULT_HEALTH                         = 150;
    public const float POWER_PLANT_DEFAULT_ENERGY_GAIN_COOLDOWN         = 5.0f;
    public const int POWER_PLANT_DEFAULT_ENERGY_GAIN                    = 5;

    public const float POWER_PLANT_UPGRADE_PRODUCTION_SPEED_INCREASE    = 1.05f;
    public const float POWER_PLANT_UPGRADE_HEALTH_INCREASE              = 1.05f;

    #endregion

    #region MINE
    /********************************************* MINE */

    public static readonly Res MINE_RES_COST                        = new Res(200, 200, 0);

    public const int MINE_DEFAULT_HEALTH                            = 150;
    public const float MINE_DEFAULT_MATTER_GAIN_COOLDOWN            = 5.0f;
    public const int MINE_DEFAULT_MATTER_GAIN                       = 5;

    public const float MINE_UPGRADE_PRODUCTION_SPEED_INCREASE       = 1.05f;
    public const float MINE_UPGRADE_HEALTH_INCREASE                 = 1.05f;

    #endregion

    #region TESLA_TOWER
    /********************************************* TESLA_TOWER */

    public static readonly Res TESLA_TOWER_RES_COST                 = new Res(100, 100, 0);

    public const float TESLA_TOWER_DEFAULT_ATTACK_COOLDOWN          = 0.5f;
    public const float TESLA_TOWER_DEFAULT_ATTACK_RADIUS            = 7.0f;
    public const int TESLA_TOWER_DEFAULT_DAMAGE_PER_ATTACK          = 2;
    public const int TESLA_TOWER_DEFAULT_HEALTH                     = 2;

    public const float TESLA_TOWER_UPGRADE_DAMAGE_INCREASE          = 1.05f;
    public const float TESLA_TOWER_UPGRADE_ATTACK_RADIUS_INCREASE   = 1.05f;
    public const float TESLA_TOWER_UPGRADE_HEALTH_INCREASE          = 1.05f;

    #endregion

    #region ARTILLERY_TOWER
    /********************************************* ARTILLERY_TOWER */

    public static readonly Res ARTILLERY_TOWER_RES_COST             = new Res(100, 100, 0);

    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MAX    = 20.0f;
    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MIN    = 10.0f;
    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_COOLDOWN      = 2.5f;
    public const float ARTILLERY_TOWER_DEFAULT_BULLET_SPEED         = 25.0f;
    public const float ARTILLERY_TOWER_DEFAULT_AOE_RADIUS           = 1.5f;
    public const int ARTILLERY_TOWER_DEFAULT_DAMAGE_PER_ATTACK      = 10;
    public const int ARTILLERY_TOWER_DEFAULT_AOE_DAMAGE             = 10;
    public const int ARTILLERY_TOWER_DEFAULT_HEALTH                 = 100;

    public const float ARTILLERY_TOWER_UPGRADE_DAMAGE_INCREASE      = 1.05f;
    public const float ARTILLERY_TOWER_UPGRADE_AOE_DAMAGE_INCREASE  = 1.05f;
    public const float ARTILLERY_TOWER_UPGRADE_MAX_RANGE_INCREASE   = 1.05f;
    public const float ARTILLERY_TOWER_UPGRADE_HEALTH_INCREASE      = 1.05f;

    #endregion

    #region WORKSHOP
    /********************************************* WORKSHOP */

    public static readonly Res WORKSHOP_RES_COST                    = new Res(400, 400, 0);

    public const int WORKSHOP_DEFAULT_HEALTH                        = 250;
    public const float WORKSHOP_DEFAULT_REPAIR_COOLDOWN             = 0.5f;
    public const int WORKSHOP_DEFAULT_REPAIR_AMOUNT                 = 1;

    public const float WORKSHOP_UPGRADE_REPAIR_AMOUNT_INCREASE      = 1.05f;
    public const float WORKSHOP_UPGRADE_REPAIR_COOLDOWN_INCREASE    = 1.05f;
    public const float WORKSHOP_UPGRADE_HEALTH_INCREASE             = 1.05f;

    #endregion

    #region NEXUS
    /********************************************* NEXUS */

    public static readonly Res NEXUS_RES_COS            = new Res(1500, 1500, 5);
    public const int NEXUS_DEFAULT_HEALTH               = 1500;

    public const float NEXUS_UPGRADE_HEALTH_INCREASE    = 1.05f;

    #endregion

    #region SHIPYARD
    /********************************************* SHIPYARD */

    public static readonly Res SHIPYARD_RES_COST            = new Res(400, 400, 0);
    public const int SHIPYARD_DEFAULT_HEALTH                = 250;

    public const float SHIPYARD_UPGRADE_HEALTH_INCREASE     = 1.05f;

    #endregion

    #region SETTLEMENT
    /********************************************* SETTLEMENT */

    public static readonly Res SETTLEMENT_RES_COST  = new Res(0, 0, 0);
    public const int SETTLEMENT_DEFAULT_HEALTH      = 750;



    #endregion

    #endregion



}
