using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectsDeleter : MonoBehaviour
{
    [SerializeField] Transform collectableObjectsHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void DestroyCollectableObjects()
    {
        foreach (Transform element in collectableObjectsHolder)
        {
            Destroy(element.gameObject);
        }
    }
}
