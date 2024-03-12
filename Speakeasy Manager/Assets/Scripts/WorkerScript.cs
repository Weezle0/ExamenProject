using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    public Inventory workerInventory = new();    
    public bool isIdle = true;
    public Inventory barInventory;
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

    private void Update()
    {
        if(currentStation.GetComponent<MachineClass>() != null)
        {

        }
    }

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
        ChangeDestination(FindObjectOfType<BarHandler>().transform);
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

        // if worker has no current station and collides with the bar make it the current station
        if (other.transform.GetComponent<BarHandler>() && currentStation == null)
        {
            other.transform.GetComponent<BarHandler>().hasWorker = true;
            currentStation = other.gameObject;
            other.GetComponent<BarHandler>().currentWorker = this;
        }
        // if the worker already has a station assume it want to collect resources
        else if(other.transform.GetComponent<BarHandler>() && currentStation.GetComponent<BarHandler>() != null)
        {
            foreach(var item in barInventory.items)
            {
                if(item.itemID == 0)
                {
                    Debug.Log("Got Resources");
                }
            }
        }
    }
}
