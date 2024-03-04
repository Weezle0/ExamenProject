using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    [SerializeField] private int heat;
    [SerializeField] private int bribeAmount;
    [SerializeField] private float bribeMultiplier;
    [SerializeField] private int heatIncreaseChance;
    [SerializeField] private int heatIncreaseAmount;
    [SerializeField] private int heatIncreaseCooldown;
    private float heatIncreaseTimer;

    private void Update()
    {
        heatIncreaseTimer += Time.deltaTime;
        if(heatIncreaseTimer % heatIncreaseCooldown == 0 )
        {
            int randomchance = Random.RandomRange(0, 1000);
            if(randomchance <= heatIncreaseChance )
            {
                heat += heatIncreaseAmount;
            }
        }
        
    }
    public void BribePolice(int amount)
    {
        bribeAmount += amount/2;
    }

    private void SpawnPolice()
    {

    }

    private void RemovePolice()
    {

    }
}
