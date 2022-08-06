using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorMachineObject : MonoBehaviour, IMachinery
{
    public string type;
    public string machineryName;
    public Sprite resourceImage;
    public Sprite productImage;
    public string description;

    public string Type { get { return type; } }
    public string MachineryName { get { return machineryName; } }
    public Sprite ResourceImage { get { return resourceImage; } }
    public Sprite ProductImage { get { return productImage; } }
    public string Description { get { return description; } }
}
