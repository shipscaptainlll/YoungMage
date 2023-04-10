using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

using UnityEngine.EventSystems;

public class TransmutationDesintegrationPanelBehavior : MonoBehaviour
{
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] private TransmutationDesintegrationMode m_transmutationDesintegrationMode;
    [SerializeField] private TransmutationDesintegrationNotificator m_transmutationDesintegrationNotificator;
    [SerializeField] private Transform m_centerBorders;
    [SerializeField] private Transform m_middleBorders;
    [SerializeField] private Transform m_outerBorders;
    [SerializeField] private RectTransform m_centerRect;
    [SerializeField] private RectTransform m_middleRect;
    [SerializeField] private RectTransform m_outerRect;
    [SerializeField] private Transform m_elementChecker;

    [SerializeField] private Transform m_mainCanvas;
    private bool m_processFinished;
    private System.Random m_rand;
    private RaycastHit hit = new RaycastHit();
    
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_rand = new System.Random(transform.GetHashCode() + DateTime.Now.Millisecond);
        m_transmutationDesintegrationNotificator.ActivatePopup("contacted nothing");
        m_clickManager.LMBClicked += CalculateLinePositon;
        RecalculateBordersPositions();
        
        m_Raycaster = m_mainCanvas.GetComponent<GraphicRaycaster>();
        m_EventSystem = m_mainCanvas.GetComponent<EventSystem>();
    }

    void CalculateLinePositon()
    {
        if (!m_processFinished)
        {

            
            
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = m_elementChecker.position;
        
            List<RaycastResult> results = new List<RaycastResult>();
        
            m_Raycaster.Raycast(m_PointerEventData, results);
        
            
            
            if (hit.transform != null)
            {
                Debug.Log("left" + hit.transform.name);
            }
            
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<DesintegrationModeElement>() != null)
                {
                    DesintegrationModeElement foundElement = result.gameObject.GetComponent<DesintegrationModeElement>();
                    
                    if (foundElement.ElementType == DesintegrationModeElement.DesintegrationElementType.center)
                    {
                        m_transmutationDesintegrationNotificator.ActivatePopup("Excellent!!!");
                        break;
                    } else if (foundElement.ElementType == DesintegrationModeElement.DesintegrationElementType.middle)
                    {
                        m_transmutationDesintegrationNotificator.ActivatePopup("Good!");
                        break;
                    } else if (foundElement.ElementType == DesintegrationModeElement.DesintegrationElementType.outer)
                    {
                        m_transmutationDesintegrationMode.ShowBadDesintegration();
                        m_transmutationDesintegrationNotificator.ActivatePopup("Try again!");
                        return;
                        break;
                    }
                    else
                    {
                        m_transmutationDesintegrationNotificator.ActivatePopup(" ");
                        break;
                    }
                }
                
                
            
            }
            
            RecalculateBordersPositions();
            
            m_transmutationDesintegrationMode.ShowNextDesintegraionElement();
            
            Debug.Log("curent left cycles " + m_transmutationDesintegrationMode.LeftCycles);
            
            if (m_transmutationDesintegrationMode.LeftCycles == 0)
            {
                FinishProcess();
            }
            
            
        }
    }

    void RecalculateBordersPositions()
    {
        int newMiddlePosition = m_rand.Next(-200, 201);
        int newMiddleSize = m_rand.Next(175, 276);
        
        int newCenterPosition = m_rand.Next(-45, 45);
        int newCenterSize = m_rand.Next(65, 90);
        
        //m_middleRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newMiddleSize);
        m_middleBorders.localPosition = new Vector3(newMiddlePosition, 0 ,0 );
        
        //m_centerRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newCenterSize);
        m_centerBorders.localPosition = new Vector3(newCenterPosition, 0 ,0 );
        
        m_centerRect = m_centerBorders as RectTransform;
        m_middleRect = m_middleBorders as RectTransform;
        m_outerRect = m_outerBorders as RectTransform;
    }

    void FinishProcess()
    {
        m_processFinished = true;
        m_clickManager.LMBClicked -= CalculateLinePositon;
        Destroy(gameObject);
    }
    
}
