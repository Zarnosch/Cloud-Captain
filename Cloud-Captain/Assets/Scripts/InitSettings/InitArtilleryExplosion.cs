using UnityEngine;
using System.Collections;

public class InitArtilleryExplosion : MonoBehaviour {

    public Damager Damager;
    public SphereCollider ExplosionRadius;

	void Awake () {
        Damager.Damage = Setting.ARTILLERY_TOWER_DEFAULT_AOE_DAMAGE;
        ExplosionRadius.radius = Setting.ARTILLERY_TOWER_DEFAULT_AOE_RADIUS;
	}
	
}
