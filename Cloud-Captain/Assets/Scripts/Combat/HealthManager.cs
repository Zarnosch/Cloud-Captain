using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class HealthManager : MonoBehaviour 
{
    public GameObject RootObject;
 

    public HealthChangedEvent OnHealthChanged;
    public HealthEvent OnZeroHealth;

    [ReadOnly]
    public int health = -1;
    [ReadOnly]
    public int maxHealth;

    private bool died = false;
    private GameObject healthBarGameObject;
    private WorldSpaceBar healthBar;

    private GameObject healthParticleSystem;

    void Start()
    {
        if (RootObject == null)
            RootObject = gameObject;

        health = maxHealth;

        
        if (!healthBarGameObject)
        {
            healthBarGameObject = (GameObject)Instantiate(BuildManager.Instance.HealthbarPrefab);

            healthBarGameObject.transform.position = gameObject.transform.position + new Vector3(Setting.HEALTH_BAR_OFFSET_X, Setting.HEALTH_BAR_OFFSET_Y, Setting.HEALTH_BAR_OFFSET_Z);

            healthBarGameObject.transform.SetParent(this.gameObject.transform);

            healthBar = healthBarGameObject.GetComponent<WorldSpaceBar>();
			healthBar.SetPercent (GetHealthPercent());

            TryActivateHealthBar();
        }

    }

    public int GetCurHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthPercent()
    {
        return (float)health / (float)maxHealth;
    }

    public bool IsDamaged()
    {
        return health < maxHealth;
    }

    public void ChangeHealth(int delta)
    {
        if (delta != 0)
        {
            OnHealthChanged.Invoke(this, delta);
            health += delta;

            if (health <= 0 && !died)
            {
                died = true;
                health = 0;
                MyOnZeroHealth();
            }

            else if (health > maxHealth)
                health = maxHealth;

            TryActivateHealthBar();
            healthBar.SetPercent(GetHealthPercent());
        }
    }

    private void MyOnZeroHealth()
    {
        OnZeroHealth.Invoke(this);


        if (this.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            IslandReference reference = RootObject.GetComponent<IslandReference>();

            if (reference && reference.island)
                reference.island.RemoveBuilding(RootObject);
        }
    }

    public void Kill(HealthManager manager)
    {
        Destroy(manager.RootObject);
    }


    public void SetMaxHealth(int health)
    {
        this.maxHealth = health;
    }

    public void SetCurAndMaxHealth(int health)
    {
        this.maxHealth = health;
        this.health = health;
    }

    private void TryActivateHealthBar()
    {
        if (!healthBarGameObject)
            return;

        else
        {
            if (!IsDamaged())
            {
                if (healthBarGameObject.activeSelf)
                    healthBarGameObject.SetActive(false);
            }

            else if (!healthBarGameObject.activeSelf)
            {
                healthBarGameObject.SetActive(true);
            }
        }
    }


    public void StartHealParticles()
    {
        if (!healthParticleSystem)
        {

            healthParticleSystem = (GameObject)Instantiate(BuildManager.Instance.HealthFeedbackParticlePrefab);
            healthParticleSystem.transform.SetParent(RootObject.transform);
            healthParticleSystem.transform.position = gameObject.transform.position;
        }


        else if (!healthParticleSystem.activeSelf)
            healthParticleSystem.SetActive(true);
    }


    public void EndHealParticles()
    {
        if (healthParticleSystem)
            healthParticleSystem.SetActive(false);
    }


}
