using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationDesintegrationPanelBehavior : MonoBehaviour
{
    [SerializeField] private ClickManager m_clickManager;
    [SerializeField] private TransmutationDesintegrationMode m_transmutationDesintegrationMode;
    [SerializeField] private TransmutationDesintegrationNotificator m_transmutationDesintegrationNotificator;
    private bool m_processFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        m_clickManager.LMBClicked += CalculateLinePositon;
    }

    void CalculateLinePositon()
    {
        if (!m_processFinished)
        {
            m_transmutationDesintegrationNotificator.ActivatePopup();
            //some business logic
            if (true)
            {
                FinishProcess();
            }
        }
    }

    void FinishProcess()
    {
        m_processFinished = true;
        m_clickManager.LMBClicked -= CalculateLinePositon;
        m_transmutationDesintegrationMode.ShowNextDesintegrationPanel();
        Destroy(gameObject);
    }
    
}
