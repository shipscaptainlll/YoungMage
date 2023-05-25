using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal2 : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] PortalOpener portalOpener;
    [Header("Main Settings")]
    public Portal2 linkedPortal;
    public string portalType;
    public MeshRenderer screen;
    public int recursionLimit = 5;
    [SerializeField] LayerMask currentObjectLayerMask;

    [Header("Advanced Settings")]
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    // Private variables
    RenderTexture viewTexture;
    Camera portalCam;
    Camera playerCam;
    Transform linkedCamera;
    Material firstRecursionMat;
    List<PortalTraveller> trackedTravellers;
    MeshFilter screenMeshFilter;
    bool searchStarted = false;

    public event Action SkeletonWasFound = delegate { };

    private void Awake()
    {
        if (portalType != "main") {
            playerCam = Camera.main;
            portalCam = GetComponentInChildren<Camera>();
            portalCam.enabled = false;

        }
        if (portalType == "main")
        {
            linkedCamera = linkedPortal.transform.Find("Camera");

        }
        
        trackedTravellers = new List<PortalTraveller>();
        
        Debug.Log(transform);
        screenMeshFilter = screen.GetComponent<MeshFilter>();
        contactManager.TeleporterDetected += ActivateSearch;
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    private void LateUpdate()
    {
        if (portalOpener.PortalOpened && portalType != "main") { Render(); }
        
    }

    
    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            // Render the view from the portal camera to the view texture
            portalCam.targetTexture = viewTexture;
            // Display the view texture on the screen of the linked portal
            linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        }
    }

    // Called just before player camera is rendered
    public void Render()
    {
        screen.enabled = false;
        CreateViewTexture();

        // Make portal cam posiion and rotation the same relative to this portal as player cam relative to linked portal
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        // Render the camera
        portalCam.Render();
        linkedPortal.screen.material.SetInt("displayMask", 1);
        screen.enabled = true;
    }

    void ActivateSearch(Transform usedPortal)
    {
        if (portalOpener.PortalIsClosing)
        {
            Debug.Log("sorry, portal is already closing");
        }
        if (usedPortal == transform && portalType == "main" && !portalOpener.PortalIsClosing)
        {
            Debug.Log("Search was activated ");
            SearchSurroundings();
        }
    }

    void SearchSurroundings()
    {
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(linkedCamera.position, 1f, linkedCamera.TransformDirection(Vector3.forward * 3), 6f);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.parent.GetComponent<Skeleton>() != null)
                {
                    Debug.Log(hit[i].transform.parent);
                    hit[i].transform.parent.GetComponent<SkeletonBehavior>().StartChazingPortal(linkedCamera);
                    if (SkeletonWasFound != null)
                    {
                        SkeletonWasFound();
                    }
                }
            }
            
        }
        
    }
}
