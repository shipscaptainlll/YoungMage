using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameMagnetismProduct : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateCollisionProduct(GameObject firstElement, GameObject secondElement)
    {
        if (firstElement != null && secondElement != null)
        {
            Vector3 instantiationPosition = firstElement.transform.position + (firstElement.transform.position - secondElement.transform.position) / 2;
            firstElement.SetActive(false);
            secondElement.SetActive(false);
            int firstElementCount = firstElement.GetComponent<GlobalResource>().Count;
            int secondElementCount = secondElement.GetComponent<GlobalResource>().Count;
            GameObject collisionProduct = Instantiate(secondElement, instantiationPosition, Quaternion.Euler(new Vector3(0, 0, 0)));
            Destroy(firstElement);
            Destroy(secondElement);
            collisionProduct.SetActive(true);
            Rigidbody productRigidbody = collisionProduct.transform.GetComponent<Rigidbody>();
            ResourcesSameMagnetism resourcesSameMagnetism = collisionProduct.transform.Find("SameResourceMagnetism(Clone)").GetComponent<ResourcesSameMagnetism>();
            resourcesSameMagnetism.StopAllCoroutines();
            resourcesSameMagnetism.ContactedObjects.Clear();
            productRigidbody.velocity = new Vector3(0, 0, 0);
            productRigidbody.AddTorque(new Vector3(100, 0, 100));
            productRigidbody.useGravity = true;
            collisionProduct.transform.position = instantiationPosition;
            collisionProduct.GetComponent<GlobalResource>().Count = firstElementCount + secondElementCount;
        }        
    }
}
