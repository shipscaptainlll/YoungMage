using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PotentialProductVisualisation : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] Transform potentialResourcesHolder;
    [SerializeField] Transform potentialProductsHolder;
    [SerializeField] PotentialProductLibrary potentialProductLibrary;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    List<TransmutationResourceChoose> resourcesChoosers = new List<TransmutationResourceChoose>();
    List<int> resourcesIDs = new List<int>();
    Dictionary<int, List<int>> potentialProducts = new Dictionary<int, List<int>>();
    List<int> productCombination = new List<int> { 20, 20};
    List<int> foundObjects = new List<int>();
    int currentProductID;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource visualisationSound;
    AudioSource mageThinking;

    public List<int> ResourcesIDs { get { return resourcesIDs; } }
    public int CurrentProductID
    {
        get { return currentProductID; }
    }
    public event Action potentialProductVisualised = delegate { };
    public event Action potentialProductUnvisualised = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        SubscribeChoosenResources();
        potentialProducts.Add(1, new List<int> { 1, 2, 3 });
        potentialProductAppearance.ObjectCreated += TemporarilyHide;
        potentialProductAppearance.ObjectTeleported += TemporarilyShow;
        visualisationSound = soundManager.LocateAudioSource("TransmutationPotentialProduct", transform);
        mageThinking = soundManager.LocateAudioSource("YoungMageThinking", transform);

    }

    void InititalisePotentialProducts()
    {
        
    }

    void SubscribeChoosenResources()
    {
        foreach (Transform element in potentialResourcesHolder) 
        {
            TransmutationResourceChoose resourceChooser = element.Find("ChooseResource").GetComponent<TransmutationResourceChoose>();
            resourcesChoosers.Add(resourceChooser);
            resourceChooser.ResourceChosen += VisualisePotentialProduct;
            resourceChooser.ResourceUnchosen += VisualisePotentialProduct;
        }
    }

    void VisualisePotentialProduct(Transform transform)
    {
        UpdateUsedResources();
        GroupResourcesIDs();
        ShowChoosenResources();
    }

    void UpdateUsedResources()
    {
        resourcesIDs.Clear();
        foreach (TransmutationResourceChoose resourceChooser in resourcesChoosers)
        {
            if (resourceChooser.ChosenResource != null)
            {
                resourcesIDs.Add(resourceChooser.ChosenResource.GetComponent<AlchemistTableResource>().ID);
            }
        }
    }

    void GroupResourcesIDs()
    {

        resourcesIDs.Sort();
    }

    void ShowChoosenResources()
    {
        //reset visualisation
        foreach (Transform element in potentialProductsHolder)
        {
            if (element.GetComponent<AlchemistPotentialProduct>().ID == currentProductID)
            {
                element.GetComponent<MeshRenderer>().enabled = false;
                element.GetComponent<CapsuleCollider>().enabled = false;
                potentialProductUnvisualised();
            }
        }
        currentProductID = 0;

        //show current objects stack
        //Debug.Log("currently holds");
        for (int i = 0; i < resourcesIDs.Count; i++)
        {
            //Debug.Log(resourcesIDs[i]);
        }
        //search if found in products library
        foreach (var element in potentialProductLibrary.PotentialProducts)
        {
            if (Enumerable.SequenceEqual(element.Value.OrderBy(e => e), resourcesIDs.OrderBy(e => e)))
            {
                //Debug.Log(element.Key + " found one");
                currentProductID = element.Key;
                break;
            }
        }
        //visualise potential product
        if (currentProductID != 0)
        {
            foreach (Transform element in potentialProductsHolder)
            {
                if (element.GetComponent<AlchemistPotentialProduct>().ID == currentProductID)
                {
                    visualisationSound.Play();
                    mageThinking.Play();
                    element.GetComponent<MeshRenderer>().enabled = true;
                    element.GetComponent<CapsuleCollider>().enabled = true;
                    potentialProductVisualised();
                    break;
                }
            }
        }
    }

    void TemporarilyShow()
    {
        StartCoroutine(ShowWithDelay());
    }

    void TemporarilyHide()
    {
        foreach (Transform element in potentialProductsHolder)
        {
            if (element.GetComponent<AlchemistPotentialProduct>().ID == currentProductID)
            {
                element.GetComponent<MeshRenderer>().enabled = false;
                element.GetComponent<CapsuleCollider>().enabled = false;
                break;
            }
        }
    }

    IEnumerator ShowWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (Transform element in potentialProductsHolder)
        {
            if (element.GetComponent<AlchemistPotentialProduct>().ID == currentProductID)
            {
                element.GetComponent<MeshRenderer>().enabled = true;
                element.GetComponent<CapsuleCollider>().enabled = true;
                break;
            }
        }
    }
}
