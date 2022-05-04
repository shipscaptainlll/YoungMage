using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SingletonPattern : MonoBehaviour
{
    private SingletonPattern() { }

    private static SingletonPattern _instance;

    public static SingletonPattern GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SingletonPattern();

        }
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
