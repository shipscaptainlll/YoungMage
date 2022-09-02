using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonObjectPositions : MonoBehaviour
{
    [Header("Skeleton points positions")]
    [SerializeField] Transform stoneHandsPosition;
    [SerializeField] Transform glovesPosition;
    [SerializeField] Transform leggingsPosition;
    [SerializeField] Transform plateArmorPosition;
    [SerializeField] Transform shoesPosition;
    [SerializeField] Transform helmPosition;
    [SerializeField] Transform bracersPosition;

    [SerializeField] Vector3 stoneHandsRotation;
    [SerializeField] Vector3 glovesRotation;
    [SerializeField] Vector3 leggingsRotation;
    [SerializeField] Vector3 plateArmorRotation;
    [SerializeField] Vector3 shoesRotation;
    [SerializeField] Vector3 helmRotation;
    [SerializeField] Vector3 bracersRotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public Transform GetObjectPosition(int id)
    {
        switch (id)
        {
            case 11:
                return stoneHandsPosition;
            case 16:
                return glovesPosition;
            case 12:
                return leggingsPosition;
            case 13:
                return plateArmorPosition;
            case 14:
                return shoesPosition;
            case 15:
                return helmPosition;
            case 17:
                return bracersPosition;
        }
        return null;
    }

    public Vector3 GetObjectRotation(int id)
    {
        switch (id)
        {
            case 11:
                return stoneHandsRotation;
            case 16:
                return glovesRotation;
            case 12:
                return leggingsRotation;
            case 13:
                return plateArmorRotation;
            case 14:
                return shoesRotation;
            case 15:
                return helmRotation;
            case 17:
                return bracersRotation;
        }
        return new Vector3(0, 0, 0);
    }
}
