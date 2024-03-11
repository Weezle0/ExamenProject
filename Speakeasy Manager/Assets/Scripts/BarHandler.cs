using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHandler : MonoBehaviour
{
    
    public bool hasWorker;
    public Inventory barInventory = new Inventory();
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
    }

    private void Update()
    {
        if(CheckBarInventory() && hasWorker)
        {
            if (!isSelling)
            {
                StartCoroutine(SellMoonshine());
            }
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

    public IEnumerator SellMoonshine()
    {
        isSelling = true;
        economyManager.IncreaseMoney(moonshineValue);
        yield return new WaitForSeconds(sellCooldown);
        isSelling = false;
    }
}
