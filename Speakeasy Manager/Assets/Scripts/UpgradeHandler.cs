using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{

    public List<GameObject> upgradeStages = new();
    public Button upgradeButton;
    [SerializeField] private  int currentStage;
    [SerializeField] private int upgradeCost;
    private EconomyManager economyManager;

    private void Start()
    {
        economyManager = EconomyManager.instance;
    }


    public void UpgradeConfirm()
    {
        if(currentStage == upgradeStages.Count-1)
        {
            return;
        }
        upgradeButton.onClick.AddListener(Upgrade);
        upgradeButton.gameObject.SetActive(true);
    }
    // when upgrading make sure the new machine has the same inventory as previous machine
    public void Upgrade()
    {
        if(economyManager.GetMoney() <= upgradeCost)
        {
            upgradeButton.gameObject.SetActive(false);
            return;
        }
        economyManager.DecreaseMoney(upgradeCost);
        MachineClass oldMachine = gameObject.GetComponent<MachineClass>();
        if(currentStage == upgradeStages.Count - 1)
        {
            upgradeButton.gameObject.SetActive(false);
            return;
        }
        GameObject newMachine = Instantiate(upgradeStages[currentStage + 1]);
        if(oldMachine.machineInventory.items.Count > 0)
        {
           newMachine.GetComponent<MachineClass>().machineInventory.items = oldMachine.machineInventory.items;
        }
       
        if(oldMachine.isCrafting)
        {
            newMachine.GetComponent<MachineClass>().machineInventory.AddItem(0, oldMachine.SuppliesNeeded);
        }
        newMachine.GetComponent<UpgradeHandler>().upgradeButton = upgradeButton;
        newMachine.GetComponent<UpgradeHandler>().currentStage = currentStage+1;
        newMachine.transform.position = oldMachine.transform.position;
        
        newMachine.GetComponent<MachineClass>().hasWorker = true;
        oldMachine.currentWorker.SetStation(newMachine);
        newMachine.GetComponent<MachineClass>().currentWorker = oldMachine.currentWorker;
        
        Destroy(oldMachine.gameObject);
        upgradeButton.gameObject.SetActive(false);
    }

    // if police resets machine spawn the first stage back then destroy current machine
    public void PoliceReset()
    {
        MachineClass oldMachine = gameObject.GetComponent<MachineClass>();

        GameObject newMachine = Instantiate(upgradeStages[0]);
        if (oldMachine.machineInventory.items.Count > 0)
        {
            newMachine.GetComponent<MachineClass>().machineInventory.items = oldMachine.machineInventory.items;
        }

        if (oldMachine.isCrafting)
        {
            newMachine.GetComponent<MachineClass>().machineInventory.AddItem(0, oldMachine.SuppliesNeeded);
        }
        newMachine.GetComponent<UpgradeHandler>().upgradeButton = upgradeButton;
        newMachine.transform.position = oldMachine.transform.position;

        newMachine.GetComponent<MachineClass>().hasWorker = true;
        oldMachine.currentWorker.SetStation(newMachine);
        newMachine.GetComponent<MachineClass>().currentWorker = oldMachine.currentWorker;

        Destroy(oldMachine.gameObject);
    }
}
