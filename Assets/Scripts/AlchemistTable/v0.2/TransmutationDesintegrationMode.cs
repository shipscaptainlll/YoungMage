using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationDesintegrationMode : MonoBehaviour
{
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    [SerializeField] private Transform m_desintegrationPanelsHolder;
    [SerializeField] private Transform m_desintegrationPanelTemplate;
    [SerializeField] private TransmutationDesintegrationModeActivator m_transmutationDesintegrationModeActivator;
    [SerializeField] private TransmutationProcessing m_transmutationProcessing;
    private int m_leftCycles;
    private int m_cyclesCount;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnterDesintegration()
    {
        m_leftCycles = m_transmutationElementsManager.ElementsFilled;
        m_cyclesCount = 0;
        ShowNextDesintegrationPanel();
    }

    public void ShowNextDesintegrationPanel()
    {
        if (m_cyclesCount - 1 > 0)
        {
            m_transmutationElementsManager.ActivatedTransmutationElements[m_cyclesCount - 1].CurrentTransmutationBaseObject.ActivateDecomposition();
        } 
        
        if (m_leftCycles == 0)
        {
            m_transmutationElementsManager.ActivatedTransmutationElements[m_cyclesCount].CurrentTransmutationBaseObject.ActivateShining();
            ExitDesintegradion();
        }
        else
        {
            Transform newDesintegrationPanel = Instantiate(m_desintegrationPanelTemplate, m_desintegrationPanelsHolder);
        }
    }

    public void ExitDesintegradion()
    {
        m_transmutationProcessing.ActivateProductProduction();
    }
    
}
