using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public void TransferMoonShine()
    {
        if(idleWorkers.Count <= 0)
        {
            return;
        }
        foreach (MachineClass machine in machines)
        {
            foreach (var item in machine.machineInventory.items)
            {
                if(item.itemID == 1)
                {
                    WorkerScript logisticWorker = idleWorkers[0].GetComponent<WorkerScript>();
                    logisticWorker.SetStation(gameObject);
                    logisticWorker.ChangeDestination(machine.transform);
                }
            }
        }
    }
}
