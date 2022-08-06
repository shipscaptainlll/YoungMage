using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IMachinery
{
    public string Type { get; }
    public string MachineryName { get; }
    public Sprite ResourceImage { get; }
    public Sprite ProductImage { get; }
    public string Description { get; }
}
