using UnityEngine;
using System.Collections;
using System;

public class PowerPlantUpgrade : Upgrade
{
    public HealthManager Health;
    public RessourceProducer Producer;

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.ProductionSpeed };
    }

    protected override void OnAwake()
    {
        Health.SetMaxHealth(Setting.POWER_PLANT_DEFAULT_HEALTH);
        Producer.Res = new Res(0, 1, 0);
        Producer.ResGain = Setting.POWER_PLANT_DEFAULT_ENERGY_GAIN;
        Producer.TimeToResGainCooldown = Setting.POWER_PLANT_DEFAULT_ENERGY_GAIN_COOLDOWN;
    }

    public override int GetNumMaxUpgrades()
    {
        return Setting.MAX_SLOTS_MINE_POWER_PLANT;
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Life:
                IncreaseHealth(Health, Setting.POWER_PLANT_UPGRADE_HEALTH_INCREASE);
                break;

            case EUpgrade.ProductionSpeed:

                ReduceFloat(ref Producer.TimeToResGainCooldown, Setting.POWER_PLANT_UPGRADE_PRODUCTION_SPEED_INCREASE);
                break;

            default:
                break;
        }
    }



}
