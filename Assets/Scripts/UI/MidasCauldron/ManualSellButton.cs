using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualSellButton : MonoBehaviour
{
    [SerializeField] DefractorGetData defractorGetData;
    [SerializeField] Transform mainButton;
    [SerializeField] Transform fieldWithResource;
    [SerializeField] Transform fieldWithProduct;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] CellButtonType cellButtonType;
    int minimalAmmount;
    int productAmmount;

    public enum CellButtonType { midasSellButton, defractorSellButton };

    // Start is called before the first frame update
    void Start()
    {
        FirstSettings();
        mainButton.GetComponent<Button>().onClick.AddListener(DecideBehavior);
    }

    void FirstSettings()
    {
        minimalAmmount = 10;
        productAmmount = 1;
    }

    void DecideBehavior()
    {
        CaclulateMinimalAmmount();
        CaclulateProductAmmount();
        if (cellButtonType == CellButtonType.midasSellButton)
        {
            ConvertToGold();
        } else if (cellButtonType == CellButtonType.defractorSellButton)
        {
            ConvertToProduct();
        }
    }

    void CaclulateMinimalAmmount()
    {
        if (cellButtonType == CellButtonType.midasSellButton)
        {
            minimalAmmount = 10;
        } else if (cellButtonType == CellButtonType.defractorSellButton)
        {
            minimalAmmount = defractorGetData.GetResourceMinimalAmmount(fieldWithResource.GetComponent<IBasicElement>().CustomID);
        }
    }

    void CaclulateProductAmmount()
    {
        if (cellButtonType == CellButtonType.midasSellButton)
        {
            productAmmount = 1;
        }
        else if (cellButtonType == CellButtonType.defractorSellButton)
        {
            productAmmount = defractorGetData.GetProductValue(fieldWithProduct.GetComponent<DefractorProductElement>().CustomID);
        }
    }

    void ConvertToGold()
    {
        if (fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter != null && 
            fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            goldCoinsCounter.AddResource(fieldWithResource.GetComponent<SellElementMidasCauldron>().GetPrice());
            fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter.GetComponent<ICounter>().GetResource(minimalAmmount);
        }
    }

    void ConvertToProduct()
    {
        if (fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter != null &&
            fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            Debug.Log(fieldWithProduct.GetComponent<DefractorProductElement>().AttachedCounter);
            Debug.Log(fieldWithProduct.GetComponent<DefractorProductElement>().AttachedCounter.GetComponent<ICounter>().Count);
            fieldWithProduct.GetComponent<DefractorProductElement>().AttachedCounter.GetComponent<ICounter>().AddResource(productAmmount);
            fieldWithResource.GetComponent<SellElementMidasCauldron>().AttachedCounter.GetComponent<ICounter>().GetResource(minimalAmmount);
        }
    }
}
