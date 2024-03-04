using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerScript : MonoBehaviour
{
    public Inventory workerInventory = new();
    [SerializeField] public bool IsWorking {  get; set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDestination(Transform targetTransform)
    {
        GetComponent<NavMeshAgent>().SetDestination(targetTransform.position);
    }
}
