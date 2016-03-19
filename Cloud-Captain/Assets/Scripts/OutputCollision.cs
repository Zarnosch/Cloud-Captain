using UnityEngine;
using System.Collections;

public class OutputCollision : MonoBehaviour {


    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("OnTrigger");
        Debug.Log(GetText(this.gameObject, collider.gameObject));
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollision");
        Debug.Log(GetText(this.gameObject, collision.collider.gameObject));
    }

    private string GetText(GameObject left, GameObject right)
    {
        return string.Concat(left.name, " on Layer:", LayerMask.LayerToName(left.layer), " collided with: ", right.name, " on layer: ", LayerMask.LayerToName(right.layer));
    }

}
