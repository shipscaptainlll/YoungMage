using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationDesintegrationMode : MonoBehaviour
{
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    [SerializeField] private Transform m_desintegrationPanelsHolder;
    [SerializeField] private Transform m_desintegrationPanelTemplate;
    [SerializeField] private Transform m_desintegrationPanelPosition;
    [SerializeField] private TransmutationDesintegrationModeActivator m_transmutationDesintegrationModeActivator;
    [SerializeField] private TransmutationProcessing m_transmutationProcessing;
    [SerializeField] private TransmutationHandController m_transmutationHandController;
    private int m_leftCycles;
    private int m_cyclesCount;
    
    public int LeftCycles
    {
        get => m_leftCycles;
    }

    public void EnterDesintegration()
    {
        m_leftCycles = m_transmutationElementsManager.ElementsFilled;
        Debug.Log("number of filled elements " + m_leftCycles);
        foreach (TransmutationElement element in m_transmutationElementsManager.ActivatedTransmutationElements)
        {
            Debug.Log(element.transform.name);
        }
        Debug.Log(m_transmutationElementsManager.ActivatedTransmutationElements[0].transform.name);
        m_cyclesCount = 0;
        Transform newDesintegrationPanel = Instantiate(m_desintegrationPanelTemplate, m_desintegrationPanelsHolder);
        newDesintegrationPanel.gameObject.SetActive(true);
        newDesintegrationPanel.position = m_desintegrationPanelPosition.position;
        m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles - 1].CurrentTransmutationBaseObject.ActivateShining();
        m_transmutationHandController.gameObject.SetActive(true);
        m_transmutationHandController.ShowHandTransmutation();
        //ShowNextDesintegrationPanel();
    }

    public void ShowBadDesintegration()
    {
        m_transmutationHandController.ShowImpact();
    }

    public void ShowNextDesintegraionElement()
    {
        m_transmutationHandController.ShowImpact();
        m_leftCycles--;
        if (m_leftCycles > 0)
        {
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.DeactivateShining();
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.ActivateDecomposition();
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles - 1].CurrentTransmutationBaseObject.ActivateShining();
            
        } 
        
        if (m_leftCycles == 0)
        {
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.DeactivateShining();
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.ActivateDecomposition();
            //m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.ActivateShining();
            ExitDesintegradion();
        }
    }

    public void ExitDesintegradion()
    {
        m_transmutationProcessing.ActivateProductProduction();
    }
    
}
