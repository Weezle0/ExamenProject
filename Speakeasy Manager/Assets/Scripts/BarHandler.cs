using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    
    public bool hasWorker;
    public Inventory barInventory = new Inventory();
    public WorkerScript currentWorker;
    public Button sellButton;
    public Button buyButton;
    public float MoonshineValue
    {
        get
        {
            return moonshineValue;
        }
    }
    [SerializeField] private float moonshineValue;
    private bool isSelling;
    [SerializeField] private float sellCooldown;
    private EconomyManager economyManager;

    private void Start()
    {
        economyManager = EconomyManager.instance;
        WorkerManager.instance.bar = this;
        sellButton.onClick.AddListener(TriggerSell);
    }

    private void Update()
    {
        if(hasWorker && CheckBarInventory())
        {
            sellButton.gameObject.SetActive(true);
        }
        else if(sellButton.gameObject.activeSelf)
        {
            sellButton.gameObject.SetActive(false);
        }
        
    }
    private bool CheckBarInventory()
    {
        foreach(var item in barInventory.items)
        {
            if(item.itemID == 1)
            {
                
                return true;
            }
        }
        return false;
    }

    public void TriggerSell()
    {
        if(CheckBarInventory())
        {
            Debug.Log("Selling");
            StartCoroutine("SellMoonshine");
        }
    }

    public void BuySupplies()
    {
        barInventory.AddItem(0, 2000);
        economyManager.DecreaseMoney(500);
    }
    public IEnumerator SellMoonshine()
    {
        isSelling = true;
        
        foreach(var item in barInventory.items)
        {
            if(item.itemID == 1)
            {
                economyManager.IncreaseMoney(moonshineValue * item.amount);
                barInventory.items.Remove(item);
                break;
            }
        }
        yield return new WaitForSeconds(sellCooldown);
        isSelling = false;
    }

    public void UpgradeBar()
    {
        gameObject.GetComponent<UpgradeHandler>().UpgradeConfirm();
    }
}
