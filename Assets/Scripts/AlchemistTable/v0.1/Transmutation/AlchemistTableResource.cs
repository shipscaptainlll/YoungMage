using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistTableResource : MonoBehaviour, IResource
{
    [SerializeField] int _id;
    float _appointedAngle;

    public int ID { get { return _id; } }

    public float AppointedAngle { get { return _appointedAngle; } set { _appointedAngle = value; } }

}
