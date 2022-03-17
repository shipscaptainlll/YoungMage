using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacketFollowerVector : MonoBehaviour
{
    [SerializeField] SacketMagnetDatabase sacketMagnetDatabase;
    [SerializeField] Transform sacketPosition;
    public Transform objectHello;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHello != null)
        {
            objectHello.GetComponent<Rigidbody>().useGravity = false;
            //objectHello.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0) ;
            Vector3 difference = objectHello.transform.position - sacketPosition.position;
            difference.Normalize();
            difference = -difference * Time.deltaTime * 250;
            Debug.Log(difference);
            objectHello.GetComponent<Rigidbody>().velocity = difference;
        }
    }
}
