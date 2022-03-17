using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float xAxis;
    [SerializeField] float yAxis;
    [SerializeField] float zAxis;

    public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }
    public float XAxis { get { return xAxis; } set { xAxis = value; } }
    public float YAxis { get { return yAxis; } set { yAxis = value; } }
    public float ZAxis { get { return zAxis; } set { zAxis = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRotation()
    {
        StartCoroutine(RotateCoroutine());
    }

    public void StopRotation()
    {
        StopAllCoroutines();
    }

    IEnumerator RotateCoroutine()
    {
        while (true)
        {
            //transform.Rotate(xAxis * rotationSpeed * Time.deltaTime, yAxis * rotationSpeed * Time.deltaTime, zAxis * rotationSpeed * Time.deltaTime);
            transform.Rotate(xAxis * rotationSpeed * Time.deltaTime, yAxis * rotationSpeed * Time.deltaTime, zAxis * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
