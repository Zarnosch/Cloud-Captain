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

    public float GetEnergyPerSecond()
    {
        return (float)(Res.Energy * ResGain) / TimeToResGainCooldown;
    }

    public float GetMatterPerSecond()
    {
        return (float)(Res.Matter * ResGain) / TimeToResGainCooldown;
    }

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
