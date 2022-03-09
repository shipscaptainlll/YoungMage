using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacketClickController : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform characterHand;
    [SerializeField] ParticleSystem destroyParticles;
    List<Transform> KickedOutItems = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        clickManager.QClicked += KickOutItem;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KickOutItem()
    {
        Debug.Log(quickAccessHandController.CurrentSlot);
        Debug.Log(quickAccessHandController.CurrentCustomID);
        Transform objectReference = objectManager.TakeObject(quickAccessHandController.CurrentCustomID).transform;
        
        var newObject = Instantiate(objectReference, characterHand.position, characterHand.rotation);
        newObject.gameObject.AddComponent<Rigidbody>();
        newObject.gameObject.AddComponent<SphereCollider>();
        newObject.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        newObject.gameObject.AddComponent<DefractorResource>();
        newObject.gameObject.GetComponent<DefractorResource>().ID = quickAccessHandController.CurrentCustomID;
        newObject.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
        //newObject.GetComponent<Rigidbody>().useGravity = enabled;
        KickedOutItems.Add(newObject);
        foreach (Transform element in KickedOutItems)
        {
            Debug.Log(element);
        }
    }

    void DestroyObject(Transform objectTransform)
    {
        foreach (Transform element in KickedOutItems)
        {
            if (element == objectTransform)
            {
                element.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
                KickedOutItems.Remove(element);
                var particles = Instantiate(destroyParticles, element.position, element.rotation);
                particles.gameObject.SetActive(true);
                particles.transform.position += new Vector3(0, 0.65f, 0);
                Destroy(element.gameObject);
                Debug.Log("was destroyed");
                break;
            }
        }
    }
}
