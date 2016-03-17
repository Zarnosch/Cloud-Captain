using UnityEngine;
using System.Collections;
using System;

public class WorkshopUpgrade : Upgrade {

    public HealthManager Health;
    public Repairer Repairer;

    protected override EUpgrade[] GetAvaibleUpgrades()
    {
        return new EUpgrade[] { EUpgrade.Life, EUpgrade.ShipRepair };
    }

    protected override void OnAwake()
    {
        Health.SetMaxHealth(Setting.WORKSHOP_DEFAULT_HEALTH);

        Repairer.healAmount = Setting.WORKSHOP_DEFAULT_REPAIR_AMOUNT;

        Repairer.maxHealCooldown = Setting.WORKSHOP_DEFAULT_REPAIR_COOLDOWN;
    }

    public override int GetNumMaxUpgrades()
    {
        return Setting.MAX_SLOTS_WORKSHOP;
    }

    protected override void OnUpgrade(EUpgrade upgrade)
    {
        switch (upgrade)
        {
            case EUpgrade.Life:
                IncreaseHealth(Health, Setting.WORKSHOP_UPGRADE_HEALTH_INCREASE);
                break;

            case EUpgrade.ShipRepair:

                Repairer.healAmount = (int)(Repairer.healAmount * Setting.WORKSHOP_UPGRADE_REPAIR_AMOUNT_INCREASE);
                ReduceFloat(ref Repairer.maxHealCooldown, Setting.WORKSHOP_UPGRADE_REPAIR_COOLDOWN_INCREASE);
                break;

            default:
                break;
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LevelUpIgnoreCost(EUpgrade.Life);
        }

        else if (Input.GetKeyDown(KeyCode.O))
        {
            LevelUpIgnoreCost(EUpgrade.ShipRepair);
        }
    }

}
