using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableParticleSystem : MonoBehaviour
{
    float timeDestruction;

    public float TimeDestruction { set { timeDestruction = value; InitiateDestruction(timeDestruction); } }

    void InitiateDestruction(float timeDestruction)
    {
        StartCoroutine(DestructionCounter());
    }

    IEnumerator DestructionCounter()
    {
        yield return new WaitForSeconds(timeDestruction);
        Destroy(gameObject);
    }
}
