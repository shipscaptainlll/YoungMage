using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelmProcessor : MonoBehaviour, IProcessorPanel
{
    [SerializeField] int customID;
    [SerializeField] Button createButton;
    [SerializeField] MetalOreCounter resourceCounter;
    [SerializeField] HelmCounter productCounter;

    [SerializeField] int productCost;
    [SerializeField] int productPerProcess;


    public int CustomID
    {
        get
        {
            return customID;
        }
    }

    void Start() 
    {
        createButton.onClick.AddListener(createItem);
    }

    void createItem()
    {
        if (checkSupplies())
        {
            spendResources();
            getProduct();
        }
        
    }

    bool checkSupplies()
    {
        if (resourceCounter.Count >= productCost)
        {
            return true;
        } else
        {
            return false;
        }
    }

    void spendResources()
    {
        resourceCounter.GetResource(productCost);
    }

    void getProduct()
    {
        productCounter.AddResource(productPerProcess);
    }
}
