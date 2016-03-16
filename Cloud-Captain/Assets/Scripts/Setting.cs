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
    /// speedup for Mine
    /// </summary>
    public const int UPGARDE_MINE_SPEEDUP_PER_SEC = 2;

    /// <summary>
    /// speedup for Powerplant
    /// </summary>
    public const int UPGARDE_POWERPLANT_SPEEDUP_PER_SEC = 2;

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
    public const int COST_RES_TOWER_ENERGY = 100;
    public const int COST_RES_TOWER_MATTER = 100;
    public const int COST_RES_TOWER_ENGINE = 0;

    public const int COST_RES_NEXUS_ENERGY = 1500;
    public const int COST_RES_NEXUS_MATTER = 1500;
    public const int COST_RES_NEXUS_ENGINE = 0;

    public const int COST_RES_WORKSHOP_ENERGY = 400;
    public const int COST_RES_WORKSHOP_MATTER = 400;
    public const int COST_RES_WORKSHOP_ENGINE = 0;

    public const int COST_RES_SHIPYARD_ENERGY = 400;
    public const int COST_RES_SHIPYARD_MATTER = 400;
    public const int COST_RES_SHIPYARD_ENGINE = 0;

    public const int COST_RES_POWERPLANT_ENERGY = 200;
    public const int COST_RES_POWERPLANT_MATTER = 200;
    public const int COST_RES_POWERPLANT_ENGINE = 0;

    public const int COST_RES_MINE_ENERGY = 200;
    public const int COST_RES_MINE_MATTER = 200;
    public const int COST_RES_MINE_ENGINE = 0;

    public const int COST_RES_SETTLEMENT_ENERGY = 0;
    public const int COST_RES_SETTLEMENT_MATTER = 0;
    public const int COST_RES_SETTLEMENT_ENGINE = 0;

    /**
    ### Costs of ships
    **/
    public const int COST_RES_SETTLESHIP_ENERGY = 750;
    public const int COST_RES_SETTLESHIP_MATTER = 750;
    public const int COST_RES_SETTLESHIP_ENGINE = 2;

    public const int COST_RES_SCOUTER_ENERGY = 250;
    public const int COST_RES_SCOUTER_MATTER = 250;
    public const int COST_RES_SCOUTER_ENGINE = 0;

    public const int COST_RES_SMALLSHIP_ENERGY = 400;
    public const int COST_RES_SMALLSHIP_MATTER = 400;
    public const int COST_RES_SMALLSHIP_ENGINE = 1;

    public const int COST_RES_MEDIUMSHIP_ENERGY = 800;
    public const int COST_RES_MEDIUMSHIP_MATTER = 800;
    public const int COST_RES_MEDIUMSHIP_ENGINE = 2;

    public const int COST_RES_BIGSHIP_ENERGY = 1200;
    public const int COST_RES_BIGSHIP_MATTER = 1200;
    public const int COST_RES_BIGSHIP_ENGINE = 3;
}
