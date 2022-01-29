using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    Material material;
    [SerializeField] Transform objectToTileAround;
    [SerializeField] int invertBool;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.SetVector("_SliceCentre", objectToTileAround.position);
        material.SetFloat("_Invert", invertBool);
    }

    // Update is called once per frame
    void Update()
    {
        material.SetVector("_SliceCentre", objectToTileAround.position);
        //Debug.Log("liquid position " + transform.position);
        //Debug.Log("Poratl position " + objectToTileAround.position);
        //Debug.Log("Difference " + objectToTileAround.position.ToString() + transform.position.ToString());
    }

    
}
