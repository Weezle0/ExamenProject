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
    [SerializeField] private int outputAmount;

    
    public void TryCraft()
    {
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
}
