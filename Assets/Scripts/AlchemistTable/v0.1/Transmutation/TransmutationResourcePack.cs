using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmutationResourcePack : MonoBehaviour, IObject, IMachinery
{
    public string type;
    public string machineryName;
    public Sprite resourceImage;
    public Sprite productImage;
    public string description;
    
    
    string _name = "alchemist table";

    public string Type { get { return type; } }
    public string MachineryName { get { return machineryName; } }
    public Sprite ResourceImage { get { return resourceImage; } }
    public Sprite ProductImage { get { return productImage; } }
    public string Description { get { return description; } }

    public string Name
    {
        get
        {
            return _name;
        }
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
