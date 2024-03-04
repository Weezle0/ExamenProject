using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineClass : MonoBehaviour
{
    public bool hasWorker;
    public bool isCrafting;
    public Inventory machineInventory;
    [SerializeField] private int suppliesNeeded;
    [SerializeField] private ResourceData resourceNeeded;
    [SerializeField] private int outputAmount;
    [SerializeField] private ResourceData outputResource;

    
    public void TryCraft()
    {
        //check if the items in the inventory are of the supply type
        foreach(var item in machineInventory.items)
        {
            if(item.itemID == 0)
            {
                machineInventory.RemoveItem(item.itemID, suppliesNeeded);
                CreateProduct();
            }
        }
    }
    public void UpgradeMachine()
    {

    }
    private void CreateProduct()
    {
        //play animation
        machineInventory.AddItem(1, outputAmount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WorkerScript>())
        {
            if(!other.GetComponent<WorkerScript>().IsWorking)
            {

            }
        }
    }
}
