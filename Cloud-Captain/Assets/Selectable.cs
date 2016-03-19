using UnityEngine;
using System.Collections;

public class Selectable : MonoBehaviour {

    void Update()
    {
        ViewportSelection.Instance.Notify(this.gameObject);
    }
}
