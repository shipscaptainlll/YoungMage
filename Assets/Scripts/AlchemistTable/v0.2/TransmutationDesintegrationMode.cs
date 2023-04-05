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
        //ShowNextDesintegrationPanel();
    }

    public void ShowNextDesintegraionElement()
    {
        m_leftCycles--;
        if (m_leftCycles > 0)
        {
            m_transmutationElementsManager.ActivatedTransmutationElements[m_leftCycles].CurrentTransmutationBaseObject.ActivateDecomposition();
            
        } 
        
        if (m_leftCycles == 0)
        {
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
