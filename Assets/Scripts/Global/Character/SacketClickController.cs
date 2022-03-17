using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SacketClickController : MonoBehaviour
{
    [SerializeField] DestroyableObjects destroyableObjects;
    [SerializeField] Transform VFX;
    [SerializeField] float xForcePower;
    [SerializeField] float yForcePower;
    [SerializeField] float xAngleOffset;
    [SerializeField] ClickManager clickManager;
    [SerializeField] CameraController cameraController;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform characterHand;
    [SerializeField] ParticleSystem destroyParticles;
    [SerializeField] Transform midasLayerHolder;
    List<Transform> KickedOutItems = new List<Transform>();
    Coroutine delayCoroutine = null;
    bool delayActive;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        clickManager.QClicked += KickOutItem;
        clickManager.QLClicked += KickOutItem;
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KickOutItem()
    {
        if (!delayActive)
        {
            
            float xTorque = (float)random.Next(-2, 2);
            float yTorque = (float)random.Next(-2, 2);
            float zTorque = (float)random.Next(-2, 2);
            delayActive = true;
            //Debug.Log(quickAccessHandController.CurrentSlot);
            //Debug.Log(quickAccessHandController.CurrentCustomID);
            Transform objectReference = objectManager.TakeObject(quickAccessHandController.CurrentCustomID).transform;

            var newObject = Instantiate(objectReference, characterHand.position, characterHand.rotation);
            newObject.gameObject.AddComponent<Rigidbody>();
            newObject.gameObject.AddComponent<Rotate>();
            Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = 1;
            newObject.gameObject.AddComponent<SphereCollider>();
            newObject.gameObject.GetComponent<SphereCollider>().isTrigger = true;
            newObject.gameObject.GetComponent<SphereCollider>().radius = 0.10f;
            newObject.gameObject.AddComponent<DefractorResource>();
            newObject.gameObject.GetComponent<DefractorResource>().ID = quickAccessHandController.CurrentCustomID;
            newObject.gameObject.GetComponent<DefractorResource>().DestroyableObjects = destroyableObjects;
            newObject.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
            newObject.gameObject.AddComponent<GlobalResource>();
            newObject.gameObject.GetComponent<GlobalResource>().TargetLayerMask = midasLayerHolder.GetComponent<LayerMaskSettings>().TargetLayer;
            newObject.gameObject.GetComponent<GlobalResource>().ID = quickAccessHandController.CurrentCustomID;
            newObject.gameObject.AddComponent<MidasResource>();
            newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * (Mathf.Cos(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) + xAngleOffset) * xForcePower);
            newObject.gameObject.GetComponent<Rigidbody>().AddTorque(100 * xTorque, 100 * yTorque, 100 * zTorque);
            if (-cameraController.YRotation > 0)
            {
                newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * yForcePower);
            } else { newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * -yForcePower); }
            
            //newObject.GetComponent<Rigidbody>().useGravity = enabled;
            KickedOutItems.Add(newObject);
            foreach (Transform element in KickedOutItems)
            {
                //Debug.Log(element);
            }
            delayCoroutine = StartCoroutine(WaitDelay());
            TakeFromCounter();
        }
        
    }

    IEnumerator WaitDelay()
    {
        VFX.GetComponent<VisualEffect>().enabled = true;
        quickAccessHandController.MakeObjectInvisible();
        yield return new WaitForSeconds(0.12f);
        delayActive = false;
        //Debug.Log("hello");
        ResetCoroutineData();
        quickAccessHandController.MakeObjectVisible();
        VFX.GetComponent<VisualEffect>().enabled = false;
    }

    void ResetCoroutineData()
    {
        if (delayCoroutine != null) { StopCoroutine(delayCoroutine); delayCoroutine = null; }
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
                
                Destroy(element.gameObject);
                //Debug.Log("was destroyed");
                break;
            }
        }
    }
    
    void TakeFromCounter()
    {
        quickAccessHandController.CurrentCounter.GetComponent<ICounter>().GetResource(1);
    }
}
