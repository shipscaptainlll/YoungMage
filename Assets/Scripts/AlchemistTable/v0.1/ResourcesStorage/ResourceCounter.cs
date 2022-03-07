using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCounter : MonoBehaviour
{
    [SerializeField] int _id;
    int _count;
    bool _isActive;

    public event Action<ResourceCounter> CounterActivated = delegate { };
    public event Action<ResourceCounter> CounterDeactivated = delegate { };
    public int ID
    { get { return _id; } }

    public int Count
    { get { return _count; }  }

    public bool IsActive
    { get { return _isActive; } }


    public void AddCount(int addAmmount)
    {
        _count += addAmmount;
        if (!_isActive && _count>0 )
        {
            if (CounterActivated != null)
            {
                CounterActivated(this);
            }
        }
    }

    public void ReduceCount(int addAmmount)
    {
        _count -= addAmmount;
        if (_isActive && _count <= 0)
        {
            if (CounterDeactivated != null)
            {
                CounterDeactivated(this);
            }
        }
    }

}
