using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoEffect : MonoBehaviour
{
    [SerializeField] Transform tornadoCenter;
    [SerializeField] float maxOffset;
    [SerializeField] float pullForce;
    [SerializeField] float refreshRate;
    System.Random random;

    public void Start()
    {
        random = new System.Random();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tornadable")
        {
            StartCoroutine(pullObject(other, true));
            //InitialVelocity(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tornadable")
        {
            StartCoroutine(pullObject(other, false));
        }
    }

    IEnumerator pullObject(Collider x, bool shouldPull)
    {
        if (shouldPull)
        {
            Vector3 ForeDir = tornadoCenter.position - x.transform.position;
            float distance = Vector3.Distance(tornadoCenter.position, x.transform.position);
            if (Mathf.Abs(distance) < maxOffset)
            {
                x.GetComponent<Rigidbody>().AddForce(ForeDir.normalized * -pullForce * 0.5f* Time.deltaTime);
            } else
            {
                x.GetComponent<Rigidbody>().AddForce(ForeDir.normalized * pullForce * Time.deltaTime);
            }
            
            yield return refreshRate;
            StartCoroutine(pullObject(x, shouldPull));
        }
    }

    public void pullObjectNormal(Collider x)
    {
        Vector3 ForeDir = tornadoCenter.position - x.transform.position;
        float distance = Vector3.Distance(tornadoCenter.position, x.transform.position);
        if (Mathf.Abs(distance) < maxOffset)
        {
            x.GetComponent<Rigidbody>().AddForce(ForeDir.normalized * -pullForce * 0.5f * Time.deltaTime);
        }
        else
        {
            x.GetComponent<Rigidbody>().AddForce(ForeDir.normalized * pullForce * Time.deltaTime);
        }

    }

    void InitialVelocity(Collider x)
    {
        float originMass = tornadoCenter.GetComponent<Rigidbody>().mass;
        float distance = Vector3.Distance(tornadoCenter.position, x.transform.position);
        x.transform.LookAt(x.transform);

        x.GetComponent<Rigidbody>().velocity += x.transform.right * Mathf.Sqrt((9.8f * originMass) / distance);
    }
}
