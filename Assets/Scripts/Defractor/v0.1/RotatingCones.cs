using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCones : MonoBehaviour
{
    [SerializeField] int direction;
    [SerializeField] int speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator RotateCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.033f);
            transform.Rotate(0, 0, speed * direction * Time.deltaTime);
        }
        
    }
}
