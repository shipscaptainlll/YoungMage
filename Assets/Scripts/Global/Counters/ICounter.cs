using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICounter
{
    public int Count { get; set; }
    public int ID { get; }
    public bool ItemOpened { get; set; }
    
    public event Action<int> AmountChanged;
    public event Action<int, int> AddedAmmount;
    public event Action<int> AmmountEnded;
    public void AddResource(int ammount);
    public void GetResource(int ammount);
}
