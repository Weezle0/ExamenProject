using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resources")]
public class ResourceData : ScriptableObject
{
    public GameObject prefab;
    [SerializeField] private int id;
    [SerializeField] private int amount;
    public int Id
    {
        get { return id; }
    }
    public int Amount
    {
        get { return amount; }
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
    public void RemoveAmount(int value)
    {
        amount -= value;
    }
}
