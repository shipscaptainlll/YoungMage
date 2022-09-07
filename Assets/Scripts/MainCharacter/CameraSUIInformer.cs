using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSUIInformer : MonoBehaviour
{
    [SerializeField] CameraController mainCameraController;
    [SerializeField] CursorCircleAnimation cursorCircleAnimation;
    GameObject lastObservedObject;
    string observedType;

    bool isSUITurned;

    public string ObservedType { get { return observedType; } }

    public event Action StoppedSeeingAnything = delegate { };
    public event Action<GameObject> SeeingNewObject = delegate { };
    public event Action<float> DistanceChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (mainCameraController.ObservedObject.transform != null)
        {
            //Debug.Log(mainCameraController.ObservedObject.transform);
            if (observedType == null)
            {
                //Debug.Log(mainCameraController.ObservedObject.transform);
                if (mainCameraController.ObservedObject.transform.parent != null
                    && mainCameraController.ObservedObject.transform.parent.GetComponent<Skeleton>() != null) {
                    observedType = "Skeleton";
                    lastObservedObject = mainCameraController.ObservedObject.transform.gameObject;
                }
                else if (mainCameraController.ObservedObject.transform.GetComponent<IOre>() != null) {
                    observedType = "Ore";
                    lastObservedObject = mainCameraController.ObservedObject.transform.gameObject;
                }
                else if (mainCameraController.ObservedObject.transform.GetComponent<IObjectSUI>() != null) {
                    observedType = "Object";
                    lastObservedObject = mainCameraController.ObservedObject.transform.gameObject;
                }
                else if (mainCameraController.ObservedObject.transform.GetComponent<IMachinery>() != null) {
                    observedType = "Machinery";
                    lastObservedObject = mainCameraController.ObservedObject.transform.gameObject;
                }
                else if (mainCameraController.ObservedObject.transform.parent != null && mainCameraController.ObservedObject.transform.parent.GetComponent<IMachinery>() != null)
                {
                    observedType = "Machinery";
                    lastObservedObject = mainCameraController.ObservedObject.transform.parent.gameObject;
                }
                else
                {
                    if (isSUITurned)
                    {
                        TurnOffSUIElement();
                        lastObservedObject = null;
                        observedType = null;

                        Debug.Log("1WROKING"); cursorCircleAnimation.StopCircleAnimation();

                    }
                    return;
                }
            }
            if (cursorCircleAnimation.IsFinished)
            {
                if ((lastObservedObject == mainCameraController.ObservedObject.transform.gameObject
                || lastObservedObject == mainCameraController.ObservedObject.transform.parent.gameObject)
            || lastObservedObject == null)
                {
                    //Debug.Log("WROKING");
                    cursorCircleAnimation.HideCircle();
                    NotifySUIElement();

                }

                if (isSUITurned)
                {
                    if (DistanceChanged != null) { DistanceChanged(mainCameraController.ObservedObject.distance); }
                }
            } else { 
                //Debug.Log("WROKING"); 
                cursorCircleAnimation.StartCircleAnimation(); }
            
            
            if (mainCameraController.ObservedObject.transform.gameObject != lastObservedObject 
                && mainCameraController.ObservedObject.transform.parent.gameObject != lastObservedObject)
            {
                //Debug.Log(mainCameraController.ObservedObject.transform);
                //Debug.Log(mainCameraController.ObservedObject.transform.parent);
                //Debug.Log(lastObservedObject);
                TurnOffSUIElement();
                lastObservedObject = null;
                observedType = null;
                
                if (cursorCircleAnimation.IsFinished)
                {
                    //Debug.Log("Hello tdwad");
                    cursorCircleAnimation.StopCircleAnimation();
                }
            }
            
        } else { 
            if (isSUITurned)
            {
                TurnOffSUIElement();
                lastObservedObject = null;
                observedType = null;
            }

            cursorCircleAnimation.StopCircleAnimation();
        }
        
        
    }

    void NotifySUIElement()
    {
        isSUITurned = true;
        if (SeeingNewObject != null) { SeeingNewObject(lastObservedObject); }
        //Debug.Log(mainCameraController.ObservedObject.transform.gameObject);
    }

    void TurnOffSUIElement()
    {
        isSUITurned = false;
        if (StoppedSeeingAnything != null) { StoppedSeeingAnything(); }
        //Debug.Log("Seeing Nothing");
    }

    
}
