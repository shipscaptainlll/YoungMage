using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningProductPopuper : MonoBehaviour
{
    [SerializeField] Transform countUIAddon;
    [SerializeField] float popupForce;
    [SerializeField] PhysicMaterial oreFrictionMaterial;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ObjectsConnector objectsConnector;
    Transform popupOrigin;
    Transform objectReference;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        popupOrigin = transform.Find("PopupPortal");
        random = new System.Random();
        //objectReference = transform.parent.GetComponent<OreMiningManager>().ProductInstance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y)) { PopupProduct(); }
    }

    public void PopupProduct(Transform oreReference)
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        //Transform objectReference = objectManager.TakeObject(quickAccessHandController.CurrentCustomID).transform;

        var newObject = Instantiate(oreReference, popupOrigin, popupOrigin);
        objectsConnector.SubscribeOnOre(newObject);
        if (newObject.GetChild(0).GetChild(0) != null)
        {
            var newObjectFine = newObject.GetChild(0);
            newObject.localPosition = new Vector3(0, 0, 0);
            newObjectFine.localPosition = new Vector3(0, 0, 0);
            newObjectFine.gameObject.AddComponent<Rigidbody>();
            newObjectFine.gameObject.GetComponent<Rigidbody>().mass = 1;
            newObjectFine.gameObject.GetComponent<Rigidbody>().drag = 1;
            newObjectFine.gameObject.AddComponent<Rotate>();
            Rotate rotator = newObjectFine.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 10; rotator.YAxis = 10; rotator.ZAxis = 10;
            //newObjectFine.gameObject.AddComponent<SphereCollider>();
            //newObjectFine.gameObject.GetComponent<SphereCollider>().material = oreFrictionMaterial;
            //newObjectFine.gameObject.GetComponent<SphereCollider>().radius = 0.0051f;
            //Transform countUIAdd = Instantiate(countUIAddon, newObjectFine.position + new Vector3(0, 0.5f, 0), newObjectFine.rotation);
            //countUIAdd.gameObject.SetActive(true);
            //countUIAdd.parent = newObject;
            newObjectFine.gameObject.AddComponent<MidasResource>();
            newObjectFine.gameObject.GetComponent<Rigidbody>().AddForce(popupOrigin.forward * popupForce);
            newObjectFine.gameObject.GetComponent<Rigidbody>().angularDrag = 150f;
            newObjectFine.gameObject.GetComponent<Rigidbody>().drag = 1f;

            boomParticles.Play();
        } else
        {
            newObject.localPosition = new Vector3(0, 0, 0);
            newObject.gameObject.AddComponent<Rigidbody>();
            newObject.gameObject.GetComponent<Rigidbody>().mass = 1;
            newObject.gameObject.GetComponent<Rigidbody>().drag = 1;
            newObject.gameObject.AddComponent<Rotate>();
            Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 10; rotator.YAxis = 10; rotator.ZAxis = 10;
            //newObject.gameObject.AddComponent<SphereCollider>();
            //newObject.gameObject.GetComponent<SphereCollider>().material = oreFrictionMaterial;
            //newObject.gameObject.GetComponent<SphereCollider>().radius = 0.0051f;
            //Transform countUIAdd = Instantiate(countUIAddon, newObject.position + new Vector3(0, 0.5f, 0), newObject.rotation);
            //countUIAdd.gameObject.SetActive(true);
            //countUIAdd.parent = newObject;
            newObject.gameObject.AddComponent<MidasResource>();
            newObject.gameObject.GetComponent<Rigidbody>().AddForce(popupOrigin.forward * popupForce);
            newObject.gameObject.GetComponent<Rigidbody>().angularDrag = 150f;
            newObject.gameObject.GetComponent<Rigidbody>().drag = 1f;

            boomParticles.Play();
        }
        
    }
}
