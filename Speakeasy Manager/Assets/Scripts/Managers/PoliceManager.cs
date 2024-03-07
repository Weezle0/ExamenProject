using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoliceManager : MonoBehaviour
{
    public GameObject policeAgentPrefab;
    public static PoliceManager instance;

    [SerializeField] private EconomyManager economyManager;
    [SerializeField] private float heat = 0f;
    [SerializeField] private float heatIncreaseRate = 0.1f;
    [SerializeField] private float maxHeat = 100f;
    [SerializeField] private float raidProbabilityScale = 0.02f;
    [SerializeField] private Animator heatDisplayAnimator;
    [SerializeField] private float bribeAmount;
    [SerializeField] private float bribeMultiplier;
    [SerializeField] private float bribeCooldown = 10f;
    private bool isBribeOnCooldown;
    private float raidChance;


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

    private void Start()
    {
        economyManager = EconomyManager.instance;
    }
    
    void Update()
    {
        if (!isBribeOnCooldown)
        {
            // increase heat gradually over time
            IncreaseHeatOverTime();
        }

        // determine raid chance based on the heat level
        CheckRaidChance();

        // try to spawn a police raid
        TrySpawnPoliceRaid();
    }

    void IncreaseHeatOverTime()
    {
        // making sure heat doesn't exceed the maximum value
        heat = Mathf.Min(heat + heatIncreaseRate/1000 * Time.deltaTime, maxHeat/100);
        heatDisplayAnimator.SetFloat("Heat", heat * 1000);

        // when the heat is below 0, apply cooldown
        if (heat < 0)
        {
            heat = 0;
            StartCoroutine(BribeCooldown());
        }
    }

    void CheckRaidChance()
    {
        // check raid chance based on the heat level
        raidChance = heat * raidProbabilityScale;
    }

    void TrySpawnPoliceRaid()
    {
        // Generate a random value between 0 and 1
        float randomValue = Random.value;

        if (randomValue < raidChance)
        {
            SpawnPoliceRaid();
            // after police raid has started reset heat
            heat = 0f;
        }
    }

    void SpawnPoliceRaid()
    {
        // spawn a police raid
        SpawnPolice();
        Debug.Log("Police Raid!");
    }
    public void BribePolice()
    {

        // decrease heat after bribing police then make sure heat does not increase after bribing for X amount of time
        heat = Mathf.Max(heat - bribeAmount, 0);
        heatDisplayAnimator.SetFloat("Heat", heat * 1000);
        economyManager.DecreaseMoney(bribeAmount);
        bribeAmount += bribeAmount * bribeMultiplier;

        StartCoroutine(BribeCooldown());
    }

    System.Collections.IEnumerator BribeCooldown()
    {
        // toggle cooldown to on wait X seconds then toggle off
        isBribeOnCooldown = true;
        yield return new WaitForSeconds(bribeCooldown);
        isBribeOnCooldown = false;
    }

    private void SpawnPolice()
    {
        Instantiate(policeAgentPrefab, PoliceManager.instance.transform.position, Quaternion.identity);
    }

    private void RemovePolice()
    {

    }
}
