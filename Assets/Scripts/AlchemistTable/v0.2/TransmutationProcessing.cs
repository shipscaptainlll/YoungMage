using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationProcessing : MonoBehaviour
{
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    [SerializeField] private TransmutationErrorsNotificator m_transmutationErrorsNotificator;
    [SerializeField] private TransmutationDesintegrationModeActivator m_transmutationDesintegrationModeActivator;
    [SerializeField] private TransmutationDesintegrationMode m_transmutationDesintegrationMode;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateProcessing()
    {
        if (m_transmutationElementsManager.ElementsFilled == 0)
        {
            m_transmutationErrorsNotificator.ActivatePopup();
        }
        else
        {
            m_transmutationDesintegrationModeActivator.ActivateDesintegrationMode();
            m_transmutationDesintegrationMode.EnterDesintegration();
        }
    }

    public void DeactivateProcessing()
    {
        m_transmutationDesintegrationModeActivator.ActivateDesintegrationMode();
        m_transmutationElementsManager.ResetElementsList();
        //m_transmutationDesintegrationMode.ExitDesintegradion();
    }

    public void ActivateProductProduction()
    {
        
    }
}
