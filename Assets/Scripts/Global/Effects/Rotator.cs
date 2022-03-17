using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Rotate rotate;
    // Start is called before the first frame update
    void Start()
    {
        rotate.StartRotation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
