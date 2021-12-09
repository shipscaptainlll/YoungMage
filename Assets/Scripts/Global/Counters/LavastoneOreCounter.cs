using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LavastoneOreCounter : MonoBehaviour
{
    [SerializeField] Text textCounter;
    int count;

    public int Count
    {
        get
        {
            return count;
        }
    }

    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddResource(int ammount)
    {
        count += ammount;
        RefreshUICounter();
    }

    public void GetResource(int ammount)
    {
        count -= ammount;
        RefreshUICounter();
    }

    void RefreshUICounter()
    {
        textCounter.text = count.ToString();
    }
}
