using UnityEngine;
using System.Collections;
using System;

public class MineUpgrade : Upgrade
{
    public HealthManager Health;
    public RessourceProducer Producer;

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.ProductionSpeed };
    }

    protected override void OnAwake()
    {
        Health.SetMaxHealth(Setting.MINE_DEFAULT_HEALTH);
        Producer.Res = new Res(1, 0, 0);
        Producer.ResGain = Setting.MINE_DEFAULT_MATTER_GAIN;
        Producer.TimeToResGainCooldown = Setting.MINE_DEFAULT_MATTER_GAIN_COOLDOWN;
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
                Health.SetCurAndMaxHealth((int)(Health.GetMaxHealth() * Setting.MINE_UPGRADE_HEALTH_INCREASE));
                break;

            case EUpgrade.ProductionSpeed:

                float reduction = Setting.MINE_UPGRADE_HEALTH_INCREASE - 1.0f;
                Producer.TimeToResGainCooldown = Producer.TimeToResGainCooldown - (Producer.TimeToResGainCooldown * reduction);
                break;

            default:
                break;
        }
    }


}
