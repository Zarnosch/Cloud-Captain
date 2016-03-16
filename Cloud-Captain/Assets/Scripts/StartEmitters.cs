using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StartEmitters : MonoBehaviour {

    public bool StartAwake = true;
    public bool RemoveOnEnd = false;

    public GameObject ObjectToRemove;

    void Start()
    {
        if(StartAwake)
            Play();

        if (ObjectToRemove == null)
            ObjectToRemove = gameObject;
    }

    public void Play()
    {

        float longestTime = 0.0f;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            ParticleSystem system = gameObject.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (system)
            {
                //  particleSystems[i].gameObject.SetActive(true);
                system.Play(true);
                longestTime = Mathf.Max(longestTime, system.startLifetime);
            }

            if(RemoveOnEnd)
                Destroy(ObjectToRemove, longestTime);
        }
    }


}
