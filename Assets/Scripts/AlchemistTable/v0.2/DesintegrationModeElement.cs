using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesintegrationModeElement : MonoBehaviour
{
    [SerializeField] private DesintegrationElementType m_desintegrationElementType;

    public enum DesintegrationElementType
    {
        outer,
        middle,
        center
    }

    public DesintegrationElementType ElementType
    {
        get => m_desintegrationElementType;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
