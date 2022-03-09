using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyObject : MonoBehaviour
{
    [SerializeField] float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(timeDestroy);
        Destroy(gameObject);
    }
}
