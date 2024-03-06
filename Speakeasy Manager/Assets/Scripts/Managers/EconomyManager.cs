using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField] private float money;
    [SerializeField] private Text moneyText;
    [SerializeField] private Text moneyChangeText;
    [SerializeField] private Animator moneyChangeAnimator;


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
        moneyChangeAnimator.SetTrigger("MoneyAdd");
        money += amount;
        moneyText.text = money.ToString();
        moneyChangeText.text = "+" + amount.ToString("F2");
    }
    public void DecreaseMoney(float _amount)
    {
        moneyChangeAnimator.SetTrigger("MoneyRemove");
        money -= _amount;
        moneyText.text = money.ToString();
        moneyChangeText.text = "-" + _amount.ToString("F2");
    }
}
