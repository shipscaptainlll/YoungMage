using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesSameMagnetism : MonoBehaviour
{
    public SameMagnetismProduct sameMagnetismProduct;
    int id;
    List<Transform> contactedObjects = new List<Transform>();
    Coroutine pullingCoroutine = null;

    public List<Transform> ContactedObjects { get { return contactedObjects; } }
    public int ID { get { return id; } set { id = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.transform.parent?.GetComponent<GlobalResource>() != null && other.transform.parent.GetComponent<GlobalResource>().ID == id)
        {
            //Debug.Log("collided with " + other.transform.parent);
            ProcessObject(other.transform.parent.gameObject);
            
        }
        /*
        if (transform.parent.GetComponent<GlobalResource>().CollidingSameResource && other.gameObject.GetComponent<GlobalResource>() == null)
        {

        }
        */
    }

    IEnumerator PullObject()
    {
        while (true)
        {
            foreach (Transform element in contactedObjects)
            {
                if (element != null)
                {
                    element.GetComponent<Rigidbody>().AddForce((transform.parent.position - element.transform.position) * Time.deltaTime * 150);
                    if (Vector3.Distance(element.transform.position, transform.parent.position) < 0.1f)
                    {
                        //Debug.Log(sameMagnetismProduct);
                        sameMagnetismProduct.InstantiateCollisionProduct(element.transform.gameObject, this.transform.parent.gameObject);
                    }
                } 
                
            }
            yield return null;
        }
        
    }

    void ProcessObject(GameObject objectToPull)
    {
        if (!contactedObjects.Contains(objectToPull.transform))
        {
            contactedObjects.Add(objectToPull.transform);
            StartPulling();
            //Debug.Log("Found same object");
        }
    }

    void StartPulling()
    {
        if (pullingCoroutine == null && contactedObjects != null)
        {
            pullingCoroutine = StartCoroutine(PullObject());
        }
    }
}
