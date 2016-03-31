using UnityEngine;
using System.Collections;
using System;

public class ShipUpgrade : Upgrade
{
    public Setting.ObjectType Type;

    public HealthManager Health;
    public SphereCollider AttackRangeSphere;
    public BulletSpawner Spawner;
    public ShipMove ShipMove;
    public GameObject ShieldRoot;


    private int baseAttackDamage;
    private int baseHealth;
    private int slots;

    private float baseMoveSpeed;
    private float baseAttackRange;

    private float baseShieldRadius;
    private int baseShieldHealth;

    private Repairer repairer;
    private Shield shield;


    public override int GetNumMaxUpgrades()
    {
        return slots;
    }

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Damage, EUpgrade.Life, EUpgrade.MovementSpeed, EUpgrade.Range, EUpgrade.Shield, EUpgrade.BuildingRepair, EUpgrade.Shield };
    }

    protected override void OnAwake()
    {
        FillBaseValues();
        //needed because settler doesnt have a spawner:
        if (Spawner)
        {
            Spawner.BulletDamage = baseAttackDamage;
            Spawner.BulletSpeed = Setting.SHIP_BULLET_SPEED;
        }

        if(AttackRangeSphere)
            AttackRangeSphere.radius = baseAttackRange;

        if(Health)
            Health.maxHealth = baseHealth;

        if(ShipMove)
            ShipMove.speed = baseMoveSpeed;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Setting.SHIP_FLIGHT_HEIGHT, gameObject.transform.position.z);
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Shield:
                if (GetNumUpgrades(EUpgrade.Shield) == 0)
                {
                    GameObject shieldObj = (GameObject)Instantiate(BuildManager.Instance.ShieldPrefab, ShieldRoot.transform.position, Quaternion.identity);
                    this.shield = shieldObj.GetComponent<Shield>();
                    shieldObj.transform.SetParent(ShieldRoot.transform);
                    InitShield();
                }

                else
                {
                    ImproveShield();
                }
                break;

            case EUpgrade.Range:
                IncreaseRange(this.AttackRangeSphere, Setting.SHIP_RANGE_INCREASE);
                break;

            case EUpgrade.Life:
                IncreaseHealth(Health, Setting.SHIP_HEALTH_INCREASE);
                
                break;
            case EUpgrade.BuildingRepair:

                if (GetNumUpgrades(EUpgrade.BuildingRepair) == 0)
                {
                    GameObject repairer = (GameObject)Instantiate(BuildManager.Instance.BuildingRepairerPrefab, gameObject.transform.position, Quaternion.identity);
                    this.repairer = repairer.GetComponent<Repairer>();
                    repairer.transform.SetParent(this.gameObject.transform);
                    InitRepairer();
                }

                else
                {
                    ImproveRepairer();
                }

                break;
            case EUpgrade.MovementSpeed:
                ShipMove.speed *= Setting.SHIP_SPEED_INCREASE;

                break;
            case EUpgrade.Damage:
                IncreaseBulletDamage(Spawner, Setting.SHIP_DAMAGE_INCREASE);
                break;
            default:
                break;
        }
    }

    private void InitShield()
    {
        this.shield.SetRadius(baseShieldRadius);
        this.shield.SetMaxHealth(baseShieldHealth);

        this.shield.Init();
    }

    private void ImproveShield()
    {
        this.shield.MultHealth(Setting.SHIP_SHIELD_INCREASE);
    }

    private void ImproveRepairer()
    {
        ReduceFloat(ref this.repairer.maxHealCooldown, Setting.SHIP_UPGRADE_REPAIR_COOLDOWN_INCREASE);
        this.repairer.healAmount = (int)(this.repairer.healAmount * Setting.SHIP_UPGRADE_REPAIR_AMOUNT_INCREASE);
    }

    private void InitRepairer()
    {
        this.repairer.maxHealCooldown = Setting.SHIP_DEFAULT_REPAIR_COOLDOWN;
        this.repairer.healAmount = Setting.SHIP_DEFAULT_REPAIR_AMOUNT;
    }

    private void FillBaseValues()
    {
        if (Type == Setting.ObjectType.Scouter)
        {
            slots = Setting.MAX_SLOTS_SCOUTER;
            baseHealth = Setting.MAX_HEALTH_SCOUTER;
            baseAttackDamage = Setting.MAX_DMG_SCOUTER;
            baseMoveSpeed = Setting.MAX_SPEED_SCOUTER;
            baseAttackRange = Setting.MAX_RANGE_SCOUTER;

            baseShieldHealth = Setting.SHIELD_HEALTH_SCOUTER;
            baseShieldRadius = Setting.SHIELD_RADIUS_SCOUTER;
        }

        else if (Type == Setting.ObjectType.SmallShip)
        {
            slots = Setting.MAX_SLOTS_SMALLSHIP;
            baseHealth = Setting.MAX_HEALTH_SMALLSHIP;
            baseAttackDamage = Setting.MAX_DMG_SMALLSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_SMALLSHIP;
            baseAttackRange = Setting.MAX_RANGE_SMALLSHIP;

            baseShieldHealth = Setting.SHIELD_HEALTH_SMALLSHIP;
            baseShieldRadius = Setting.SHIELD_RADIUS_SMALLSHIP;
        }

        else if (Type == Setting.ObjectType.MediumShip)
        {
            slots = Setting.MAX_SLOTS_MEDIUMSHIP;
            baseHealth = Setting.MAX_HEALTH_MEDIUMSHIP;
            baseAttackDamage = Setting.MAX_DMG_MEDIUMSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_MEDIUMSHIP;
            baseAttackRange = Setting.MAX_RANGE_MEDIUMSHIP;

            baseShieldHealth = Setting.SHIELD_HEALTH_MEDIUMSHIP;
            baseShieldRadius = Setting.SHIELD_RADIUS_MEDIUMSHIP;
        }

        else if (Type == Setting.ObjectType.BigShip)
        {
            slots = Setting.MAX_SLOTS_BIGSHIP;
            baseHealth = Setting.MAX_HEALTH_BIGSHIP;
            baseAttackDamage = Setting.MAX_DMG_BIGSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_BIGSHIP;
            baseAttackRange = Setting.MAX_RANGE_BIGSHIP;

            baseShieldHealth = Setting.SHIELD_HEALTH_BIGSHIP;
            baseShieldRadius = Setting.SHIELD_RADIUS_BIGSHIP;
        }

        else if(Type == Setting.ObjectType.SettleShip)
        {

            slots = Setting.MAX_SLOTS_SETTLESHIP;
            baseHealth = Setting.MAX_HEALTH_SETTLESHIP;
            baseAttackDamage = Setting.MAX_DMG_SETTLESHIP;
            baseMoveSpeed = Setting.MAX_SPEED_SETTLESHIP;
            baseAttackRange = Setting.MAX_RANGE_SETTLESHIP;
        }

        else
            Debug.Assert(false);
    }

}
