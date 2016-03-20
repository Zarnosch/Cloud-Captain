using UnityEngine;
using System.Collections;
using System;

public class NexusUpgrade : Upgrade
{
    //TODO: movementspeed, shild
    public HealthManager Health;
    public RessourceProducer Producer;

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

        Producer.TimeToResGainCooldown = Setting.NEXUS_RESSOURCE_PRODUCE_TIME;
        Producer.Res = new Res(Setting.NEXUS_MATTER_PRODUCE_AMOUNT, Setting.NEXUS_ENERGY_PRODUCE_AMOUNT, 0);
        Producer.ResGain = 1;


}

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Shild:
                break;
            case EUpgrade.Life:
                IncreaseHealth(Health, Setting.NEXUS_UPGRADE_HEALTH_INCREASE);

                break;
            case EUpgrade.MovementSpeed:
                break;

            default:
                break;
        }
    }


}
