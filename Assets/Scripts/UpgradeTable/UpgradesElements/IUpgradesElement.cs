using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradesElement
{
    public event Action<int> buttonClicked;
    public int CustomID
    {
        get;
    }
}
