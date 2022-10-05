using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CrouchingYoungmage>() != null)
        {
            other.GetComponent<CrouchingYoungmage>().StartCrouching();
        } else if (other.GetComponent<SkeletonBehavior>() != null)
        {
            other.GetComponent<CrouchingSkeleton>().StartCrouching();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CrouchingYoungmage>() != null)
        {
            other.GetComponent<CrouchingYoungmage>().StopCrouching();
        } else if (other.GetComponent<SkeletonBehavior>() != null)
        {
            other.GetComponent<CrouchingSkeleton>().StopCrouching();
        }
    }
}
