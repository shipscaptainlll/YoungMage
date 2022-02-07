using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject3D : MonoBehaviour
{
    float transformationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopOutObject(Transform objectToShow)
    {

    }

    IEnumerator PopOut()
    {
        float elapsed = 0;
        float xScale;
        float yScale;
        float zScale;
        while (elapsed < transformationSpeed)
        {
            elapsed += Time.deltaTime;

            yield return null;
        }
        
    }
}
