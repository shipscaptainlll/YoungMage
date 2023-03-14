using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoObjectsCatcher : MonoBehaviour
{
    [SerializeField] SacketResourceCatcher sacketResourceCatcher;
    [SerializeField] ParticleSystem flyingParticleSystem;
    [SerializeField] Transform countersHolder;
    [SerializeField] SacketMagnetism sacketMagnetism;
    [SerializeField] Transform catchParticles;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource acquiringObject;

    BoxCollider transformBoxcollider;
    // Start is called before the first frame update
    void Start()
    {
        acquiringObject = soundManager.FindSound("ObjectAcquiringFirst");
        transformBoxcollider = transform.GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        
        //Debug.Log("Found some " + other.transform);
        
        if (other.GetComponent<GlobalResource>() != null 
            || other.transform.parent != null 
            && other.transform.parent.GetComponent<GlobalResource>() != null
            )
        {
            
            
            if (other.transform.GetComponent<ConnectableResource>() != null && other.transform.GetComponent<ConnectableResource>().OreLevitator.LevitationActivated) 
            { 
                other.transform.GetComponent<ConnectableResource>().OreLevitator.DeactivateLevitation(); Debug.Log("Ore levitation stopped"); 
            }
            if (Mathf.Abs(other.transform.position.x - transform.TransformPoint(transformBoxcollider.center).x) <= 0.5f
                && Mathf.Abs(other.transform.position.y - transform.TransformPoint(transformBoxcollider.center).y) <= 0.5f)
            {
                if (!other.GetComponent<GlobalResource>().WasCollected)
                {
                    itemsCounterQuests.countQuestItem(other.GetComponent<GlobalResource>().ID);
                }
                CatchResource(other.transform);
            } else
            {
                float distance = Vector3.Distance(other.transform.position, transform.TransformPoint(transformBoxcollider.center));
                Vector3 tornadoDirection = -(other.transform.position - transform.TransformPoint(transformBoxcollider.center)).normalized * 15f;
                tornadoDirection.y = tornadoDirection.y * 1.75f;
                if (tornadoDirection.y < 0) { tornadoDirection.y = 0; }
                other.transform.RotateAround(transform.TransformPoint(transformBoxcollider.center), Vector3.up, 100 * Time.deltaTime);
                other.transform.GetComponent<Rigidbody>().AddForce(tornadoDirection);
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log("Found some " + other.transform);

        if (other.GetComponent<GlobalResource>() != null ||
            other.transform.parent != null && other.transform.parent.GetComponent<GlobalResource>() != null)
        {

            ParticleSystem flyingParticles = Instantiate(flyingParticleSystem, other.transform.position, other.transform.rotation);
            //flyingParticles.transform.localScale = new Vector3(1, 1, 1);
            flyingParticles.transform.name = "flying particles";
            flyingParticles.transform.parent = other.transform;
            flyingParticles.transform.position = other.transform.position;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        //Debug.Log("Found some " + other.transform);

        if (other.GetComponent<GlobalResource>() != null ||
            other.transform.parent != null && other.transform.parent.GetComponent<GlobalResource>() != null)
        {
            if (other.transform.Find("flying particles") != null)
            {
                Destroy(other.transform.Find("flying particles").gameObject);
            }
            
        }

    }


    void CatchResource(Transform resource)
    {
        if (!resource.GetComponent<OreCounter>().CatchedByTornado)
        {
            resource.GetComponent<OreCounter>().CatchedByTornado = true;
            Debug.Log("catching this ");
            AddToCounter(resource.GetComponent<GlobalResource>().ID, resource.GetComponent<OreCounter>().OreCount);
            HoldResource(resource);
            StartCoroutine(DestroyObject(resource));
            acquiringObject.Play();
            //InstantiateCatchParticles(resource);
        }

    }

    void HoldResource(Transform resource)
    {
        Destroy(resource.GetComponent<Rigidbody>());
    }

    IEnumerator DestroyObject(Transform resource)
    {
        yield return new WaitForSeconds(1);
        Destroy(resource.gameObject);
    }

    void AddToCounter(int customerID, int count)
    {
        foreach (Transform holder in countersHolder)
        {
            foreach (Transform counter in holder)
            {
                if (counter.GetComponent<ICounter>().ID == customerID)
                {
                    
                    counter.GetComponent<ICounter>().AddResource(count);
                    Debug.Log("Just added count " + count);
                    return;
                }
            }
        }
    }
    void InstantiateCatchParticles(Transform resource)
    {
        Instantiate(catchParticles, transform.position, transform.rotation);
    }
}
