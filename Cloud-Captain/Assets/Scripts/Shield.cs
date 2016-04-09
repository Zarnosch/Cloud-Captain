using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider), typeof(MeshRenderer))]
public class Shield : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private MeshRenderer meshRenderer;

    private float lastHitCooldown;
    private float cooldownToShield;

    private int health;
    private int maxHealth;

    public bool IsShieldActive {  get { return sphereCollider.enabled; } }

    public void SetMaxHealth(int newHealth)
    {
        bool changeCurHealth = this.maxHealth == this.health;

        this.maxHealth = newHealth;

        if (changeCurHealth)
            this.health = this.maxHealth;
    }

    public void MultHealth(float factor)
    {
        bool changeCurHealth = this.maxHealth == this.health;

        this.maxHealth = (int)(this.maxHealth * factor);

        if (changeCurHealth)
            this.health = this.maxHealth;
    }

    public void SetRadius(float radius)
    {
        gameObject.transform.localScale = new Vector3(radius * 2.0f, radius * 2.0f, radius * 2.0f);
    }

	// Use this for initialization
	public void Init ()
    {
        sphereCollider = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();

        this.cooldownToShield = Setting.SHIP_SHIELD_COOLDOWN;
        this.lastHitCooldown = Setting.SHIP_SHIELD_COOLDOWN;

        ShieldUp();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!IsShieldActive)
        {
            lastHitCooldown += Time.deltaTime;

            if (lastHitCooldown >= cooldownToShield)
                ShieldUp();
        }
    }


    public void OnImpact(Vector3 position, int damage)
    {
        lastHitCooldown = 0.0f;

        health -= damage;

        if(health <= 0)
        {
            health = 0;
            ShieldDown();
        }
    }

    void ShieldDown()
    {
        sphereCollider.enabled = false;
        meshRenderer.enabled = false;
    }

    void ShieldUp()
    {
        sphereCollider.enabled = true;
        meshRenderer.enabled = true;
        health = maxHealth;
    }

    public float GetPercent()
    {
        return (float)health / maxHealth;
    }
   
}
