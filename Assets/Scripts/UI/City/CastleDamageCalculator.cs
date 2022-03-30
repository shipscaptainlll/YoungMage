using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamageCalculator : MonoBehaviour
{
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;
    int skeletonsNumber;
    float dps;

    // Start is called before the first frame update
    void Start()
    {
        skeletonsNumber = 3;
        CalculateDPS();
        StartCoroutine(DecreaseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateDPS()
    {
        dps = skeletonsNumber * 0.01f;
    }

    void CalculateDamage()
    {
        dps = 1;
    }

    IEnumerator DecreaseHealth()
    {
        while (true)
        {
            castleHealthDecreaser.DealDamage(dps);
            yield return new WaitForSeconds(0.033f);
        }
        
    }
}
