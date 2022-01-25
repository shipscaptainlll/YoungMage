using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutomticSellController : MonoBehaviour
{
    [SerializeField] SellElementMidasCauldron sellElementMidasCauldron;
    [SerializeField] DefractorGetData defractorGetData;
    [SerializeField] CounterManager counterManager;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] Transform automaticCellsHandler;
    [SerializeField] ClickManager clickManager;
    [SerializeField] Image cacheImage;
    [SerializeField] TypeOfCell typeOfThisCell;
    int productAmmount;
    
    Coroutine actualCoroutineCache;
    int currentCellIndex;
    int minimalAmmount;
    bool currentlyWorking;
    List<Transform> workingAutomaticCells = new List<Transform>();
    List<Transform> connectedCounters = new List<Transform>();

    public enum TypeOfCell { midasCell, midasAutoCell, defractorCell, defractorAutoCell };
    public string TypeOfThisCell { get { return typeOfThisCell.ToString(); } }
    void Start()
    {
        FirstSettings();
        SubscribeOnEvents();
        clickManager.PClicked += VisualizeNext;
    }

    void FirstSettings()
    {
        ClearCellsList();
        minimalAmmount = 10;
        productAmmount = 1;
        currentCellIndex = 0;
        currentlyWorking = false;
    }
    void UpdateMinimalAmmount()
    {
        if (typeOfThisCell == TypeOfCell.midasCell)
        {
            minimalAmmount = 10;
        }
        else if (typeOfThisCell == TypeOfCell.defractorCell || typeOfThisCell == TypeOfCell.defractorAutoCell)
        {
            int resourceCustomID = workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().CustomID;
            minimalAmmount = defractorGetData.GetResourceMinimalAmmount(resourceCustomID);
        }
    }

    void UpdateProductAmmount()
    {
        if (typeOfThisCell == TypeOfCell.midasCell)
        {
            productAmmount = 1;
        }
        else if (typeOfThisCell == TypeOfCell.defractorCell || typeOfThisCell == TypeOfCell.defractorAutoCell)
        {
            int productCustomID = defractorGetData.GetProductID(workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().CustomID);
            productAmmount = defractorGetData.GetProductValue(productCustomID);
        }
    }

    void FindUnconnectedCounters()
    {
        ClearCellsList();
        GetActiveCells();
        for (int i = 0; i < workingAutomaticCells.Count; i++)
        {
            bool found = false;
            if (connectedCounters != null && connectedCounters.Count > 0)
            {
                foreach (Transform counter in connectedCounters)
                {
                    if (counter == workingAutomaticCells[i].GetComponent<IBasicElement>().AttachedCounter)
                    {
                        found = true;
                    }
                }
            }
            
            if (!found)
            {
                connectedCounters.Add(workingAutomaticCells[i].GetComponent<IBasicElement>().AttachedCounter);
                workingAutomaticCells[i].GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().AmountChanged += CheckAllAvailability;
            }
        }
    }

    void CheckAllAvailability(int count)
    {
        if (!currentlyWorking) {
            for (int i = 0; i < workingAutomaticCells.Count; i++)
            {
                if (workingAutomaticCells[i].GetComponent<IBasicElement>().IsEnough)
                {
                    currentCellIndex = 0;
                    VisualizeNext();
                }
            }
        }
    }

    void UpdateControllSystem(Transform changedCell)
    {
        
        var changedCellCache = GetChangedCellIndex(changedCell);
        if (changedCellCache!= -1 && currentCellIndex >= changedCellCache)
        {
            ClearFillFrom(changedCellCache);
        }
        
        ClearCellsList();
        GetActiveCells();
        FindUnconnectedCounters();
        if (changedCellCache != -1 && currentCellIndex >= changedCellCache)
        {
            RestartProcessingFrom(changedCellCache);
        } else if (currentCellIndex == 0 && GetChangedCellIndex(changedCell) == 0)
        {
            VisualizeNext();
        }
    }

    void VisualizeNext()
    {
        for (int i = 0; i < workingAutomaticCells.Count; i++)
        {
            Image nextImage = GetNextImage();
            if (CheckAvailability())
            {
                VisualizeProcessing(nextImage);
                currentlyWorking = true;
                return;
            }
            else { currentCellIndex++; }
        }
        currentlyWorking = false;
        
    }

    bool CheckAvailability()
    {
        if (workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().IsEnough) { return true; } else { return false; }
    }

    Image GetNextImage()
    {
        Image currentCell;
        if (currentCellIndex <= (workingAutomaticCells.Count - 1))
        {
            currentCell = workingAutomaticCells[currentCellIndex].Find("Fill").GetComponent<Image>();
            return currentCell;
        } else
        {
            currentCellIndex = 0;
            for (int i = 0; i <= workingAutomaticCells.Count - 1; i++)
            {
                SetFillTo(workingAutomaticCells[i].Find("Fill").GetComponent<Image>(), 0);
            }
            currentCell = workingAutomaticCells[currentCellIndex].Find("Fill").GetComponent<Image>();
            return currentCell;
        }
        
        
    }

    void VisualizeProcessing(Image nextImage)
    {
        UpdateMinimalAmmount();
        UpdateProductAmmount();
        actualCoroutineCache = StartCoroutine(CacheVisualize(nextImage));
    }

    void RestartProcessingFrom(int startIndex)
    {
        foreach (Transform counter in connectedCounters)
        {
            counter.GetComponent<ICounter>().AmountChanged -= CheckAllAvailability;
        }
        FindUnconnectedCounters();
        if (actualCoroutineCache != null)
        {
            StopCoroutine(actualCoroutineCache);
        }
        currentCellIndex = startIndex;
        VisualizeNext();
    }

    IEnumerator CacheVisualize(Image targetImage)
    {
        float currenTime = 0f;
        var percent = 0;
        float timeLimit = 5f;
        while (currenTime < timeLimit)
        {
            currenTime += Time.deltaTime;
            targetImage.fillAmount = Mathf.Lerp(0, 1f, currenTime / timeLimit);
            yield return null;
        }
        targetImage.fillAmount = 1f;
        actionsInFinish();
    }

    void actionsInFinish()
    {
        ConvertCellContent();
        currentCellIndex++;
        
        VisualizeNext();
    }

    void SetFillTo(Image targetImage, float targetFillAmmount) 
    {
        targetImage.fillAmount = targetFillAmmount;
    }

    void ClearFillFrom(int startIndex)
    {
        for (int i = startIndex; i < workingAutomaticCells.Count; i++)
        {
            SetFillTo(workingAutomaticCells[i].Find("Fill").GetComponent<Image>(), 0);
        }
    }
    

    void GetActiveCells()
    {
        foreach (Transform automaticCell in automaticCellsHandler)
        {
            if (automaticCell.GetComponent<AutomaticSaleMidasCauldron>().ButtonIsActive)
            {
                workingAutomaticCells.Add(automaticCell);
            }
        }
    }

    void ClearCellsList()
    {
        workingAutomaticCells.Clear();
    }

    void UpdateCellsPosition(Transform deletedCellTransform)
    {
        int cacheChangedCellIndex = GetChangedCellIndex(deletedCellTransform);
        
        for (int i = 0; i < workingAutomaticCells.Count; i++)
        {
            if (workingAutomaticCells[i] == deletedCellTransform)
            {
                SwapCells(i);
            }
        }
            if (cacheChangedCellIndex != -1 && currentCellIndex >= cacheChangedCellIndex)
            {
                ClearFillFrom(cacheChangedCellIndex);
            }

            if (cacheChangedCellIndex != -1 && currentCellIndex >= cacheChangedCellIndex)
            {
                RestartProcessingFrom(cacheChangedCellIndex);
            }
    }

    int GetChangedCellIndex(Transform deletedCellTransform)
    {
        for (int i = 0; i < workingAutomaticCells.Count; i++)
        {
            if (workingAutomaticCells[i] == deletedCellTransform)
            {
                return i;
            }
        }
        return -1;
    }

    void SwapCells(int x)
    {
        for (int i = x + 1; i < workingAutomaticCells.Count; i++)
        {
            workingAutomaticCells[i - 1].GetComponent<AutomaticSaleMidasCauldron>().CustomID = workingAutomaticCells[i].GetComponent<AutomaticSaleMidasCauldron>().CustomID;
        }
        automaticCellsHandler.GetChild(workingAutomaticCells.Count - 1).GetComponent<AutomaticSaleMidasCauldron>().CustomID = 0; 
    }
    
    void SubscribeOnEvents()
    {
        foreach (Transform automaticCell in automaticCellsHandler)
        {
            automaticCell.GetComponent<AutomaticSaleMidasCauldron>().ButtonUpdated += UpdateControllSystem;
            automaticCell.GetComponent<AutomaticSaleMidasCauldron>().ButtonCleared += UpdateCellsPosition;
        }
    }

    void ConvertCellContent()
    {
        if (typeOfThisCell == TypeOfCell.midasAutoCell)
        {
            ConvertToGold();
        }
        else if (typeOfThisCell == TypeOfCell.defractorAutoCell)
        {
            ConvertToProduct();
        }
    }

    void ConvertToGold()
    {
        if (workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count > minimalAmmount)
        {
            workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().GetResource(minimalAmmount);
            goldCoinsCounter.AddResource(sellElementMidasCauldron.GetPrice(workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().CustomID));
        }
    }
    
    void ConvertToProduct()
    {
        if (workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count > minimalAmmount)
        {
            int productCustomID = defractorGetData.GetProductID(workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().CustomID);
            Transform productCounter = counterManager.TakeCounter(productCustomID);
            productCounter.GetComponent<ICounter>().AddResource(productAmmount);
            workingAutomaticCells[currentCellIndex].GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().GetResource(minimalAmmount); ;
        }
    }
}
