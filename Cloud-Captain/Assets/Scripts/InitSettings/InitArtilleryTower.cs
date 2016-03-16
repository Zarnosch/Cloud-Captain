using UnityEngine;
using System.Collections;

public class InitArtilleryTower : MonoBehaviour {

    public HealthManager Health;
    public BulletSpawner Spawner;
    public SphereCollider AttackRadiusSphere;

    void Awake()
    {
        Health.EditorHealth = Setting.ARTILLERY_TOWER_DEFAULT_HEALTH;
        Spawner.MinDistance = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MIN;
        AttackRadiusSphere.radius = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_RADIUS_MAX;
        Spawner.attackCooldown = Setting.ARTILLERY_TOWER_DEFAULT_ATTACK_COOLDOWN;

    }
}
