using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSUI
{
    public string ObjectName { get; }
    public Sprite ObjectImage { get; }
    public string ObjectType { get; }
    public int Count { get; }

}
