using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectableResource : MonoBehaviour
{
    [SerializeField] ParticleSystem destructionPS;
    
    bool enabled;
    Transform targetConnection;
    Vector3 targetConnectionPosition;


    public event Action<Transform, Transform> ContactedResource = delegate { };

    public bool Enabled { get { return enabled; } set { enabled = value; DestroyOre(); } }
    public Transform TargetConnection { get { return targetConnection; } set { targetConnection = value; GetTargetPosition(); } }
    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ConnectableResource>() != null && other.GetComponent<HandResource>() == null)
        {
            if (ContactedResource != null) { ContactedResource(transform, other.transform); }
        }
    }

    void GetTargetPosition()
    {
        if (targetConnection != null)
        {
            targetConnectionPosition = targetConnection.position;
        }
    }

    void DestroyOre()
    {
        if (!enabled)
        {
            StartCoroutine(Destruction(0));
        }
    }

    IEnumerator Destruction(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Being destroyed ");
        Transform destructionParticleSystem = Instantiate(destructionPS.transform, transform.position, transform.rotation);
        //destructionParticleSystem.parent = transform;
        
        destructionParticleSystem.GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity / 5;
        DirectParticles(destructionParticleSystem);
        destructionParticleSystem.transform.gameObject.SetActive(true);
        destructionParticleSystem.GetComponent<ParticleSystem>().Play();
        Destroy(transform.gameObject);
        yield return new WaitForSeconds(3);
        
    }

    void DirectParticles(Transform particlesTransform)
    {
        if (targetConnection != null)
        {
            Vector3 distance = (transform.position - targetConnection.position).normalized;
            particlesTransform.GetComponent<Rigidbody>().velocity = - distance * 0.42f;
        } else if (targetConnectionPosition != null)
        {
            Vector3 distance = (transform.position - targetConnectionPosition).normalized;
            particlesTransform.GetComponent<Rigidbody>().velocity = - distance * 0.42f;
        } else
        {

        }
    }

    public void DissolvingDestruction()
    {
        enabled = false;
        
    }
}
