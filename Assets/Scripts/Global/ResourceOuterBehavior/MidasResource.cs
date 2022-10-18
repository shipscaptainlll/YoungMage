using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasResource : MonoBehaviour
{
    Transform necklessOne;
    Transform necklessTwo;
    Transform counter;
    bool beingDissolved;


    public bool BeingDissolved { get { return beingDissolved; } set { beingDissolved = value; } }
    // Start is called before the first frame update
    void Start()
    {
        necklessOne = transform.GetChild(0).Find("NecklessParticleSystem");
        necklessTwo = transform.GetChild(0).Find("NecklessParticleSystem (1)");
        counter = transform.GetChild(0).Find("OreCounter");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateAfterCreation()
    {
        
    }

    public void DissolvingDestruction()
    {
        necklessOne.gameObject.SetActive(false);
        necklessTwo.gameObject.SetActive(false);
        counter.gameObject.SetActive(false);
    }
}
