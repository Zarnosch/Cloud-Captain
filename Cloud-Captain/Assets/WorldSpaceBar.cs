using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class WorldSpaceBar : MonoBehaviour 
{
    public Slider Slider;


	// Update is called once per frame
	void Update () 
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                  Camera.main.transform.rotation * Vector3.up);
    }

    public void SetPercent(float p)
    {
        Slider.value = p;
    }
}
