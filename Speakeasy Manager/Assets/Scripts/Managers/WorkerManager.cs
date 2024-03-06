using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<WorkerScript> workers = new();
    public List<MachineClass> machines = new();
    public static WorkerManager instance;
    private float checkCooldown;

    private void Awake()
    {
        // if there is no instance of this object set it to this object
        // else destroy this object
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }


    }
    private void Update()
    {
        // every time the timer has passed 5 check if there is an idle machine
        checkCooldown += Time.deltaTime;
        if (checkCooldown <= 5)
        {
            return;
        }
        checkCooldown = 0;
        var idleMachine = FindIdleMachine();
        if(idleMachine != null )
        {
            // if there is an idle machine find an idle worker to work it
            FindIdleWorker(idleMachine.transform);
        }
        
    }
    public MachineClass FindIdleMachine()
    {
        // check each machine to find if they are idle
        foreach (MachineClass machine in machines)
        {
            if (!machine.hasWorker)
            {
                return machine.GetComponent<MachineClass>();
            }
        }
        return null;
    }
    public void FindIdleWorker(Transform newTarget)
    {
        // check each worker to find if they are idle
        foreach (WorkerScript worker in workers)
        {
            if(!worker.IsWorking)
            {
                worker.ChangeDestination(newTarget);
                return;
            }
        }
    }
}
