using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class RemoveParticleSystem : MonoBehaviour {

    ParticleSystem mySystem;

	// Use this for initialization
	void Start () {
        mySystem = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (mySystem)
        {
            if (!mySystem.isPlaying)
                Destroy(gameObject);
        }
	}
}
