using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradHandler : MonoBehaviour
{

    List<GameObject> upgradeStages = new();
    private int currentStage;


    // when upgrading make sure the new machine has the same inventory as previous machine
    public void Upgrade()
    {

        MachineClass oldMachine = gameObject.GetComponent<MachineClass>();
        if(currentStage == upgradeStages.Count - 1)
        {
            return;
        }
        GameObject newMachine = Instantiate(upgradeStages[currentStage + 1]);
        newMachine.GetComponent<MachineClass>().machineInventory = oldMachine.machineInventory;

        if(oldMachine.isCrafting)
        {
            newMachine.GetComponent<MachineClass>().machineInventory.AddItem(0, oldMachine.SuppliesNeeded);
        }

        Destroy(gameObject);
    }

    // if police resets machine spawn the first stage back then destroy current machine
    public void PoliceReset()
    {
        GameObject newObject = Instantiate(upgradeStages[0]);
        Destroy(gameObject);
    }
}
