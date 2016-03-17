using UnityEngine;
using System.Collections;
using System;

public class ShipyardUpgrade : Upgrade
{
    public HealthManager Health;
    public ShipBuilder Builder;

    public override int GetNumMaxUpgrades()
    {
        return Setting.MAX_SLOTS_SHIPYARD;
    }

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.ProductionSpeed, EUpgrade.Shild };
    }

    protected override void OnAwake()
    {
        Health.maxHealth = Setting.SHIPYARD_DEFAULT_HEALTH;
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Shild:
                break;
            case EUpgrade.Life:

                IncreaseHealth(Health, Setting.SHIPYARD_UPGRADE_HEALTH_INCREASE);

                break;
            case EUpgrade.ProductionSpeed:
                break;

            default:
                break;
        }
    }
}
