using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PotentialProductAppearance : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] AmuletsTransmutation amuletsTransmutation;
    [SerializeField] PotentialProductVisualisation potentialProductVisualisation;
    [SerializeField] Transform productsHolder;
    [SerializeField] Transform amuletsHolder;
    [SerializeField] PotentialProductLibrary potentialProductLibrary;
    [SerializeField] Transform resourcePacksHolder;

    [SerializeField] EClickVariations eClickVariations;
    Transform createdObject;
    bool isCreated = false;

    public event Action<int> ObjectProduced = delegate { };
    public bool IsCreated
    {
        get { return isCreated; } set { isCreated = value; }
    }
    public event Action<Transform> AmuletRequestedReset = delegate { };
    public event Action<Transform> StartedAutomaticTransmutation = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        clickManager.EClicked += InstantiateProduct;
        amuletsTransmutation.AutomaticTransmutationContinue += InstantiateProduct;

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
        if (eClickVariations.IsTransmutating && !isCreated && potentialProductVisualisation.CurrentProductID != 0)
        {
            foreach (Transform element in productsHolder)
            {
                if (element.GetComponent<TransmutationProduct>().ID == potentialProductVisualisation.CurrentProductID)
                {
                    isCreated = true;
                    ObjectCreated();
                    createdObject = Instantiate(element, element.position, element.rotation);
                    createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                    createdObject.GetComponent<MeshRenderer>().enabled = true;
                    createdObject.GetComponent<Rigidbody>().useGravity = true;
                    break;
                }
            }
        }
    }

    void InstantiateProduct(Transform amulet)
    {
        if (amulet != null)
        {
            if (amulet.GetComponent<TransmutationAmulet>().ID != 0 && !isCreated)
            {
                foreach (Transform element in productsHolder)
                {
                    if (element.GetComponent<TransmutationProduct>().ID == amulet.GetComponent<TransmutationAmulet>().ID)
                    {
                        isCreated = true;
                        ObjectCreated();
                        createdObject = Instantiate(element, element.position, element.rotation);
                        createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                        createdObject.GetComponent<MeshRenderer>().enabled = true;
                        createdObject.GetComponent<Rigidbody>().useGravity = true;
                        foreach (Transform resourcePack in resourcePacksHolder)
                        {
                            AmuletRequestedReset(resourcePack.GetChild(1).transform);
                        }
                        amulet.GetComponent<TransmutationAmulet>().ShowAmuletProduct();
                        StartedAutomaticTransmutation(amulet);
                        /*
                        foreach (var potentialProduct in potentialProductLibrary.PotentialProducts)
                        {
                            if (!Enumerable.SequenceEqual(potentialProduct.Value.OrderBy(e => e), potentialProductVisualisation.ResourcesIDs.OrderBy(e => e)))
                            {

                            }
                        }
                        */
                        break;
                    }
                }
            }
            else if (amulet.GetComponent<TransmutationAmulet>().ID == 0 && potentialProductVisualisation.CurrentProductID != 26 && !isCreated && potentialProductVisualisation.CurrentProductID != 0)
            {
                foreach (Transform element in productsHolder)
                {
                    if (element.GetComponent<TransmutationProduct>().ID == potentialProductVisualisation.CurrentProductID)
                    {
                        isCreated = true;
                        ObjectCreated();
                        createdObject = Instantiate(element, element.position, element.rotation);
                        createdObject.GetComponent<TransmutationProduct>().EnteredPortal += DestroyObject;
                        createdObject.GetComponent<MeshRenderer>().enabled = true;
                        createdObject.GetComponent<Rigidbody>().useGravity = true;
                        amulet.GetComponent<TransmutationAmulet>().ID = element.GetComponent<TransmutationProduct>().ID;
                        break;
                    }
                }
            }
        }
        else
        {
            StartedAutomaticTransmutation(null);
        }
        
    }

    void DestroyObject()
    {
        if (ObjectProduced != null) { ObjectProduced(createdObject.GetComponent<TransmutationProduct>().ID); }
        Destroy(createdObject.gameObject);
        ObjectTeleported();
        StartCoroutine(WaitDelay());
    }

    IEnumerator WaitDelay()
    {
        yield return new WaitForSeconds(0.3f);
        isCreated = false;
    }
}
