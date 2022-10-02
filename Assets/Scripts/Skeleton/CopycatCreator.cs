using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopycatCreator : MonoBehaviour
{
    [SerializeField] GameObject copycatInstance;
    [SerializeField] GameObject originPortal;
    [SerializeField] GameObject copycatPortal;

    Vector3 lastCopycatPosition;
    GameObject copycat;
    Vector3 spawnOffset;

    public event Action OriginTeleported = delegate { };
    public event Action SkeletonFinallyTeleported = delegate { }; //unsafe code refactoring potential
    public Vector3 SpawnOffset
    {
        get
        {
            return spawnOffset;
        }
    }

    public Transform CopycatPortal
    {
        get
        {
            return copycatPortal.transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("terhe");
        distanceOriginPortal();
        copycat = Instantiate(copycatInstance, copycatPortal.transform.position + spawnOffset, transform.rotation);
        copycat.GetComponent<CopycatManager>().Origin = transform;
        copycat.AddComponent<Copycat>().ConnectedInstance = transform;
        copycat.transform.Find("GameObject").Find("Icosphere.014").GetComponent<ObjectSlicer>().ObjectToTileAround = copycatPortal.transform;
        copycat.transform.Find("GameObject").Find("Icosphere.014").GetComponent<ObjectSlicer>().Offset = new Vector3(1,0,0);
        copycat.transform.Find("GameObject").Find("Icosphere.014").GetComponent<ObjectSlicer>().InvertBool = 1;
        //copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().ObjectToTileAround = copycatPortal.transform;
        //copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().Offset = new Vector3(1, 0, 0);
        //copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().InvertBool = 1;
        if (copycatPortal.transform.parent.parent.Find("CopycatCatcher") != null)
        {
            copycatPortal.transform.parent.parent.Find("CopycatCatcher").GetComponent<CopycatCatcher>().CopycatCached += destroyCopycat;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceOriginPortal();
    }

    void distanceOriginPortal()
    {
        float distanceX = transform.position.x - originPortal.transform.position.x;
        float distanceY = transform.position.y - originPortal.transform.position.y;
        //Debug.Log("original portal location" + originPortal.transform.position.x + " " + originPortal.transform.position.y);
        //Debug.Log("original location" + transform.position.x + " " + transform.position.y);
        //Debug.Log("difference" + distanceX + " " + distanceY);
        float distanceZ = transform.position.z - originPortal.transform.position.z;
        //Debug.Log(transform.position.x + " " + originPortal.transform.position.x);
        //Debug.Log(distanceX);
        //Debug.Log(spawnOffset);
        spawnOffset = new Vector3(distanceX, distanceY, distanceZ);
    }

    void destroyCopycat(Transform modelOrigin)
    {
        Debug.Log(modelOrigin);
        if (modelOrigin == transform)
        {
            copycatPortal.transform.parent.parent.Find("CopycatCatcher").GetComponent<CopycatCatcher>().CopycatCached -= destroyCopycat;
            lastCopycatPosition = copycat.transform.position;
            Debug.Log("Copycat destroyed");
            Destroy(copycat);
        }
        
        //transform.GetComponent<SkeletonBehavior>().StopActivities();
        //teleportOrigin();
        //copycatPortal.transform.parent.parent.Find("CopycatCatcher").GetComponent<CopycatCatcher>().CopycatCached -= destroyCopycat;
    }

    void teleportOrigin()
    {
        transform.position = lastCopycatPosition + new Vector3(0,0,0);

        transform.GetComponent<SkeletonBehavior>().IsTeleported = false;
        if (SkeletonFinallyTeleported != null)
        {
            SkeletonFinallyTeleported();
        }
        transform.GetChild(1).GetComponent<ObjectSlicer>().setObjectToTileAround(transform.GetChild(1).GetComponent<ObjectSlicer>().CopycatPortal);
        transform.GetChild(1).GetComponent<ObjectSlicer>().setInvert(1);
        transform.GetChild(1).GetComponent<ObjectSlicer>().setOffset(new Vector3(1, 0, 0));
        transform.GetChild(2).GetComponent<ObjectSlicer>().setObjectToTileAround(transform.GetChild(2).GetComponent<ObjectSlicer>().CopycatPortal); ;
        transform.GetChild(2).GetComponent<ObjectSlicer>().setInvert(1);
        transform.GetChild(2).GetComponent<ObjectSlicer>().setOffset(new Vector3(1, 0, 0));
        if (OriginTeleported != null)
        {
            OriginTeleported();
        }
    }
}
