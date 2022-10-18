using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundResource : MonoBehaviour
{
    [SerializeField] OreLevitator oreLevitator;

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
            //Destroy(transform.parent.parent.gameObject);
        }
    }
}
