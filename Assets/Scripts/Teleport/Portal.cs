using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] LayerMask currentObjectLayerMask;
    public Portal Other;
    public Camera PortalView;
    public Camera PortalViewForward;
    RaycastHit hit = new RaycastHit();

    private void Start()
    {
        Other.PortalView.targetTexture = new RenderTexture(Screen.width, Screen.height, 2);
        GetComponentInChildren<MeshRenderer>().sharedMaterial.mainTexture = Other.PortalView.targetTexture;
    }

    private void Update()
    {
        // Position
        Vector3 lookerPosition = Other.transform.worldToLocalMatrix.MultiplyPoint3x4(Camera.main.transform.position);
        PortalViewForward.transform.localPosition = lookerPosition;
        Vector3 lookerPositionInverted = new Vector3(-lookerPosition.x, lookerPosition.y, -lookerPosition.z);
        PortalView.transform.localPosition = lookerPositionInverted;

        // Rotation
        Quaternion difference = transform.rotation * Quaternion.Inverse(Other.transform.rotation * Quaternion.Euler(0, 180, 0));
        PortalViewForward.transform.rotation = Camera.main.transform.rotation;
        PortalView.transform.rotation = difference * Camera.main.transform.rotation;

        // Clipping
        PortalView.nearClipPlane = lookerPositionInverted.magnitude;
        PortalViewForward.nearClipPlane = lookerPosition.magnitude;
        if (Physics.SphereCast(PortalViewForward.transform.position, 0.1f, PortalViewForward.transform.TransformDirection(Vector3.forward * 4), out hit, 100f, currentObjectLayerMask))
        {
            Debug.Log(hit.transform);
        }
    }
}
