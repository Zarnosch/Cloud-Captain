using UnityEngine;
using System.Collections;

public class BulletSpawnerReference : MonoBehaviour {

    public BulletSpawner Attacker;

    public BulletSpawner GetAttacker()
    {
        return Attacker;
    }
	
}
