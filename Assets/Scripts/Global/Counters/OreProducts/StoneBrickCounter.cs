using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneBrickCounter : MonoBehaviour, ICounter
{
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
    }

    public bool ItemOpened
    {
        get
        {
            return itemOpened;
        }
    }

    public event Action<int> AmountChanged = delegate { };
    public event Action<int, int> AddedAmmount = delegate { };
    public event Action<int> AmmountEnded = delegate { };
    public event Action<int, Transform> ItemCreated = delegate { };
    void Start()
    {
        count = 0;
    }

    public void AddResource(int ammount)
    {
        count += ammount;
        if (AddedAmmount != null) { AddedAmmount(id, ammount); }
        //Debug.Log("inside addd resource with ammount now " + count);
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
                //Debug.Log("visibility controller");
                itemOpened = true;
                NotifyItemCreated();
            }
        }
        else if (count <= 0)
        {
            if (itemOpened)
            {
                itemOpened = false;
                if(AmmountEnded != null) { AmmountEnded((int)ItemsList.Items.stoneBrick); }
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

            if (id == 18) { //Debug.Log((int)ItemsList.Items.stoneBrick);
            }
            ItemCreated((int)ItemsList.Items.stoneBrick, transform);
        }
    }
}
