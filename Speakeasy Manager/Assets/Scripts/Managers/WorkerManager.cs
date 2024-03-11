using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<WorkerScript> workers = new();
    public List<WorkerScript> idleWorkers = new();
    public WorkerScript selectedWorker;
    public List<MachineClass> machines = new();
    public BarHandler bar;
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
    public void CheckMachineState(MachineClass machine)
    {
        // if there is an idle machine find an idle worker to work it
        if (!machine.hasWorker)
        {
            FindIdleWorkers(machine.transform);
        }

    }
    public void FindIdleWorkers(Transform newTarget)
    {
        // check each worker to find if they are idle
        foreach (WorkerScript worker in workers)
        {
            if (worker.isIdle)
            {
                idleWorkers.Add(worker);
            }
            else
            {
                if (idleWorkers.Contains(worker))
                {
                    idleWorkers.Remove(worker);
                }
            }
        }
    }
}
