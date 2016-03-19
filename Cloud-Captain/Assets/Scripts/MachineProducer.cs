using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MachineProducer : MonoBehaviour 
{
    public int NumMachinesInQueue { get; private set; }

    private float curTime = Setting.MACHINE_PRODUCE_TIME;

    public void ProduceMachine()
    {
        NumMachinesInQueue++;
    }

    public void InstantProduceMachine()
    {
        PlayerManager.Instance.ChangeResource(0, 0, 1);
    }

    void Update()
    {

        if (NumMachinesInQueue > 0)
        {
            curTime -= Time.deltaTime;

            if (curTime <= 0.0f)
            {
                curTime = Setting.MACHINE_PRODUCE_TIME;
                NumMachinesInQueue--;
                InstantProduceMachine();
            }
        }
    }

}
