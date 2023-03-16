using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessHandController : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform sacketModel;
    [SerializeField] Transform magicwandModel;
    [SerializeField] Animator sacketEnter;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] Transform inventoryQuickAccessPanel;
    [SerializeField] Transform hand;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform countersHolder;
    [SerializeField] Transform skeletonItemsHolder;
    [SerializeField] Transform oreProductsHolder;

    [Header("Sounds  Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource changingElementSound;
    Transform currentCounter;
    GameObject objectInHand;
    int currentCustomID;
    int? currentSlot;

    Coroutine materializationCoroutine;
    public GameObject ObjectInHand
    {
        get
        {
            return objectInHand;
        }
    }

    public int CurrentCustomID
    {
        get
        {
            return currentCustomID;
        }
        set 
        {
            currentCustomID = value;
            if(ObjectHandsChanged != null) { ObjectHandsChanged(); } 
        }
    }

    public int? CurrentSlot
    {
        get
        {
            return currentSlot;
        }
    }

    public Transform CurrentCounter { get { return currentCounter; } }

    public event Action ObjectHandsChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        changingElementSound = soundManager.FindSound("ChanginHandElement");
        subscribeOnSlots();
        currentSlot = 1;
        objectInHand = null;
        clickManager.OneClicked += pickUpFromInventory;
        clickManager.TwoClicked += pickUpFromInventory;
        clickManager.ThreeClicked += pickUpFromInventory;
        clickManager.FourClicked += pickUpFromInventory;
        clickManager.FiveClicked += pickUpFromInventory;
        clickManager.SixClicked += pickUpFromInventory;
        clickManager.SevenClicked += pickUpFromInventory;
        clickManager.EightClicked += pickUpFromInventory;
        clickManager.NineClicked += pickUpFromInventory;
        RefreshCurrentSlot();
        
    }

    public void pickUpFromInventory(int slotNumber)
    {
        if (objectInHand != null)
        {
            if (currentCustomID == 10)
            {
                objectInHand.GetComponent<LassoInvoker>().Unsubscribe();
                Destroy(objectInHand);
            } else { Destroy(objectInHand); }
            
        }
        int targetCustomID = quickAccessPanel.GetChild(slotNumber - 1).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID;
        if (targetCustomID != currentCustomID || targetCustomID == 0) { changingElementSound.Play(); }
        if (targetCustomID != 0 && targetCustomID != 10)
        {
            magicwandModel.gameObject.SetActive(false);
            sacketModel.gameObject.SetActive(true);
            GameObject objectToTake = objectManager.TakeObject(targetCustomID);
            objectInHand = Instantiate(objectToTake, hand.position, hand.rotation);
            objectInHand.gameObject.layer = 30;
            objectInHand.transform.gameObject.AddComponent<HandResource>();
            SetLayerRecursively(objectInHand.transform, 11);
            DestroyImmediate(objectInHand.transform.GetComponent<SphereCollider>());
            Destroy(objectInHand.transform.GetComponent<ConnectableResource>());
            if (objectInHand.transform.GetComponent<SphereCollider>() != null) { objectInHand.transform.GetComponent<SphereCollider>().isTrigger = true; }
            
            MaterializeEffect(objectInHand.transform, 0.65f);
            SetObjectMovement(objectInHand);
            //sacketEnter.Play("GetSacketAnimation");
            objectInHand.transform.parent = hand;
            objectInHand.transform.localScale = new Vector3(objectToTake.transform.localScale.x, objectToTake.transform.localScale.y, objectToTake.transform.localScale.z);
            if (targetCustomID == 10)
            {
                objectInHand.transform.parent = hand.parent;
            }
        }
        else if (targetCustomID == 10)
        {
            magicwandModel.gameObject.SetActive(true);
            sacketModel.gameObject.SetActive(false);
        }
        MarkUsedSlot(slotNumber);
        SetCurrentCustomID(slotNumber);
        currentSlot = slotNumber;
        if (targetCustomID == 0)
        {
            objectInHand = null;
        }
        FindCurrentCounter();
        
    }

    void MarkUsedSlot(int slotNumber)
    {
        if (currentSlot != null)
        {
            UnMarkUsedSlot((int)currentSlot);
        }
        quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(1, 1, 1);
        inventoryQuickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(1, 1, 1);
    }

    void UnMarkUsedSlot(int slotNumber)
    {
        Color quickAccessColor = quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color;
        quickAccessColor = new Color(0.6792453f, 0.4657796f, 0.2210751f);
        quickAccessColor.a = 0.7294118f;
        quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = quickAccessColor;
        inventoryQuickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = quickAccessColor;
    }

    void SetCurrentCustomID(int slotNumber)
    {
        CurrentCustomID = quickAccessPanel.GetChild(slotNumber - 1).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID;
    }

    void RefreshCurrentSlot()
    {
        pickUpFromInventory((int)currentSlot);
    }

    void subscribeOnSlots()
    {
        foreach (Transform slot in quickAccessPanel)
        {
            var slotScript = slot.Find("Borders").Find("Element").GetComponent<QuickAccessElement>();
            slotScript.SlotWasUpdated += RefreshCurrentSlot;
        }
    }

    void SetObjectMovement(GameObject targetObject)
    {
        targetObject.AddComponent<Rotate>();
        Rotate rotator = targetObject.GetComponent<Rotate>(); rotator.RotationSpeed = 150; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = -1;
        rotator.StartRotation();
    }

    public void MakeObjectInvisible()
    {
        if (objectInHand != null)
        {
            objectInHand.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void MakeObjectVisible()
    {
        if (objectInHand != null)
        {
            objectInHand.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void FindCurrentCounter()
    {
        foreach (Transform element in countersHolder)
        {
            if (element.GetComponent<ICounter>().ID == currentCustomID)
            {
                currentCounter = element;
                return;
            }
        }
        foreach (Transform element in skeletonItemsHolder)
        {
            if (element.GetComponent<ICounter>().ID == currentCustomID)
            {
                currentCounter = element;
                return;
            }
        }
        foreach (Transform element in oreProductsHolder)
        {
            if (element.GetComponent<ICounter>().ID == currentCustomID)
            {
                currentCounter = element;
                return;
            }
        }
    }

    void MaterializeEffect(Transform materializedTransform, float duration)
    {
        if (materializationCoroutine != null) { StopCoroutine(materializationCoroutine); }
        materializationCoroutine = StartCoroutine(MaterializeItem(materializedTransform, duration));
    }

    IEnumerator MaterializeItem(Transform materializedTransform, float duration)
    {
        float elapsed = 0;
        MeshRenderer productMeshrenderer = materializedTransform.GetChild(0).GetComponent<MeshRenderer>();
        MeshRenderer secondProductMeshrenderer = materializedTransform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        Material productMaterial = productMeshrenderer.material;
        Material secondProductMaterial = secondProductMeshrenderer.material;

        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(1f, 0.00f, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            secondProductMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            secondProductMeshrenderer.material = secondProductMaterial;
            //Debug.Log(materializedTransform.GetChild(0).GetComponent<MeshRenderer>().material.GetFloat("_Clip"));
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0f);
        secondProductMaterial.SetFloat("_Clip", 0f);
        productMeshrenderer.material = productMaterial;
        secondProductMeshrenderer.material = secondProductMaterial;
        //Destroy(productTransform.gameObject);
        yield return null;
    }

    void SetLayerRecursively(Transform objectTransform, int layerNumber)
    {
        objectTransform.gameObject.layer = layerNumber;

        foreach (Transform child in objectTransform)
        {
            SetLayerRecursively(child, layerNumber);
        }
    }
}
