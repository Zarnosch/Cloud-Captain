using UnityEngine;
using System.Collections;

public abstract class Upgrade : MonoBehaviour
{
    public enum EUpgrade
    {
        /// <summary>Dont uses this as a upgrade.</summary>
        None,

        Shield, Range, Life, ShipRepair, BuildingRepair, MovementSpeed, ProductionSpeed, Damage
    }

    /// <summary>Which upgrades can be used on a given object?</summary>
    [ReadOnly]
    public EUpgrade[] AvaibleUpgrades;

    /// <summary>The upgrades that have been build:</summary>
    [ReadOnly]
    public EUpgrade[] UsedUpgrades;

    /// <summary>How many upgrades have been build in? </summary>
    private int upgradeCount = 0;
    private int engineCost = 1;

    void Awake()
    {
        OnAwake();

        this.UsedUpgrades = new EUpgrade[GetNumMaxUpgrades()];
        this.AvaibleUpgrades = GetAvaibleUpgrades();
    }


    public void LevelUp(EUpgrade upgrade)
    {
        InternalLevelUp(upgrade, engineCost);
    }

    public void LevelUpIgnoreCost(EUpgrade upgrade)
    {
        InternalLevelUp(upgrade, 0);
    }

    private void InternalLevelUp(EUpgrade upgrade, int cost)
    {
        //if this object can be upgrades by the given Upgrade, and it still has slots to upgrade
        if (IsUpgradeAvaible(upgrade) && GetNumUsedUpgrades() < GetNumMaxUpgrades())
        {
            //not enough engines:
            if (PlayerManager.Instance.EnoughResource(0, 0, cost))
            {
                PlayerManager.Instance.ChangeResource(0, 0, -cost);
                OnUpgrade(upgrade);

                UsedUpgrades[upgradeCount] = upgrade;

                engineCost++;
                upgradeCount++;
            }
        }
    }

    public int GetNumUsedUpgrades()
    {
        return upgradeCount;
    }

    public bool IsUpgradeAvaible(EUpgrade upgrade)
    {
        for (int i = 0; i < AvaibleUpgrades.Length; i++)
        {
            if (AvaibleUpgrades[i] == upgrade)
                return true;
        }
        return false;
    }

    public bool HasEmptySlots()
    {
        return GetNumUsedUpgrades() < GetNumMaxUpgrades();
    }


    protected abstract EUpgrade[] GetAvaibleUpgrades();

    protected abstract void OnAwake();

    public abstract int GetNumMaxUpgrades();

    protected abstract void OnUpgrade(EUpgrade upgrade);



    protected void IncreaseHealth(HealthManager health, float increase)
    {
        int newMaxHealth = (int)(health.maxHealth * increase);

        if (health.IsDamaged())
        {
            health.SetMaxHealth(newMaxHealth);
        }

        else
        {
            health.SetCurAndMaxHealth(newMaxHealth);
        }
    }

    protected void IncreaseRange(SphereCollider rangeSphere, float increase)
    {
        rangeSphere.radius *= increase;
    }

    protected void IncreaseBulletDamage(BulletSpawner spawner, float increase)
    {
        spawner.BulletDamage = (int)(spawner.BulletDamage * increase);
    }

    protected void ReduceFloat(ref float cooldown, float increase)
    {
        float reduction = increase - 1.0f;

        cooldown = cooldown - (reduction * cooldown);
    }

    protected int GetNumUpgradesOfType(EUpgrade upgrade)
    {
        int num = 0;

        for (int i = 0; i < UsedUpgrades.Length; i++)
        {
            if (UsedUpgrades[i] == upgrade)
                num++;
        }

        return num;
    }

}
