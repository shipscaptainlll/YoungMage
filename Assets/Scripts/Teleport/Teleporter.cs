using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Teleporter Other;
    Transform objectCache;
    bool teleportUsed = false;
    Vector3 localPos;
    Quaternion difference;

    public event Action TeleportFound = delegate { };
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


        if (zPos < 0)
        {
                if (TeleportFound != null)
                {
                    TeleportFound();
                }
                Teleport(other.transform);
            
        }
    }

    private void Teleport(Transform obj)
    {
        objectCache = obj;
        // Position
        if (!teleportUsed)
        {
            localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(obj.position);
            localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
            difference = Other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
            teleportUsed = true;
        }
        Debug.Log(Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos));
        obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos) + new Vector3(0f,0,0);
        //obj.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
        Debug.Log(obj.gameObject + " " + obj.position);
        // Rotation
        
        
        obj.rotation = difference * obj.rotation;
    }

    void setPosition()
    {
        
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
