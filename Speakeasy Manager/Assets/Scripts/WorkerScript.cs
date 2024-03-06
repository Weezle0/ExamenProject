using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    public Inventory workerInventory = new();
    [SerializeField]
    public bool IsWorking
    {
        get
        {
            return isWorking;
        }
        set
        {
            isWorking = value;
        }
    }
    [SerializeField] private MachineClass currentMachine = null;
    [SerializeField] private bool isWorking;


    // Update is called once per frame
    void Update()
    {
        // if worker is at a machine make the worker work
        if (currentMachine != null && !currentMachine.isCrafting)
        {
            isWorking = true;
            bool canCraft = currentMachine.TryCraft();
            if (!canCraft)
            {
                IsWorking = false;
                StoreMoonShine();
            }
        }
        // else stop working
        if (currentMachine == null && isWorking)
        {
            isWorking = false;
        }
    }

    public void ChangeDestination(Transform targetTransform)
    {
        //set the new destination of the worker
        GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
    }

    public MachineClass GetCurrentMachine()
    {
        if (currentMachine != null)
        {
            return currentMachine;
        }
        return null;
    }

    public void StoreMoonShine()
    {
        foreach (var item in workerInventory.items)
        {
            if (item.itemID == 1)
            {
                currentMachine.machineInventory.TransferItems(workerInventory, currentMachine.machineInventory, item.itemID, 5);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.name + " found a machine");
        if (other.transform.GetComponent<MachineClass>() && currentMachine == null)
        {
            currentMachine = other.transform.GetComponent<MachineClass>();
            currentMachine.hasWorker = true;
        }
    }
}
