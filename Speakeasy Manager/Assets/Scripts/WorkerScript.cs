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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MachineClass>() && currentStation == null)
        {
            currentStation = other.gameObject;
            currentStation.GetComponent<MachineClass>().hasWorker = true;
        }
        if(other.transform.GetComponent<BarHandler>())
        {
            other.transform.GetComponent<BarHandler>().hasWorker = true;
        }
    }
}
