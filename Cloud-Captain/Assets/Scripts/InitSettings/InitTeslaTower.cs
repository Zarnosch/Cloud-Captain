using UnityEngine;
using System.Collections;

public class InitTeslaTower : MonoBehaviour
{
    public HealthManager Health;
    public SphereCollider AttackRangeSphere;
    public BulletSpawner Spawner;

     
	
    void Awake()
    {
        Health.EditorHealth = Setting.TESLA_TOWER_DEFAULT_HEALTH;
        AttackRangeSphere.radius = Setting.TESLA_TOWER_DEFAULT_ATTACK_RADIUS;
        Spawner.attackCooldown = Setting.TESLA_TOWER_DEFAULT_ATTACK_COOLDOWN;
    }
}
