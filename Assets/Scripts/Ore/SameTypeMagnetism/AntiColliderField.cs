using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiColliderField : MonoBehaviour
{
    int id;

    public int ID { get { return id; } set { id = value; } }
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
        
        if (other.transform.parent?.parent?.GetComponent<GlobalResource>() != null && other.transform.parent.parent.GetComponent<GlobalResource>().ID == id)
        {
            //Debug.Log("anticollided with " + other.transform.parent.parent);
            other.transform.parent.parent.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //Debug.Log("angular velocity " + other.transform.parent.parent.GetComponent<Rigidbody>().angularVelocity);
            Vector3 angularVelocity = other.transform.parent.parent.GetComponent<Rigidbody>().angularVelocity;
            float xAngularVelocity = Mathf.Clamp(angularVelocity.x, -1, 1);
            float yAngularVelocity = Mathf.Clamp(angularVelocity.y, -1, 1);
            float zAngularVelocity = Mathf.Clamp(angularVelocity.z, -1, 1);
            //Debug.Log(xAngularVelocity);
            //other.transform.parent.parent.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            //other.transform.parent.parent.GetComponent<Rigidbody>().AddTorque(new Vector3(xAngularVelocity * 100, yAngularVelocity, zAngularVelocity * 100));
            //Debug.Log("changed angular velocity " + other.transform.parent.parent.GetComponent<Rigidbody>().angularVelocity);
            other.transform.parent.parent.GetComponent<GlobalResource>().CollidingSameResource = true;
            other.transform.parent.parent.GetComponent<SphereCollider>().isTrigger = true;
            
            other.transform.parent.parent.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
