using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalResource : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    
    int id;
    bool activatedMagnetism = false;
    Coroutine delayMagnetism;

    public LayerMask TargetLayerMask { set { layerMask = value; } }
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
        //Debug.Log(other.gameObject.layer + " " + other.transform);
        if (other.gameObject.layer == 6 && id != 1
            || other.gameObject.layer == 0 && id != 1)
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
        if (other.gameObject.layer == 15)
        {
            //Debug.Log("entered magnet " + transform);
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
        Debug.Log("hello");
        activatedMagnetism = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
