using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PotentialProductAppearance : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] ClickManager clickManager;
    [SerializeField] AmuletsTransmutation amuletsTransmutation;
    [SerializeField] TransmutationCostTaker transmutationCostTaker;
    [SerializeField] PotentialProductVisualisation potentialProductVisualisation;
    [SerializeField] Transform productsHolder;
    [SerializeField] Transform amuletsHolder;
    [SerializeField] PotentialProductLibrary potentialProductLibrary;
    [SerializeField] Transform resourcePacksHolder;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;

    [SerializeField] ParticleSystem productParticleSystem;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;

    [SerializeField] EClickVariations eClickVariations;
    [SerializeField] Transform instantiatedProductHolder;
    [SerializeField] private FireElementalBehavior m_fireElementalBehavior;
    Transform createdObject;
    bool isCreated = false;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource conjurationAppearSound;
    AudioSource throughPortalSound;

    public Transform InstantiatedProductHolder { get { return instantiatedProductHolder; } }
    public Transform CreatedObject { get { return createdObject; } }

    public event Action<int> ObjectProduced = delegate { };
    public bool IsCreated
    {
        get { return isCreated; } set { isCreated = value; }
    }
    public event Action<Transform> AmuletRequestedReset = delegate { };
    public event Action<Transform> StartedAutomaticTransmutation = delegate { };
    public event Action NoResourcesLeft = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        clickManager.EClicked += InstantiateProduct;
        amuletsTransmutation.AutomaticTransmutationContinue += InstantiateProduct;
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", productParticleSystem.transform);

        foreach (Transform amulet in amuletsHolder)
        {
            amulet.GetComponent<TransmutationAmulet>().AmuletChoosen += InstantiateProduct;
            amulet.GetComponent<TransmutationAmulet>().StopedAutomaticTransmutation += InstantiateProduct;
        }
    }

    public event Action ObjectCreated = delegate { };
    public event Action ObjectTeleported = delegate { };
    void InstantiateProduct()
    {
        ActivateProductPS();
        if (eClickVariations.IsTransmutating && !isCreated && potentialProductVisualisation.CurrentProductID != 0)
        {
            foreach (Transform element in productsHolder)
            {
                if (element.GetComponent<TransmutationProduct>().ID == potentialProductVisualisation.CurrentProductID && transmutationCostTaker.CheckCost(potentialProductVisualisation.CurrentProductID))
                {
                    isCreated = true;
                    ObjectCreated();
                    //Debug.Log("Creating object with id3 :" + element.GetComponent<TransmutationProduct>().ID);
                    itemsCounterQuests.countCreatedQuest(element.GetComponent<TransmutationProduct>().ID);
                    createdObject = Instantiate(element, element.position, element.rotation);
                    throughPortalSound = soundManager.LocateAudioSource("GoingThroughPortal", createdObject);
                    StartCoroutine(PortalSoundDelay());
                    createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                    createdObject.GetComponent<MeshRenderer>().enabled = true;
                    createdObject.parent = instantiatedProductHolder;
                    //createdObject.GetComponent<Rigidbody>().useGravity = true;
                    m_fireElementalBehavior.ShowConjuringAnimation();
                    StartCoroutine(MaterializeProduct(createdObject, 1f));
                    break;
                }
            }
        }
    }

    public void InstantiateProduct(int uploadedID, Vector3 position)
    {
        ActivateProductPS();
        foreach (Transform element in productsHolder)
        {
            if (element.GetComponent<TransmutationProduct>().ID == uploadedID)
            {
                createdObject = Instantiate(element, position, element.rotation);
                createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                createdObject.GetComponent<MeshRenderer>().enabled = true;
                createdObject.parent = instantiatedProductHolder;
                StartCoroutine(MaterializeProduct(createdObject, 1f));
                break;
            }
        }
    }

    void InstantiateProduct(Transform amulet)
    {
        ActivateProductPS();
        if (amulet != null)
        {
            if (amulet.GetComponent<TransmutationAmulet>().ID != 0 && !isCreated)
            {
                foreach (Transform element in productsHolder)
                {
                    
                    
                    if (element.GetComponent<TransmutationProduct>().ID == amulet.GetComponent<TransmutationAmulet>().ID)
                    {
                        if (transmutationCostTaker.CheckCost(element.GetComponent<TransmutationProduct>().ID))
                        {
                            isCreated = true;
                            ObjectCreated();
                            Debug.Log("Creating object with id1 :" + element.GetComponent<TransmutationProduct>().ID);
                            createdObject = Instantiate(element, element.position, element.rotation);
                            throughPortalSound = soundManager.LocateAudioSource("GoingThroughPortal", createdObject);
                            StartCoroutine(PortalSoundDelay());
                            itemsCounterQuests.countCreatedQuest(element.GetComponent<TransmutationProduct>().ID);
                            createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                            createdObject.GetComponent<MeshRenderer>().enabled = true;
                            createdObject.parent = instantiatedProductHolder;
                            //createdObject.GetComponent<Rigidbody>().useGravity = true;
                            foreach (Transform resourcePack in resourcePacksHolder)
                            {
                                AmuletRequestedReset(resourcePack.GetChild(1).transform);
                            }
                            amulet.GetComponent<TransmutationAmulet>().ShowAmuletProduct();
                            StartedAutomaticTransmutation(amulet);
                            StartCoroutine(MaterializeProduct(createdObject, 1f));
                            /*
                            foreach (var potentialProduct in potentialProductLibrary.PotentialProducts)
                            {
                                if (!Enumerable.SequenceEqual(potentialProduct.Value.OrderBy(e => e), potentialProductVisualisation.ResourcesIDs.OrderBy(e => e)))
                                {

                                }
                            }
                            */
                            break;
                        } else { if (NoResourcesLeft != null) { NoResourcesLeft(); } }
                        
                    }
                }
            }
            else if (amulet.GetComponent<TransmutationAmulet>().ID == 0 && potentialProductVisualisation.CurrentProductID != 26 && !isCreated && potentialProductVisualisation.CurrentProductID != 0)
                { 
                foreach (Transform element in productsHolder)
                {
                    if (element.GetComponent<TransmutationProduct>().ID == potentialProductVisualisation.CurrentProductID)
                    {
                        if (transmutationCostTaker.CheckCost(element.GetComponent<TransmutationProduct>().ID))
                        {
                            isCreated = true;
                            ObjectCreated();
                            Debug.Log("Creating object with id2 " + element.GetComponent<TransmutationProduct>().ID);
                            createdObject = Instantiate(element, element.position, element.rotation);
                            throughPortalSound = soundManager.LocateAudioSource("GoingThroughPortal", createdObject);
                            StartCoroutine(PortalSoundDelay());
                            itemsCounterQuests.countCreatedQuest(element.GetComponent<TransmutationProduct>().ID);
                            createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                            createdObject.GetComponent<MeshRenderer>().enabled = true;
                            createdObject.parent = instantiatedProductHolder;
                            //createdObject.GetComponent<Rigidbody>().useGravity = true;
                            amulet.GetComponent<TransmutationAmulet>().ID = element.GetComponent<TransmutationProduct>().ID;
                            StartCoroutine(MaterializeProduct(createdObject, 1f));
                            break;
                        } else { if (NoResourcesLeft != null) { NoResourcesLeft(); } }
                        
                    }
                }
            }
        }
        else
        {
            StartedAutomaticTransmutation(null);
        }
        
    }

    IEnumerator PortalSoundDelay()
    {
        yield return new WaitForSeconds(1);
        throughPortalSound.Play();
    }

    IEnumerator MaterializeProduct(Transform productTransform, float duration)
    {
        float elapsed = 0;
        //Debug.Log("hello");
        MeshRenderer productMeshrenderer = productTransform.GetComponent<MeshRenderer>();
        //Debug.Log("hello");
        Material productMaterial = productMeshrenderer.material;
        //Debug.Log("hello");
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(1, 0, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            //Debug.Log(currentMaterialization);
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0);
        productMeshrenderer.material = productMaterial;
        productTransform.GetComponent<Rigidbody>().useGravity = true;
        yield return null;
    }

    void DestroyObject()
    {
        Debug.Log(1);
        DeactivateProductPS();
        if (ObjectProduced != null && createdObject.GetComponent<TransmutationProduct>().ID != 26) { 
            Debug.Log(1); 
            ObjectProduced(createdObject.GetComponent<TransmutationProduct>().ID); 
            Debug.Log(1); }
        Debug.Log(1);
        Destroy(createdObject.gameObject);
        Debug.Log(1);
        ObjectTeleported();
        StartCoroutine(WaitDelay());
    }

    public void ReleaseProduct()
    {
        StopAllCoroutines();
        
        if (InstantiatedProductHolder.childCount > 0)
        {
            DestroyImmediate(InstantiatedProductHolder.GetChild(0).gameObject);
        }
        
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isCreated = false;
    }

    void ActivateProductPS()
    {
        appearanceTransmutationCircle.CircleAppearance();
        //productParticleSystem.gameObject.SetActive(true);
        //productParticleSystem.Play();
        conjurationAppearSound.Play();
    }

    void DeactivateProductPS()
    {
        appearanceTransmutationCircle.CircleDisappearance();
        //Debug.Log(1);
        //productParticleSystem.Stop();
        //Debug.Log(1);
        //productParticleSystem.gameObject.SetActive(false);
        //Debug.Log(1);
    }
}
