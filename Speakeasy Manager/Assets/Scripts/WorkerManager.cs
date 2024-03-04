using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public GameObject temp;
    public List<WorkerScript> workers = new();
    public List<MachineClass> machines = new();
    public static WorkerManager instance;
    private float checkCooldown;

    private void Awake()
    {
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
        checkCooldown += Time.deltaTime;
        if (checkCooldown <= 5)
        {
            return;
        }
        var idleMachine = FindIdleMachine();
        if(idleMachine != null )
        {
            FindIdleWorker(idleMachine.transform);
        }
        
    }
    public MachineClass FindIdleMachine()
    {
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
        foreach (WorkerScript worker in workers)
        {
            if(!worker.IsWorking)
            {
                worker.ChangeDestination(newTarget);
                worker.IsWorking = true;
                return;
            }
        }
    }
}
