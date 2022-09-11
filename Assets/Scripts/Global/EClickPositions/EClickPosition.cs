using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EClickPosition : MonoBehaviour
{
    [SerializeField] string tag;

    public event Action<string> SomethingEntered = delegate { };
    public event Action<string> SomethingLeaved = delegate { };
    public string Tag { get { return tag; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("something entered");
            SomethingEntered(tag);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            //Debug.Log("something left");
            SomethingLeaved(tag);
        }
        
    }
}
