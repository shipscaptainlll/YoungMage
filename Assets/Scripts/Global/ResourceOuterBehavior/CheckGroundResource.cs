using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundResource : MonoBehaviour
{
    [SerializeField] OreLevitator oreLevitator;
    [SerializeField] OreCounter oreCounter;
    [SerializeField] ParticleSystem firstCircle;
    [SerializeField] ParticleSystem secondCircle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 24
            )
        {
            //Debug.Log(other.gameObject.layer + " contacted ground " + other.transform);
            oreLevitator.ActivateLevitation();
            oreCounter.ShowCounter();
            firstCircle.Play();
            secondCircle.Play();
            //Destroy(transform.parent.parent.gameObject);
        }
    }
}
