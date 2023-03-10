using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPortalActivator : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    bool insidePortal;

    public bool InsidePortal { get { return insidePortal; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = true;
            particleSystem.Play();
            //Debug.Log("character entered");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = true;
            //Debug.Log("character stays");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            insidePortal = false;
            particleSystem.Stop();
            //Debug.Log("character got out");
        }
    }
}
