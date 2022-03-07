using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenResource : MonoBehaviour
{
    [SerializeField] ObjectManager _objectManager;
    int id;

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            RefreshObject();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RefreshObject()
    {

    }
}
