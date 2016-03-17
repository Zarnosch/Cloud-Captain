using UnityEngine;
using System.Collections;
using System;

public class NexusUpgrade : Upgrade
{
    //TODO: movementspeed, shild
    public HealthManager Health;


    public override int GetNumMaxUpgrades()
    {
        return Setting.MAX_SLOTS_NEXUS;
    }

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.Shild, EUpgrade.MovementSpeed };
    }

    protected override void OnAwake()
    {
        Health.maxHealth = Setting.NEXUS_DEFAULT_HEALTH;
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Shild:
                break;
            case EUpgrade.Life:
                Health.SetCurAndMaxHealth((int)(Health.maxHealth * Setting.NEXUS_UPGRADE_HEALTH_INCREASE));
            
                break;
            case EUpgrade.MovementSpeed:
                break;

            default:
                break;
        }
    }


}
