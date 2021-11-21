using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInvoker : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] GameObject skeleton;
    

    void Start()
    {
        contactManager.OreDetected += InvokeSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InvokeSkeleton(Transform detectedOre)
    {
        Vector3 offsetPosition = new Vector3(0, 1, 3);
        Quaternion zeroRotation = Quaternion.Euler(0, -90, 0);
        GameObject newShinySkeleton = Instantiate(skeleton, detectedOre.position + offsetPosition, zeroRotation);
        Destroy(newShinySkeleton, 10f);
    }
}
