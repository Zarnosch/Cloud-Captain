using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Repairer : MonoBehaviour
{

    public enum TargetRepairLayer { Ships, Buildings }
    public enum RepairMode { SingleTarget, InRadius }


    private int targetLayerId;
    public RepairMode Mode = RepairMode.InRadius;
    public TargetRepairLayer TargetLayer = TargetRepairLayer.Ships;

    private List<HealthManager> shipsHealthInRange = new List<HealthManager>();
    private List<HealthManager> damagedShips = new List<HealthManager>();

    private HealthManager currentFocus;

    [ReadOnly]
    public float maxHealCooldown = 1.0f;
    [ReadOnly]
    public float curHealCooldown = 1.0f;
    [ReadOnly]
    public int healAmount = 1;


    void Start()
    {
        curHealCooldown = maxHealCooldown;

        targetLayerId = LayerMask.NameToLayer(TargetLayer.ToString());
    }

    public void Update()
    {
        if (curHealCooldown > 0.0f)
        {
            curHealCooldown -= Time.deltaTime;
        }
     
   
        else
        {
            for (int i = 0; i < damagedShips.Count; i++)
            {
                damagedShips[i].ChangeHealth(healAmount);
                curHealCooldown = maxHealCooldown;
            }
        }
    }


    void HealSingle()
    {
        if (currentFocus && curHealCooldown <= 0.0f)
        {

            if (currentFocus.IsDamaged())
            {
                currentFocus.ChangeHealth(healAmount);
                curHealCooldown = maxHealCooldown;
            }

            else
            {
                damagedShips.Remove(currentFocus);
                currentFocus = null;
            }


        }

        else
        {
            if (damagedShips.Count > 0 && damagedShips[0].IsDamaged())
            {
                currentFocus = damagedShips[0];
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag && other.gameObject.layer == targetLayerId)
        {
            HealthManager health = other.gameObject.GetComponent<HealthManager>();

            if (health)
            {
                shipsHealthInRange.Add(health);
                health.OnHealthChanged.AddListener(OnHealthChanged);

                if (health.IsDamaged())
                    damagedShips.Add(health);
            }

        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == gameObject.tag && other.gameObject.layer == targetLayerId)
        {
            HealthManager health = other.gameObject.GetComponent<HealthManager>();

            if (health)
            {
                if (health.IsDamaged())
                    damagedShips.Remove(health);

                shipsHealthInRange.Remove(health);
                health.OnHealthChanged.RemoveListener(OnHealthChanged);
                
            }

        }
    }

    void OnHealthChanged(HealthManager health, int delta)
    {
        if(delta < 0)
        {
            if(!damagedShips.Contains(health))
                damagedShips.Add(health);
        }

        else if(delta > 0)
        {
            if (health.GetCurHealth() + delta >= health.GetMaxHealth())
            {
                damagedShips.Remove(health);
            }
        }
    }
}
