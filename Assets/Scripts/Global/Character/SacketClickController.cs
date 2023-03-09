using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SacketClickController : MonoBehaviour
{
    [SerializeField] Transform magnetismAddOn;
    [SerializeField] Transform anticolliderAddOn;
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
    [SerializeField] Transform countUIAddon;
    [SerializeField] BookSpellsActivator bookSpellsActivator;
    [SerializeField] ObjectsConnector objectsConnector;
    [SerializeField] Transform contactableObjectsPool;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    
    SameMagnetismProduct sameMagnetismProduct;
    List<Transform> KickedOutItems = new List<Transform>();
    Coroutine delayCoroutine = null;
    bool delayActive;
    System.Random random;

    AudioSource popUpSound;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hello there");
        
        clickManager.TabClicked += KickOutItem;
        //clickManager.TabClicked += KickOutItem;
        random = new System.Random();
        sameMagnetismProduct = GameObject.Find("SameMagnetismProduct").GetComponent<SameMagnetismProduct>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void KickOutItem()
    {
        if (!delayActive)
        {
            //Debug.Log(transform);
            bookSpellsActivator.CastThrowObject();
            bookSpellsActivator.CastNullSpell();

            if(quickAccessHandController.CurrentCustomID == 10) { 
                return;
            }
            
            if (objectManager.TakeObject(quickAccessHandController.CurrentCustomID) != null)
            {
                float xTorque = (float)random.Next(-2, 2);
                float yTorque = (float)random.Next(-2, 2);
                float zTorque = (float)random.Next(-2, 2);
                delayActive = true;
                //Debug.Log(quickAccessHandController.CurrentSlot);
                //Debug.Log(quickAccessHandController.CurrentCustomID);
                Transform objectReference = objectManager.TakeObject(quickAccessHandController.CurrentCustomID).transform;

                var newObject = Instantiate(objectReference, characterHand.position, characterHand.rotation);
                objectsConnector.SubscribeOnOre(newObject);
                newObject.gameObject.AddComponent<Rigidbody>();
                newObject.gameObject.AddComponent<Rotate>();
                Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = 1;
                //newObject.gameObject.AddComponent<SphereCollider>();
                //newObject.gameObject.GetComponent<SphereCollider>().isTrigger = true;
                //newObject.gameObject.GetComponent<SphereCollider>().radius = 0.16f;
                //newObject.GetChild(0).gameObject.AddComponent<SphereCollider>();
                //newObject.GetChild(0).gameObject.GetComponent<SphereCollider>().isTrigger = true;
                //newObject.GetChild(0).gameObject.GetComponent<SphereCollider>().radius = 0.005f;
                newObject.gameObject.AddComponent<DefractorResource>();
                newObject.gameObject.GetComponent<DefractorResource>().ID = quickAccessHandController.CurrentCustomID;
                newObject.gameObject.GetComponent<DefractorResource>().DestroyableObjects = destroyableObjects;
                newObject.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
                newObject.gameObject.GetComponent<DefractorResource>().SoundManagerScript = soundManager;
                newObject.gameObject.AddComponent<GlobalResource>();
                newObject.gameObject.GetComponent<GlobalResource>().WasCollected = true;
                newObject.gameObject.GetComponent<GlobalResource>().TargetLayerMask = midasLayerHolder.GetComponent<LayerMaskSettings>().TargetLayer;
                newObject.gameObject.GetComponent<GlobalResource>().ID = quickAccessHandController.CurrentCustomID;
                newObject.gameObject.GetComponent<GlobalResource>().GlobalSoundManager = soundManager;
                //missed somewhereTransform magneticAdd = Instantiate(magnetismAddOn, newObject.position, newObject.rotation);
                //Transform countUIAdd = Instantiate(countUIAddon, newObject.position + new Vector3(0, 0.5f, 0), newObject.rotation);
                //countUIAdd.gameObject.SetActive(true);
                //missed somewheremagneticAdd.parent = newObject;
                //countUIAdd.parent = newObject;
                //Transform anticolliderAdd = Instantiate(anticolliderAddOn, magneticAdd.position, magneticAdd.rotation);
                //anticolliderAdd.parent = magneticAdd;
                //newObject.Find("SameResourceMagnetism(Clone)").Find("AntiColliderField").GetComponent<AntiColliderField>().ID = quickAccessHandController.CurrentCustomID;
                //newObject.Find("SameResourceMagnetism(Clone)").gameObject.GetComponent<ResourcesSameMagnetism>().ID = quickAccessHandController.CurrentCustomID;
                //Debug.Log(sameMagnetismProduct);
                //newObject.Find("SameResourceMagnetism(Clone)").gameObject.GetComponent<ResourcesSameMagnetism>().sameMagnetismProduct = sameMagnetismProduct;
                //newObject.gameObject.AddComponent<MidasResource>();
                //newObject.gameObject.GetComponent<MidasResource>().InstantiateAfterCreation();
                newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * (Mathf.Cos(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) + xAngleOffset) * xForcePower);
                newObject.gameObject.GetComponent<Rigidbody>().angularDrag = 0.75f;
                newObject.gameObject.GetComponent<Rigidbody>().drag = 0.4f;
                newObject.gameObject.GetComponent<Rigidbody>().AddTorque(100 * xTorque, 100 * yTorque, 100 * zTorque);
                if (-cameraController.YRotation > 0)
                {
                    newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * yForcePower);
                }
                else { newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * -yForcePower); }

                //newObject.GetComponent<Rigidbody>().useGravity = enabled;
                KickedOutItems.Add(newObject);
                foreach (Transform element in KickedOutItems)
                {
                    //Debug.Log(element);
                }
                delayCoroutine = StartCoroutine(WaitDelay());
                TakeFromCounter();
                newObject.GetComponent<OreCounter>().OreCount = 1;
                newObject.parent = contactableObjectsPool;
            }
            
            
        }
        
    }

    public void KickOutItem(int objectId, Vector3 position, Quaternion rotation, int count)
    {
        Debug.Log(transform);


        if (objectManager.TakeObject(objectId) != null)
        {
            float xTorque = (float)random.Next(-2, 2);
            float yTorque = (float)random.Next(-2, 2);
            float zTorque = (float)random.Next(-2, 2);
            Transform objectReference = objectManager.TakeObject(objectId).transform;

            var newObject = Instantiate(objectReference, position, rotation);
            objectsConnector.SubscribeOnOre(newObject);
            newObject.gameObject.AddComponent<Rigidbody>();
            newObject.gameObject.AddComponent<Rotate>();
            Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = 1;
            
            newObject.gameObject.AddComponent<DefractorResource>();
            newObject.gameObject.GetComponent<DefractorResource>().ID = objectId;
            newObject.gameObject.GetComponent<DefractorResource>().DestroyableObjects = destroyableObjects;
            newObject.gameObject.GetComponent<DefractorResource>().objectContactedDefractor += DestroyObject;
            newObject.gameObject.GetComponent<DefractorResource>().SoundManagerScript = soundManager;
            newObject.gameObject.AddComponent<GlobalResource>();
            newObject.gameObject.GetComponent<GlobalResource>().WasCollected = true;
            newObject.gameObject.GetComponent<GlobalResource>().TargetLayerMask = midasLayerHolder.GetComponent<LayerMaskSettings>().TargetLayer;
            newObject.gameObject.GetComponent<GlobalResource>().ID = objectId;
            newObject.gameObject.GetComponent<GlobalResource>().GlobalSoundManager = soundManager;

            newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * (Mathf.Cos(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) + xAngleOffset) * xForcePower);
            newObject.gameObject.GetComponent<Rigidbody>().angularDrag = 0.75f;
            newObject.gameObject.GetComponent<Rigidbody>().drag = 0.4f;
            newObject.gameObject.GetComponent<Rigidbody>().AddTorque(100 * xTorque, 100 * yTorque, 100 * zTorque);
            if (-cameraController.YRotation > 0)
            {
                newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * yForcePower);
            }
            else { newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * -yForcePower); }

            KickedOutItems.Add(newObject);
            newObject.GetComponent<OreCounter>().OreCount = count;
            Debug.Log("count was " + count);
            newObject.parent = contactableObjectsPool;
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
            //Debug.Log(element);
            if (element == objectTransform)
            {
                objectTransform.gameObject.GetComponent<DefractorResource>().objectContactedDefractor -= DestroyObject;
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
        Debug.Log(quickAccessHandController.CurrentCustomID + " well hello there");
        if (quickAccessHandController.CurrentCustomID == 10)
        {
            return;
        }
        quickAccessHandController.CurrentCounter.GetComponent<ICounter>().GetResource(1);
    }
}
