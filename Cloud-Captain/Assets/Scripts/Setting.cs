using UnityEngine;
using System.Collections;

public static class Setting
{

    public const float BULLETSPEED = 0;
    
    /********************************************* Buildings */
    /**
    #### Max Health for Buildings
    **/
    public const int MAX_HEALTH_TOWER = 500;

    public const int MAX_HEALTH_NEXUS = 1500;

    public const int MAX_HEALTH_WORKSHOP = 250;

    public const int MAX_HEALTH_SHIPYARD = 250;

    public const int MAX_HEALTH_POWERPLANT = 150;

    public const int MAX_HEALTH_MINE = 150;

    public const int MAX_HEALTH_SETTLEMENT = 750;

    /**
    #### Max upgrade slots for Buildings
    **/
    public const int MAX_SLOTS_TOWER = 6;

    public const int MAX_SLOTS_NEXUS = 2;

    public const int MAX_SLOTS_WORKSHOP = 2;

    public const int MAX_SLOTS_SHIPYARD = 2;

    public const int MAX_SLOTS_POWERPLANT = 2;

    public const int MAX_SLOTS_MINE = 2;

    public const int MAX_SLOTS_SETTLEMENT = 0;

    /********************************************* Ships */
    /**
    #### Max Health for Ships
    **/
    public const int MAX_HEALTH_SETTLESHIP = 750;

    public const int MAX_HEALTH_SCOUTER = 250;

    public const int MAX_HEALTH_SMALLSHIP = 400;

    public const int MAX_HEALTH_MEDIUMSHIP = 800;

    public const int MAX_HEALTH_BIGSHIP = 1200;

    /**
    #### Max MovementSpeed for Ships
    **/
    public const float MAX_SPEED_SETTLESHIP = 9f;

    public const float MAX_SPEED_SCOUTER = 7f;

    public const float MAX_SPEED_SMALLSHIP = 5f;

    public const float MAX_SPEED_MEDIUMSHIP = 3.5f;

    public const float MAX_SPEED_BIGSHIP = 2f;

    /**
    #### Max attack range for Ships
    **/
    public const float MAX_RANGE_SETTLESHIP = 0f;

    public const float MAX_RANGE_SCOUTER = 50f;

    public const float MAX_RANGE_SMALLSHIP = 60f;

    public const float MAX_RANGE_MEDIUMSHIP = 60f;

    public const float MAX_RANGE_BIGSHIP = 90f;

    /**
    #### Max damage per attack for Ships
    **/
    public const int MAX_DMG_SETTLESHIP = 0;

    public const int MAX_DMG_SCOUTER = 10;

    public const int MAX_DMG_SMALLSHIP = 40;

    public const int MAX_DMG_MEDIUMSHIP = 60;

    public const int MAX_DMG_BIGSHIP = 100;

    /**
    #### Max upgrade slots for Ships
    **/
    public const int MAX_SLOTS_SETTLESHIP = 0;

    public const int MAX_SLOTS_SCOUTER = 1;

    public const int MAX_SLOTS_SMALLSHIP = 2;

    public const int MAX_SLOTS_MEDIUMSHIP = 4;

    public const int MAX_SLOTS_BIGSHIP = 6;

    /**
    #### Special things for buildings
    **/
    /// <summary>
    /// a ship heals with this amount other ships
    /// </summary>
    public const int MAX_REPAIR_WORKSHOP_PER_SEC = 3;


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

    /// <summary>
    /// the upgrade adds that much health to the building / ship
    /// </summary>
    public const int UPGARDE_PLUS_HEALTH = 600;

    /// <summary>
    /// a ship heals with this amount buildings and other ships (only one per upgrade)
    /// </summary>
    public const int UPGARDE_REPAIR_PER_SEC = 3;

    /// <summary>
    /// the additional damage a tower or ship does with this upgrade
    /// </summary>
    public const int UPGARDE_DAMAGE = 25;

    /********************************************* Costs - Eco */
    /// <summary>
    /// maximum amount of ressources, that can be hold in the bank
    /// </summary>
    public const int MAX_RES = 9999;

    /**
    ### Costs of buildings
    **/
    public static readonly Res COST_RES_TOWER = new Res(100, 100, 0);

    public static readonly Res COST_RES_NEXUS = new Res(1500, 1500, 5);

    public static readonly Res COST_RES_WORKSHOP = new Res(400, 400, 0);

    public static readonly Res COST_RES_SHIPYARD = new Res(400, 400, 0);

    public static readonly Res COST_RES_POWERPLANT = new Res(200, 200, 0);

    public static readonly Res COST_RES_MINE = new Res(200, 200, 0);

    public static readonly Res COST_RES_SETTLEMENT = new Res(0, 0, 0);


    /**
    ### Ressource costs of ships
    **/
    public static readonly Res COST_SUPPLY_SETTLESHIP = new Res(750, 750, 2);

    public static readonly Res COST_SUPPLY_SCOUTER = new Res(250, 250, 0);

    public static readonly Res COST_SUPPLY_SMALLSHIP = new Res(400, 400, 1);

    public static readonly Res COST_SUPPLY_MEDIUMSHIP = new Res(800, 800, 2);

    public static readonly Res COST_SUPPLY_BIGSHIP_ENERGY = new Res(1200, 1200, 3);

    /**
    ### Supply costs of ships
    **/
    public const int COST_RES_SETTLESHIP = 1;

    public const int COST_RES_SCOUTER = 2;

    public const int COST_RES_SMALLSHIP = 4;

    public const int COST_RES_MEDIUMSHIP = 6;

    public const int COST_RES_BIGSHIP_ENERGY = 8;

    /**
    ### Costs to produce one engine
    **/
    public static readonly Res COST_RES_ENGINE = new Res(250, 750, 0);


    /********************************************* Supply */

    public const int SUPPLY_MAX_OVERALL = 500;

    public const int SUPPLY_MAX_START = 100;

    public const int SUPPLY_PLUS_PER_NEXUS = 100;



    #region POWER_PLANT
    /********************************************* POWER_PLANT */

    public const float POWER_PLANT_DEFAULT_ENERGY_GAIN_COOLDOWN = 5.0f;
    public const int POWER_PLANT_DEFAULT_ENERGY_GAIN            = 5;

    #endregion


    #region MINE
    /********************************************* MINE */

    public const float MINE_DEFAULT_MATTER_GAIN_COOLDOWN    = 5.0f;
    public const int MINE_DEFAULT_MATTER_GAIN               = 5;

    #endregion


    #region TESLA_TOWER
    /********************************************* TESLA_TOWER */

    public const float TESLA_TOWER_DEFAULT_ATTACK_COOLDOWN  = 0.5f;
    public const float TESLA_TOWER_DEFAULT_ATTACK_RADIUS    = 7.0f;
    public const int TESLA_TOWER_DEFAULT_DAMAGE_PER_ATTACK  = 2;
    public const int TESLA_TOWER_DEFAULT_HEALTH             = 100;

    #endregion


    #region ARTILLERY_TOWER
    /********************************************* ARTILLERY_TOWER */

    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MAX    = 20.0f;
    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MIN    = 10.0f;
    public const float ARTILLERY_TOWER_DEFAULT_ATTACK_COOLDOWN      = 2.5f;
    public const float ARTILLERY_TOWER_DEFAULT_BULLET_SPEED         = 25.0f;
    public const float ARTILLERY_TOWER_DEFAULT_AOE_RADIUS           = 1.5f;
    public const int ARTILLERY_TOWER_DEFAULT_DAMAGE_PER_ATTACK      = 10;
    public const int ARTILLERY_TOWER_DEFAULT_AOE_DAMAGE             = 10;
    public const int ARTILLERY_TOWER_DEFAULT_HEALTH                 = 100;

    #endregion

}
