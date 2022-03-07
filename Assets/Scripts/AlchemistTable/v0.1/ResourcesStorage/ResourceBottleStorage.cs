using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceBottleStorage : MonoBehaviour
{
    [SerializeField] Transform _resourceCountersHolder;
    List<ResourceCounter> _countersList = new List<ResourceCounter>();
    List<ResourceCounter> _activeCounters = new List<ResourceCounter>();

    
    public List<ResourceCounter> ActiveCounters { get { return _activeCounters; } }

    public event Action ResourcesCountChanged = delegate { };
    public event Action InstanceInitialised = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        InitialiseCounters();
        SubscribeOnCounters();
        AddToCounters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void InitialiseCounters()
    {
        foreach (Transform counter in _resourceCountersHolder)
        {
            _countersList.Add(counter.GetComponent<ResourceCounter>());
        }
        if (InstanceInitialised != null)
        {
            InstanceInitialised();
        }
        
    }

    void SubscribeOnCounters()
    {
        foreach (ResourceCounter counter in _countersList)
        {
            counter.CounterActivated += AddListElement;
            counter.CounterDeactivated += RemoveListElement;
        }
    }

    void AddListElement(ResourceCounter counter)
    {
        if (!CounterIsRegistered(counter))
        {
            _activeCounters.Add(counter);
            if (ResourcesCountChanged!=null) { ResourcesCountChanged(); }
        }
    }

    void RemoveListElement(ResourceCounter counter)
    {
        if (CounterIsRegistered(counter))
        {
            _activeCounters.Remove(counter);
            if (ResourcesCountChanged != null) { ResourcesCountChanged(); }
        }
    }

    void AddToCounters()
    {
        foreach(ResourceCounter counter in _countersList)
        {
            counter.AddCount(1);
        }
    }

    bool CounterIsRegistered(ResourceCounter targetCounter)
    {
        if (_activeCounters != null)
        {
            foreach (ResourceCounter counter in _activeCounters)
            {
                if (counter == targetCounter)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
