using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationElement : MonoBehaviour
{
    [SerializeField] private int m_transmutationSlotID;
    [SerializeField] private Transform m_transmutationObjectsHolder;
    private Dictionary<int, GameObject> m_transmutationObjectsDictionary = new Dictionary<int, GameObject>();
    private int m_currentBaseObjectID;
    private TransmutationBaseObjectsBehavior m_currentTransmutationBaseObjectsBehavior;
    
    public int TransmutationSlotID { get {return m_transmutationSlotID;} }
    public int CurrentBaseObjectID { get {return m_currentBaseObjectID;} }
    public TransmutationBaseObjectsBehavior CurrentTransmutationBaseObject { get {return m_currentTransmutationBaseObjectsBehavior;} }

    // Start is called before the first frame update
    void Start()
    {
        m_currentBaseObjectID = -1;
        int indexer = 0;
        foreach (Transform element in m_transmutationObjectsHolder)
        {
            m_transmutationObjectsDictionary.Add(element.GetComponent<TransmutationBaseObject>().BaseObjectID, element.gameObject);
        }
    }

    public void HideVisibility()
    {
        Debug.Log("where are we");
        if (m_currentBaseObjectID != -1)
        {
            m_transmutationObjectsDictionary[m_currentBaseObjectID].GetComponent<TransmutationBaseObjectsBehavior>().HideObject();
            m_currentBaseObjectID = -1;
            m_currentTransmutationBaseObjectsBehavior = null;
        }
    }

    public void ShowObject(int id)
    {
        HideVisibility();
        Debug.Log("id is " + id);
        if (id != 0)
        {
            m_transmutationObjectsDictionary[id].SetActive(true);
            m_transmutationObjectsDictionary[id].GetComponent<TransmutationBaseObjectsBehavior>().ActivateFloating();
            m_currentBaseObjectID = id;
            m_currentTransmutationBaseObjectsBehavior = m_transmutationObjectsDictionary[id].GetComponent<TransmutationBaseObjectsBehavior>();
        }
        
    }
}
