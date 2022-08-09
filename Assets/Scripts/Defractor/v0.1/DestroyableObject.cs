using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        foreach (Transform element in transform)
        {
            //element.GetComponent<ConstantForce>().force = new Vector3(0, -2500f, 0);
            element.GetComponent<BoxCollider>().isTrigger = true;
        }
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }
}
