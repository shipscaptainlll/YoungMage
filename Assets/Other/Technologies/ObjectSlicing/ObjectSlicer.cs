using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlicer : MonoBehaviour
{
    Material material;
    [SerializeField] Transform objectToTileAround;
    [SerializeField] Transform copycatPortal;
    [SerializeField] Vector3 offset;
    [SerializeField] int invertBool;

    public Transform ObjectToTileAround
    {
        get
        {
            return objectToTileAround;
        }
        set
        {
            objectToTileAround = value;
        }
    }

    public Vector3 Offset
    {
        get
        {
            return offset;
        }
        set
        {
            offset = value;
            //material.SetVector("_SliceCentre", objectToTileAround.position + offset);
        }
    }

    public int InvertBool
    {
        get
        {
            return invertBool;
        }
        set
        {
            invertBool = value;
            //material.SetFloat("_Invert", invertBool);
        }
    }

    public Transform CopycatPortal
    {
        get
        {
            return copycatPortal;
        }
    }

    public void setOffset(Vector3 newOffset)
    {
        offset = newOffset;
        material.SetVector("_SliceCentre", objectToTileAround.position + offset);
    }

    public void setObjectToTileAround(Transform newObject)
    {
        objectToTileAround = newObject;
    }

    public void setInvert(int newInvert)
    {
        invertBool = newInvert;
        material.SetFloat("_Invert", invertBool);
    }

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SkinnedMeshRenderer>().material;
        material.SetVector("_SliceCentre", objectToTileAround.position + offset);
        material.SetFloat("_Invert", invertBool);
    }

    // Update is called once per frame
    void Update()
    {
        //material.SetVector("_SliceCentre", objectToTileAround.position + offset);
        //Debug.Log("liquid position " + transform.position);
        //Debug.Log("Poratl position " + objectToTileAround.position);
        //Debug.Log("Difference " + objectToTileAround.position.ToString() + transform.position.ToString());
    }

    
}
