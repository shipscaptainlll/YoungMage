using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResourceMachinery
{
    public string machineryName { get; }
    public string machineryDescription { get; }
    public Sprite machineryResourceImage { get; }
    public Sprite machineryProductImage { get; }
     
}
