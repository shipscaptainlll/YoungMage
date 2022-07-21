using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningProductPopuper : MonoBehaviour
{
    [SerializeField] Transform countUIAddon;
    [SerializeField] float popupForce;
    [SerializeField] PhysicMaterial oreFrictionMaterial;
    [SerializeField] ParticleSystem boomParticles;
    Transform popupOrigin;
    Transform objectReference;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        popupOrigin = transform.Find("PopupPortal");
        random = new System.Random();
        objectReference = transform.parent.GetComponent<OreMiningManager>().ProductInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) { PopupProduct(); }
    }

    public void PopupProduct()
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        //Transform objectReference = objectManager.TakeObject(quickAccessHandController.CurrentCustomID).transform;

        var newObject = Instantiate(objectReference, popupOrigin, popupOrigin);
        newObject.localPosition = new Vector3(0, 0, 0);
        newObject.gameObject.AddComponent<Rigidbody>();
        newObject.gameObject.GetComponent<Rigidbody>().mass = 1;
        newObject.gameObject.GetComponent<Rigidbody>().drag = 1;
        newObject.gameObject.AddComponent<Rotate>();
        Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 10; rotator.YAxis = 10; rotator.ZAxis = 10;
        newObject.gameObject.AddComponent<SphereCollider>();
        newObject.gameObject.GetComponent<SphereCollider>().material = oreFrictionMaterial;
        //newObject.gameObject.GetComponent<SphereCollider>().isTrigger = true;
        newObject.gameObject.GetComponent<SphereCollider>().radius = 0.5f;
        //newObject.gameObject.AddComponent<DefractorResource>();
        //newObject.gameObject.GetComponent<DefractorResource>().ID = quickAccessHandController.CurrentCustomID;
        //newObject.gameObject.GetComponent<DefractorResource>().DestroyableObjects = destroyableObjects;
        //newObject.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
        //newObject.gameObject.AddComponent<GlobalResource>();
        //newObject.gameObject.GetComponent<GlobalResource>().TargetLayerMask = midasLayerHolder.GetComponent<LayerMaskSettings>().TargetLayer;
        //newObject.gameObject.GetComponent<GlobalResource>().ID = quickAccessHandController.CurrentCustomID;
        Transform countUIAdd = Instantiate(countUIAddon, newObject.position + new Vector3(0, 0.5f, 0), newObject.rotation);
        countUIAdd.gameObject.SetActive(true);
        countUIAdd.parent = newObject;
        newObject.gameObject.AddComponent<MidasResource>();
        newObject.gameObject.GetComponent<Rigidbody>().AddForce(popupOrigin.forward * popupForce);
        newObject.gameObject.GetComponent<Rigidbody>().angularDrag = 150f;
        newObject.gameObject.GetComponent<Rigidbody>().drag = 1f;
        newObject.gameObject.GetComponent<Rigidbody>().AddTorque(100000 * xTorque, 100000 * yTorque, 100000 * zTorque);
        
        boomParticles.Play();
    }
}
