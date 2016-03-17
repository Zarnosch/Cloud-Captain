using UnityEngine;
using System.Collections;

public class RessourceProducer : MonoBehaviour
{

    [ReadOnly]
    public int ResGain;
    [ReadOnly]
    public float TimeToResGainCooldown;
    [ReadOnly]
    public float TimeToResGain;
    [ReadOnly]
    public Res Res;



    void Start()
    {
        TimeToResGain = TimeToResGainCooldown;
    }

    void Update()
    {
        if (TimeToResGain <= 0.0f)
        {
            TimeToResGain = TimeToResGainCooldown;
            PlayerManager.Instance.ChangeResource(Res * ResGain);
        }

        else
        {
            TimeToResGain -= Time.deltaTime;
        }
    }
}
