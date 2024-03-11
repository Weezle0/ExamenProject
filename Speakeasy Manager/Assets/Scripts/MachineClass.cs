using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MachineClass : MonoBehaviour
{
    public bool test;

    public bool hasWorker;
    public bool isCrafting;
    public Inventory machineInventory;
    [SerializeField] private ResourceManager resourceManager;
    public int SuppliesNeeded { get; private set; }
    [SerializeField] private ResourceData[] resourceNeeded;
    [SerializeField] private int outputAmount;
    [SerializeField] private ResourceData outputResource;
    public void Start()
    {
        resourceManager = ResourceManager.instance;
    }
    public bool TryCraft()
    {
        // check if the items in the inventory are of the supply type
        List<ResourceData> requiredResources = resourceNeeded.ToList();

        // check each item in the inventory to see if it contains enough supllies
        foreach (var item in machineInventory.items)
        {

            if (item.itemID == 0)
            {
                if (item.amount >= SuppliesNeeded)
                {
                    requiredResources.Remove(resourceManager.resources[item.itemID]);
                    machineInventory.RemoveItem(item.itemID, SuppliesNeeded);
                    break;
                }

            }

        }
        if (requiredResources.Count == 0 && !isCrafting)
        {
            isCrafting = true;
            StartCoroutine(CreateProduct());
            return true;
        }
        return false;
    }
    public void UpgradeMachine()
    {

    }
    private IEnumerator CreateProduct()
    {
        yield return new WaitForSeconds(5);
        machineInventory.AddItem(1, outputAmount);
        yield return new WaitForSeconds(1);
        isCrafting = false;
        StopCoroutine(CreateProduct());

    }
}
