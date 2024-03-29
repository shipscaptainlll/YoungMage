using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursedOreCounter : MonoBehaviour, ICounter
{
    [SerializeField] ItemsList itemsList;
    [SerializeField] Text textCounter;
    [SerializeField] int id;
    int count;
    bool itemOpened;

    public int ID { get { return id; } }
    public int Count
    {
        get
        {
            return count;
        }
        set { count = value; }
    }

    public bool ItemOpened
    {
        get
        {
            return itemOpened;
        }
        set
        {
            itemOpened = value;
        }
    }

    public event Action<int> AmountChanged = delegate { };
    public event Action<int, int> AddedAmmount = delegate { };
    public event Action CursedOreCreated = delegate { };
    public event Action<int> AmmountEnded = delegate { };
    public event Action<int, Transform> ItemCreated = delegate { };
    void Start()
    {
        count = 0;
    }

    public void AddResource(int ammount)
    {
        count += ammount;
        RefreshUICounter();
        if (AddedAmmount != null) { AddedAmmount(id, ammount); }
        NotifyAmountChanged(count);
        controlInventoryVisibility();
    }

    public void GetResource(int ammount)
    {
        count -= ammount;
        RefreshUICounter();
        NotifyAmountChanged(count);
        controlInventoryVisibility();
    }

    void RefreshUICounter()
    {
        textCounter.text = count.ToString();
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
                if (AmmountEnded != null) { AmmountEnded((int)ItemsList.Items.cursedOre); }
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
            ItemCreated((int)ItemsList.Items.cursedOre, transform);
        }
        if (CursedOreCreated != null)
        {
            CursedOreCreated();
        }
    }
}
