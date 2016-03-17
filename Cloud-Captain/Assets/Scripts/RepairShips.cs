using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepairShips : MonoBehaviour {

    private List<HealthManager> shipsHealthInRange = new List<HealthManager>();
    private List<HealthManager> damagedShips = new List<HealthManager>();

    private HealthManager currentFocus;

    private float maxHealCooldown = 1.0f;
    private float curHealCooldown = 1.0f;
    private int healAmount = 1;

    public void Update()
    {

        if(curHealCooldown > 0.0f)
        {
            curHealCooldown -= Time.deltaTime;
        }

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
        if (other.tag == gameObject.tag)
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
        if (other.tag == gameObject.tag)
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
