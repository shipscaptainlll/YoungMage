using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCoinsCounter : MonoBehaviour, ICounter
{
    [SerializeField] ItemsList itemsList;
    [SerializeField] int id;
    [SerializeField] OreCounter oreCounter;
    int count;
    bool itemOpened;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource coinsPaySound;

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
        get => itemOpened;
        set => itemOpened = value;
    }

    public event Action<int> AmountChanged = delegate { };
    public event Action<int, int> AddedAmmount = delegate { };
    public event Action<int> AmmountEnded = delegate { };
    public event Action<int, Transform> ItemCreated = delegate { };
    void Start()
    {
        AddResource(250);
        
        coinsPaySound = soundManager.FindSound("CoinPay");
    }
    
    public void AddResource(int ammount)
    {
        if (ammount < 0) { coinsPaySound.Play(); }
        count += ammount;
        if (AddedAmmount != null) { AddedAmmount(id, ammount); }                                                       
        NotifyAmountChanged(count);
        oreCounter.OreCount = count;
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
        } else if (count <= 0)
        {
            if (itemOpened)
            {
                itemOpened = false;
                if (AmmountEnded != null)
                {
                    AmmountEnded((int)ItemsList.Items.goldCoins);
                }
            }
        }
    }

    void NotifyAmountChanged(int count)
    {
        if (AmountChanged != null)
        {
            Debug.Log("hello there");
            AmountChanged(count);
        }
    }

    void NotifyItemCreated()
    {
        if (ItemCreated != null)
        {
            ItemCreated((int) ItemsList.Items.goldCoins, transform);
        }
    }
}
