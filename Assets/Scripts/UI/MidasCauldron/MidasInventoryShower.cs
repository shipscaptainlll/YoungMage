using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MidasInventoryShower : MonoBehaviour
{
    [SerializeField] Transform accessableElementsHolder;
    [SerializeField] Transform exampleElement;
    [SerializeField] Transform panelForNewElements;


    List<Transform> potentialPositions = new List<Transform>();
    List<Transform> currentlyOpenedElements = new List<Transform> ();
    // Start is called before the first frame update
    void Start()
    {
        SubscribeOnElements();
    }

    void UpdateElementsVisualization()
    {
        ClearVisibleElementsCollector();
        CountVisibleElements();
        SortList();
        ArrangeElementsPositions();
    }

    void ClearVisibleElementsCollector()
    {
        currentlyOpenedElements.Clear();
    }

    void SortList()
    {
        currentlyOpenedElements = currentlyOpenedElements.OrderBy(o => o.GetComponent<MidasInventoryElement>().CustomID).ToList();
    }

    void CountVisibleElements()
    {
        foreach (Transform element in accessableElementsHolder)
        {
            if (element.GetComponent<MidasInventoryElement>().IsVisible)
            {
                currentlyOpenedElements.Add(element);
            }
        }
    }

    void ArrangeElementsPositions()
    {
        for (int i = 0; i < currentlyOpenedElements.Count; i++)
        {
            currentlyOpenedElements[i].SetSiblingIndex(i);
        }
    }
    void SubscribeOnElements()
    {
        for (int i = 0; i < 8; i++)
        {
            accessableElementsHolder.GetChild(i).GetComponent<MidasInventoryElement>().VisibilityChanged += UpdateElementsVisualization;
        }
    }
}
