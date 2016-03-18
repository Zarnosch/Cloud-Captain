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


    private int baseAttackDamage;
    private int baseHealth;
    private int slots;


    private float baseMoveSpeed;
    private float baseAttackRange;

    private Repairer repairer;



    public override int GetNumMaxUpgrades()
    {
        return slots;
    }

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Damage, EUpgrade.Life, EUpgrade.MovementSpeed, EUpgrade.Range, EUpgrade.Shild, EUpgrade.BuildingRepair };
    }

    protected override void OnAwake()
    {
        FillBaseValues();

        Spawner.BulletDamage = baseAttackDamage;
        Spawner.BulletSpeed = Setting.SHIP_BULLET_SPEED;

        AttackRangeSphere.radius = baseAttackRange;
        Health.maxHealth = baseHealth;
        ShipMove.speed = baseMoveSpeed;
        ShipMove.targetPosition = gameObject.transform.position;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Setting.SHIP_FLIGHT_HEIGHT, gameObject.transform.position.z);
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Shild:
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
        }

        else if (Type == Setting.ObjectType.SmallShip)
        {
            slots = Setting.MAX_SLOTS_SMALLSHIP;
            baseHealth = Setting.MAX_HEALTH_SMALLSHIP;
            baseAttackDamage = Setting.MAX_DMG_SMALLSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_SMALLSHIP;
            baseAttackRange = Setting.MAX_RANGE_SMALLSHIP;
        }

        else if (Type == Setting.ObjectType.MediumShip)
        {
            slots = Setting.MAX_SLOTS_MEDIUMSHIP;
            baseHealth = Setting.MAX_HEALTH_MEDIUMSHIP;
            baseAttackDamage = Setting.MAX_DMG_MEDIUMSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_MEDIUMSHIP;
            baseAttackRange = Setting.MAX_RANGE_MEDIUMSHIP;
        }

        else if (Type == Setting.ObjectType.BigShip)
        {
            slots = Setting.MAX_SLOTS_BIGSHIP;
            baseHealth = Setting.MAX_HEALTH_BIGSHIP;
            baseAttackDamage = Setting.MAX_DMG_BIGSHIP;
            baseMoveSpeed = Setting.MAX_SPEED_BIGSHIP;
            baseAttackRange = Setting.MAX_RANGE_BIGSHIP;
        }

        else
        {
            Debug.Assert(false);
        }
    }

}
