using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmutateAmuletsCounter : MonoBehaviour, ICounter, ISkeletonItems
{
    [SerializeField] ItemsList itemsList;
    [SerializeField] int id;
    int count = 0;
    int maxCount = 7;
    bool itemOpened = false;

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

    public event Action AmuletAdded = delegate { };
    public event Action<int, int> AddedAmmount = delegate { };
    public event Action<int> AmmountEnded = delegate { };
    public event Action<int> AmountChanged = delegate { };
    public event Action ItemFirstCreated;

    void Start()
    {
        count = 0;
    }

    public void AddResource(int ammount)
    {
        if (count < maxCount)
        {
            count += ammount;
            if (AddedAmmount != null) { AddedAmmount(id, ammount); }
            if (AmuletAdded != null)
            {
                AmuletAdded();
            }
        }
    }

    public void GetResource(int ammount)
    {
        count -= ammount;
        NotifyAmountChanged(count);
    }


    void NotifyAmountChanged(int count)
    {
        if (AmountChanged != null)
        {
            AmountChanged(count);
        }
    }

}
