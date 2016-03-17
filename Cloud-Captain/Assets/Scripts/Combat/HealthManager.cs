using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class HealthManager : MonoBehaviour 
{
    public HealthChangedEvent OnHealthChanged;
    public HealthEvent OnZeroHealth;

    public GameObject RootObject;
    public int EditorHealth;

    private bool died = false;

    public int health = -1;

    public int maxHealth;
   

    void Start()
    {
        if (RootObject == null)
            RootObject = gameObject;

        health = EditorHealth;
        maxHealth = EditorHealth;
    }

    public int GetCurHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
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

            EditorHealth = health;

            if (health <= 0 && !died)
            {
                died = true;
                health = 0;
                OnZeroHealth.Invoke(this);
            }

            else if (health > maxHealth)
                health = maxHealth;
        }
    }

    public void Kill(HealthManager manager)
    {
        Destroy(manager.RootObject);
    }

}
