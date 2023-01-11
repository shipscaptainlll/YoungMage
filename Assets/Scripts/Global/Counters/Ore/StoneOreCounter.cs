using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneOreCounter : MonoBehaviour, ICounter
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] int startAmmount;
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
    public event Action StoneOreCreated = delegate { };
    public event Action<int> AmmountEnded = delegate { };
    public event Action<int, Transform> ItemCreated = delegate { };
    void Start()
    {
        count = 0;
        //clickManager.Ente
        //ed += AddManually;
    }

    public void AddManually()
    {
        count += startAmmount;
        //Debug.Log("added: " + startAmmount);
        RefreshUICounter();
        NotifyAmountChanged(count);
        controlInventoryVisibility();
    }

    public void AddResource(int ammount)
    {
        count += ammount;
        RefreshUICounter();
        if (AddedAmmount != null) { AddedAmmount(id, ammount); }
        NotifyAmountChanged(count);
        controlInventoryVisibility();
        //Debug.Log("added");
    }

    public void GetResource(int ammount)
    {
        count -= ammount;
        RefreshUICounter();
        NotifyAmountChanged(count);
        controlInventoryVisibility();
        //Debug.Log("taken " + ammount);
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
                if (AmmountEnded != null) { AmmountEnded((int)ItemsList.Items.stoneOre); }
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
            ItemCreated((int)ItemsList.Items.stoneOre, transform);
        }
        if (StoneOreCreated != null)
        {
            StoneOreCreated();
        }
    }
}
