using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoney : MonoBehaviour
{
    private Animator moneyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoneyLogic());
    }

    private IEnumerator MoneyLogic()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
