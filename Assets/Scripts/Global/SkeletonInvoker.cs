using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonInvoker : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] GameObject skeleton;
    Transform targetOre;

    public Transform TargetOre => targetOre;
    

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
        targetOre = detectedOre;
        Debug.Log("1:"  + targetOre);
        Vector3 offsetPosition = new Vector3(0, 1, 30);
        Quaternion zeroRotation = Quaternion.Euler(0, -90, 0);
        GameObject newShinySkeleton = Instantiate(skeleton, detectedOre.position + offsetPosition, zeroRotation);
        newShinySkeleton.GetComponent<SkeletonBehavior>().AddTarget(detectedOre);
    }
}
