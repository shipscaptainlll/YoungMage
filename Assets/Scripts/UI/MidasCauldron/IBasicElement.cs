using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AutomaticSaleMidasCauldron;

public interface IBasicElement
{
    public int CustomID 
    { 
        get; set; 
    }

    public Transform AttachedCounter
    {
        get;
    }

    public string TypeOfThisCell { get; }
    public bool IsEnough { get; }
}
