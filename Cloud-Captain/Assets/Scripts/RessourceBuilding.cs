using UnityEngine;
using System.Collections;

public class RessourceBuilding : MonoBehaviour
{
	public enum ResourceType { Energy, Matter }

    public ResourceType Type = ResourceType.Energy;


    private Res res;
    int ResGain;

    float timeToResGainCooldown;
    float timeToResGain;

    void Start()
    {
        if(Type == ResourceType.Energy)
        {

            res = new Res(0, 1, 0);
            ResGain = Setting.POWER_PLANT_DEFAULT_ENERGY_GAIN;
            timeToResGainCooldown = Setting.POWER_PLANT_DEFAULT_ENERGY_GAIN_COOLDOWN;
        }


        else if(Type == ResourceType.Matter)
        {
            res = new Res(1, 0, 0);
            ResGain = Setting.MINE_DEFAULT_MATTER_GAIN;
            timeToResGainCooldown = Setting.MINE_DEFAULT_MATTER_GAIN_COOLDOWN;
        }

        timeToResGain = timeToResGainCooldown;
    }

    void Update()
    {

        if (timeToResGain <= 0.0f)
        {
            timeToResGain = timeToResGainCooldown;
            PlayerManager.Instance.ChangeResource(res * ResGain);
        }

        else
        {
            timeToResGain -= Time.deltaTime;
        }
    }
}
