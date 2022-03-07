using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableResource : MonoBehaviour, IResource
{
    [SerializeField] int _id;
    
    public int ID { get { return _id; } }
}
