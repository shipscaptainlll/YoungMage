using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationWorkflow : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private ContactManager m_contactManager;
    [SerializeField] private ClickManager m_clickManager;

    [Header("Other")] 
    [SerializeField] private CameraController m_cameraController;
    [SerializeField] private PersonMovement m_personMovement;
    [SerializeField] private SavePanel m_savePanel;
    [SerializeField] private PanelsManager m_panelsManager;
    [SerializeField] private Transform m_quickAccessPanel;
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField] private Transform m_cameraDestination;
    [SerializeField] private Transform m_cameraStartingPoint;

    private Coroutine m_cameraRepositioningCoroutine;

    private bool m_isTransmutationMode;
    public bool IsTransmutationMode { get { return m_isTransmutationMode; } set { m_isTransmutationMode = value; } }
    
    void Start()
    {
        m_contactManager.ContactedTransmutationWorkflow += EnterTransmutaionMode;
        m_clickManager.EscClicked += ExitTransmutationMode;
    }

    void EnterTransmutaionMode()
    {
        if (!m_isTransmutationMode)
        {
            m_isTransmutationMode = true;
            
            m_cameraController.IsTransmutationMode = true;
            m_personMovement.IsTransmutationMode = true;
            m_savePanel.IsTransmutationMode = true;
            
            m_quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
            
            CursorManager.ForceCursorEnabled();
            
            if (m_cameraRepositioningCoroutine != null) { 
                StopCoroutine(m_cameraRepositioningCoroutine);
                m_cameraRepositioningCoroutine = null;
            }
            m_cameraRepositioningCoroutine = StartCoroutine(CameraToDestination(m_cameraTransform, m_cameraDestination, 1.2f));
            
            m_panelsManager.OpenTransmutationPanel();
        }
    }

    void ExitTransmutationMode()
    {
        m_isTransmutationMode = false;
        
        m_quickAccessPanel.GetComponent<CanvasGroup>().alpha = 1;
        
        CursorManager.ForceCursorDisabled();
        
        if (m_cameraRepositioningCoroutine != null)
        {
            StopCoroutine(m_cameraRepositioningCoroutine);
            m_cameraRepositioningCoroutine = null;
        }
        m_cameraRepositioningCoroutine = StartCoroutine(CameraToDestination(m_cameraTransform, m_cameraStartingPoint, 0.6f));
    }
    
    IEnumerator CameraToDestination(Transform startPoint, Transform endPoint, float delay)
    {
        float elapsed = 0;
        float maxTime = delay;
        float xStartPosition = startPoint.position.x;
        float yStartPosition = startPoint.position.y;
        float zStartPosition = startPoint.position.z;
        float xPosition = startPoint.position.x;
        float yPosition = startPoint.position.y;
        float zPosition = startPoint.position.z;

        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            
            xPosition = Mathf.Lerp(xStartPosition, endPoint.position.x, elapsed / maxTime);
            yPosition = Mathf.Lerp(yStartPosition, endPoint.position.y, elapsed / maxTime);
            zPosition = Mathf.Lerp(zStartPosition, endPoint.position.z, elapsed / maxTime);
            m_cameraTransform.position = new Vector3(xPosition, yPosition, zPosition);
             
            yield return null;
        }
        m_cameraTransform.position = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z);
        m_cameraRepositioningCoroutine = null;
        
        yield return null;
    }
}
