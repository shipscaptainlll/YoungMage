using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationBaseObject : MonoBehaviour
{
    [SerializeField] private int m_baseObjectID;
    
    public int BaseObjectID { get {return m_baseObjectID;} }
}
