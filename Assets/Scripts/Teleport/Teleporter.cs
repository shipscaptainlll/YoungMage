using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter Other;
    Transform objectCache;
    private void Start()
    {

    }

    private void Update()
    {
        if (objectCache != null)
        {
            //Debug.Log(objectCache.gameObject + " " + objectCache.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float zPos = transform.worldToLocalMatrix.MultiplyPoint3x4(other.transform.position).z;

        if (zPos < 0) Teleport(other.transform);
    }

    private void Teleport(Transform obj)
    {
        objectCache = obj;
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        Debug.Log(Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos));
        obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
        //obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
        Debug.Log(obj.gameObject + " " + obj.position);
        // Rotation
        Quaternion difference = Other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        obj.rotation = difference * obj.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }
}
