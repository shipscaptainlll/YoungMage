using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalIngotCounter : MonoBehaviour, ICounter
{
    int count;
    bool itemOpened;

    public int Count
    {
        get
        {
            return count;
        }
    }

    public bool ItemOpened
    {
        get
        {
            return itemOpened;
        }
    }

    public event Action<int> AmountChanged = delegate { };
    public event Action<int, Transform> ItemCreated = delegate { };
    void Start()
    {
        count = 0;
    }

    public void AddResource(int ammount)
    {
        count += ammount;
        NotifyAmountChanged(count);
        controlInventoryVisibility();
    }

    public void GetResource(int ammount)
    {
        count -= ammount;
        NotifyAmountChanged(count);
        controlInventoryVisibility();
    }

    void controlInventoryVisibility()
    {
        if (count > 0)
        {
            if (!itemOpened)
            {
                itemOpened = true;
                NotifyItemCreated();
            }
        }
        else if (count <= 0)
        {
            if (itemOpened)
            {
                itemOpened = false;
            }
        }
    }

    void NotifyAmountChanged(int count)
    {
        if (AmountChanged != null)
        {
            AmountChanged(count);
        }
    }

    void NotifyItemCreated()
    {
        if (ItemCreated != null)
        {
            ItemCreated((int)ItemsList.Items.metalIngot, transform);
        }
    }
}