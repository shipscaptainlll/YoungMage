using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalResource : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] int id;
    int count = 1;
    bool collidingSameResource;


    bool wasCollected;
    bool activatedMagnetism = false;
    Coroutine delayMagnetism;

    public bool WasCollected { get { return wasCollected; } set { wasCollected = value; } }
    public int Count { get { return count; } set { count = value; if (CountChanged != null) { CountChanged(); Debug.Log("count changed" + count + " on " + transform); } } }
    public LayerMask TargetLayerMask { set { layerMask = value; } }
    public bool CollidingSameResource { get { return collidingSameResource; } set { collidingSameResource = value; } }
    public int ID { get { return id; } set { id = value; } }

    public event Action CountChanged = delegate { };
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
        //Debug.Log(other.gameObject.layer + " " + other.transform);
        if (!collidingSameResource && other.gameObject.layer == 6 && id != 1 
            )
        {
            //|| other.gameObject.layer == 0 && id != 1 && other.gameObject.GetComponent<GlobalResource>() == null
            //Debug.Log(other.gameObject.layer + " " + other.transform);
            GetComponent<SphereCollider>().isTrigger = false;
            //transform.Find("SameResourceMagnetism(Clone)")?.gameObject.SetActive(true);
            //if (Vector3.Distance(transform.position, other.transform.position) > 2f)
            //{
            //    transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0));
            //}
            //Debug.Log(transform.GetComponent<Rigidbody>().velocity.y);
            //transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100f, 0));
        }
        if (other.gameObject.layer == 15)
        {
            //Debug.Log("entered magnet " + transform);
        }
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        if (!collidingSameResource && other.gameObject.layer == 6 && id != 1)
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 1.2f)
            {
                Debug.Log("trying to leave" + Vector3.Distance(transform.position, other.transform.position) + other.transform);
                transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 37.5f, 0));
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 15)
        {
            //Debug.Log("leaved magnet " + transform);
        }
    }

    public void ResetMagneticField()
    {
        Debug.Log("leaved magnet ");
    }

    public void ActivateInventoryMagnetism(Transform startMovementParticles)
    {
        if (!activatedMagnetism)
        {
            activatedMagnetism = true;
            GetComponent<Rigidbody>().useGravity = false;
            Instantiate(startMovementParticles, transform.position, transform.rotation);
        }
        StopAllCoroutines();
        delayMagnetism = StartCoroutine(delayStopMagnetism());
    }

    IEnumerator delayStopMagnetism()
    {

        yield return new WaitForSeconds(0.05f);
        //Debug.Log("hello");
        activatedMagnetism = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
    */
}
