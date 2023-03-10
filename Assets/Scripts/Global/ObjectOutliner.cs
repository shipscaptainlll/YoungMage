using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutliner : MonoBehaviour
{
    Transform currentlyViewedObject;
    Transform cacheViewedObject;

    public event Action StartedViewingObject = delegate { };
    public event Action StoppedViewingObject = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreVewedObject(Transform viewedObject)
    {
        if (currentlyViewedObject != viewedObject)
        {
            if (currentlyViewedObject != null) { currentlyViewedObject.GetComponent<IShowClickable>().Hide(); }
            //Debug.Log(currentlyViewedObject + " become");
            currentlyViewedObject = viewedObject;
            Debug.Log(currentlyViewedObject);
            if (currentlyViewedObject != null) { currentlyViewedObject.GetComponent<IShowClickable>().Show(); }
        }
    }
}
