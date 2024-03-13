using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationManger : MonoBehaviour
{
    public Animator charachterController;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<NavMeshAgent>().velocity == new Vector3(0,0,0))
        {
            charachterController.SetFloat("Moving", 0);
        }
        else
        {
            charachterController.SetFloat("Moving", 1);
        }
    }
}
