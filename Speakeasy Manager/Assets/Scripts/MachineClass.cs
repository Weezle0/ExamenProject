using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MachineClass : MonoBehaviour
{
    public bool hasWorker;
    public bool isCrafting;
    public Inventory machineInventory;
    public int machineLevel;
    public int SuppliesNeeded
    {
        get
        {
            return suppliesNeeded;
        }
        private set
        {
            suppliesNeeded = value;
        }
    }
    public WorkerScript currentWorker;

    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private ResourceData[] resourceNeeded;
    [SerializeField] private int suppliesNeeded;
    [SerializeField] private int outputAmount;
    [SerializeField] private ResourceData outputResource;
    public void Start()
    {
        resourceManager = ResourceManager.instance;
    }

    private void Update()
    {
        if (hasWorker && !isCrafting)
        {
            isCrafting = true;
            if (TryCraft() == false)
            {
                currentWorker.GatherResources();
            }
        }
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
                }
            }
        }
        if (requiredResources.Count == 0 && !isCrafting)
        {
            isCrafting = true;
            StartCoroutine(CreateProduct());
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpgradeMachine()
    {
        gameObject.GetComponent<UpgradeHandler>().UpgradeConfirm();
    }
    private IEnumerator CreateProduct()
    {
        machineInventory.RemoveItem(0, SuppliesNeeded);
        yield return new WaitForSeconds(5);
        machineInventory.AddItem(1, outputAmount);
        yield return new WaitForSeconds(1);
        isCrafting = false;
        StopCoroutine(CreateProduct());

    }
}
