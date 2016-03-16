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

    private int health = -1;

    void Start()
    {
        if (RootObject == null)
            RootObject = gameObject;

        health = EditorHealth;
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
        }
    }

    public void Kill(HealthManager manager)
    {
        Destroy(manager.RootObject);
    }

}
