using UnityEngine;
using System.Collections;
using System;

public class ArtilleryUpgrade : Upgrade
{
    public HealthManager Health;
    public SphereCollider AttackRadius;
    public BulletSpawner BulletSpawner;


    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.Range, EUpgrade.Damage };
    }

    protected override void OnAwake()
    {
        Health.SetMaxHealth(Setting.ARTILLERY_TOWER_DEFAULT_HEALTH);
        AttackRadius.radius = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MAX;
        BulletSpawner.BulletDamage = Setting.ARTILLERY_TOWER_DEFAULT_DAMAGE_PER_ATTACK;
        BulletSpawner.MinDistance = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MIN;
        BulletSpawner.attackCooldown = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_COOLDOWN;
        BulletSpawner.BulletSpeed = Setting.ARTILLERY_TOWER_DEFAULT_BULLET_SPEED;
        BulletSpawner.SecondaryDamage = Setting.ARTILLERY_TOWER_DEFAULT_AOE_DAMAGE;
        BulletSpawner.SecondaryRange = Setting.ARTILLERY_TOWER_DEFAULT_AOE_RADIUS;
    }

    public override int GetNumMaxUpgrades()
    {
        return Setting.MAX_SLOTS_TOWER;
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Range:
                IncreaseRange(AttackRadius, Setting.ARTILLERY_TOWER_UPGRADE_MAX_RANGE_INCREASE);
                break;
            case EUpgrade.Life:
                IncreaseHealth(Health, Setting.ARTILLERY_TOWER_UPGRADE_HEALTH_INCREASE);
                break;
            case EUpgrade.Damage:
                IncreaseBulletDamage(BulletSpawner, Setting.ARTILLERY_TOWER_UPGRADE_DAMAGE_INCREASE);
                BulletSpawner.SecondaryDamage = (int)(BulletSpawner.SecondaryDamage * Setting.ARTILLERY_TOWER_UPGRADE_AOE_DAMAGE_INCREASE);
                break;
            default:
                break;
        }

    }

  


}
