using UnityEngine;
using System.Collections;
using System;

public class TeslaTowerUpgrade : Upgrade
{
    public BulletSpawner BulletSpawner;
    public SphereCollider RangeSphere;
    public HealthManager Health;
     

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Range, EUpgrade.Damage, EUpgrade.Life };
    }

    protected override void OnAwake()
    {
        RangeSphere.radius = Setting.TESLA_TOWER_DEFAULT_ATTACK_RADIUS;
        Health.SetMaxHealth(Setting.TESLA_TOWER_DEFAULT_HEALTH);
        BulletSpawner.attackCooldown = Setting.TESLA_TOWER_DEFAULT_ATTACK_COOLDOWN;
        BulletSpawner.BulletDamage = Setting.TESLA_TOWER_DEFAULT_DAMAGE_PER_ATTACK;
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
                RangeSphere.radius *= Setting.TESLA_TOWER_UPGRADE_ATTACK_RADIUS_INCREASE;
                break;

            case EUpgrade.Life:
                Health.SetCurAndMaxHealth((int)(Health.GetMaxHealth() * Setting.TESLA_TOWER_UPGRADE_HEALTH_INCREASE));
                break;

            case EUpgrade.Damage:
                BulletSpawner.BulletDamage = (int)(BulletSpawner.BulletDamage * Setting.TESLA_TOWER_UPGRADE_DAMAGE_INCREASE);
                break;

            default:
                break;
        }
    }

    


}
