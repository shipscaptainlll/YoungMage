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
        distanceOriginPortal();
        copycat = Instantiate(copycatInstance, copycatPortal.transform.position + spawnOffset, transform.rotation);
        copycat.GetComponent<CopycatManager>().Origin = transform;
        copycat.transform.Find("MiddlePart.002").GetComponent<ObjectSlicer>().ObjectToTileAround = copycatPortal.transform;
        copycat.transform.Find("MiddlePart.002").GetComponent<ObjectSlicer>().Offset = new Vector3(1,0,0);
        copycat.transform.Find("MiddlePart.002").GetComponent<ObjectSlicer>().InvertBool = 1;
        copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().ObjectToTileAround = copycatPortal.transform;
        copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().Offset = new Vector3(1, 0, 0);
        copycat.transform.Find("OuterPart.002").GetComponent<ObjectSlicer>().InvertBool = 1;
        Debug.Log(copycatPortal.transform.parent.parent);
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
        spawnOffset = new Vector3(distanceX, distanceY, distanceZ);
    }

    void destroyCopycat()
    {
        lastCopycatPosition = copycat.transform.position;
        Destroy(copycat);
        Debug.Log(transform + " this is us" + transform.GetComponent<SkeletonBehavior>().Activity);
        transform.GetComponent<SkeletonBehavior>().StopActivities();
        Debug.Log(transform + " this is us" + transform.GetComponent<SkeletonBehavior>().Activity);
        teleportOrigin();
        copycatPortal.transform.parent.parent.Find("CopycatCatcher").GetComponent<CopycatCatcher>().CopycatCached -= destroyCopycat;
        Debug.Log("Copycat destroyed");
    }

    void teleportOrigin()
    {
        transform.position = lastCopycatPosition + new Vector3(1,1,1);
        if (OriginTeleported != null)
        {
            OriginTeleported();
        }
    }
}
