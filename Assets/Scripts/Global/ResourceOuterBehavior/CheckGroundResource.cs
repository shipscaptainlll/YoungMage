using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundResource : MonoBehaviour
{
    [SerializeField] OreLevitator oreLevitator;
    [SerializeField] OreCounter oreCounter;
    [SerializeField] ParticleSystem firstCircle;
    [SerializeField] ParticleSystem secondCircle;
    bool checkGroundActivated;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("just contacted " + other + " with layer mask " + other.gameObject.layer);
        if (!checkGroundActivated && 
            (other.gameObject.layer == 6 || other.gameObject.layer == 24))
        {
            checkGroundActivated = true;
            oreLevitator.ActivateLevitation();
            oreCounter.ShowCounter();
            firstCircle.Play();
            secondCircle.Play();
        }
    }
}
