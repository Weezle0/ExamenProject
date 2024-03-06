using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private float money;
    [SerializeField] private Text moneyText;
    
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public float GetMoney()
    {
        moneyText.text = money.ToString();
        return money;
    }

    public void IncreaseMoney(float amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
    public void DecreaseMoney(float _amount)
    {
        money -= _amount;
        moneyText.text = money.ToString();
    }
}
