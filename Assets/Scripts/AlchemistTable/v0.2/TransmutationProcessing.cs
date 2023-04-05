using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationProcessing : MonoBehaviour
{
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    [SerializeField] private TransmutationErrorsNotificator m_transmutationErrorsNotificator;
    [SerializeField] private TransmutationDesintegrationModeActivator m_transmutationDesintegrationModeActivator;
    [SerializeField] private TransmutationDesintegrationMode m_transmutationDesintegrationMode;
    [SerializeField] private TransmutationProductCreation m_transmutationProductCreation;
    private bool m_isProcessing;
    
    public bool IsProcessing { get => m_isProcessing; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ActivateProcessing()
    {
        m_isProcessing = true;
        Debug.Log("activated");
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
        m_isProcessing = false;
        Debug.Log("deactivated");
        //m_transmutationDesintegrationModeActivator.ActivateDesintegrationMode();
        m_transmutationElementsManager.ResetElementsList();
        m_transmutationDesintegrationModeActivator.DeactivateDesintegrationMode();
    }

    public void ActivateProductProduction()
    {
        //some business logic
        m_transmutationProductCreation.StartProcessing();
    }
}
