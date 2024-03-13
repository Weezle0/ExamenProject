using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    public Inventory workerInventory = new();
    public bool isIdle = true;
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
    [SerializeField] private bool isWorking;
    [SerializeField] private GameObject currentStation = null;

    public void ChangeDestination(Transform targetTransform)
    {
        //set the new destination of the worker
        GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
    }

    public GameObject GetStation()
    {
        return currentStation;
    }
    public void SetStation(GameObject newStation)
    {
        currentStation = newStation;
    }


    public void GatherResources()
    {
        Debug.Log("Gather Resources");
        ChangeDestination(WorkerManager.instance.bar.transform);
    }

    public void StoreMoonshine()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        // if worker has no current station and collides with a machines class make it the current station
        if (other.transform.GetComponent<MachineClass>() && currentStation == null)
        {
            currentStation = other.gameObject;
            currentStation.GetComponent<MachineClass>().hasWorker = true;
            currentStation.GetComponent<MachineClass>().currentWorker = this;
        }
        else if (other.transform.GetComponent<MachineClass>() == currentStation)
        {
            Debug.Log("Own Machine");
            if (workerInventory.items.Count > 0)
            {
                foreach (var item in workerInventory.items)
                {
                    if (item.itemID == 0)
                    {
                        workerInventory.TransferItems(currentStation.GetComponent<MachineClass>().machineInventory, workerInventory, 0, 200);
                        currentStation.GetComponent<MachineClass>().isCrafting = false;
                        GetComponent<MachineClass>().TryCraft();
                    }
                }
            }
        }
        // if worker has no current station and collides with the bar make it the current station
        if (other.transform.GetComponent<BarHandler>() && currentStation == null)
        {
            other.transform.GetComponent<BarHandler>().hasWorker = true;
            currentStation = other.gameObject;
            other.GetComponent<BarHandler>().currentWorker = this;
        }
        // if the worker already has a station assume it want to collect resources
        else if (other.transform.GetComponent<BarHandler>() && currentStation.GetComponent<MachineClass>())
        {
            foreach (var item in other.GetComponent<BarHandler>().barInventory.items)
            {
                if (item.itemID == 0)
                {
                    workerInventory.TransferItems(workerInventory, other.GetComponent<BarHandler>().barInventory, 0, 200);
                    ChangeDestination(currentStation.transform);
                    return;
                }
            }

        }
    }
}

