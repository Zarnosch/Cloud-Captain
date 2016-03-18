using UnityEngine;
using System.Collections;

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

    void Start()
    {
        if (RootObject == null)
            RootObject = gameObject;

        health = maxHealth;

        
        if (!healthBarGameObject)
        {
            healthBarGameObject = (GameObject)Instantiate(BuildManager.Instance.HealthbarPrefab);

            healthBarGameObject.transform.position = gameObject.transform.position + new Vector3(0.0f, -5.0f, 0.0f);

            healthBarGameObject.transform.SetParent(this.gameObject.transform);

            healthBar = healthBarGameObject.GetComponent<WorldSpaceBar>();
			healthBar.SetPercent (GetHealthPercent());
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
                OnZeroHealth.Invoke(this);
            }

            else if (health > maxHealth)
                health = maxHealth;

            healthBar.SetPercent(GetHealthPercent());
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
}
