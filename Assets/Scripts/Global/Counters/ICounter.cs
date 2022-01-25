using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICounter
{
    public int Count { get; }
    public event Action<int> AmountChanged;
    public void AddResource(int ammount);
    public void GetResource(int ammount);
}
