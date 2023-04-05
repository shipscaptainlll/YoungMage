using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TransmutationElementsManager : MonoBehaviour
{
    [SerializeField] private Transform baseElementsHolder;
    [SerializeField] private Transform transmutationSlotsHolder;
    [SerializeField] private TransmutationHandAnimator m_transmutationHandAnimator;
    private List<TransmutationElement> m_activatedTransmutationElements = new List<TransmutationElement>();
    List<int> m_activatedObjectsIDs = new List<int>();
    private int m_elementsFilled;
    private Dictionary<int, TransmutationElement> m_transmutationElementsDictionary = new Dictionary<int, TransmutationElement>();
    private Dictionary<int, Element> m_transmutationSlotsDictionary = new Dictionary<int, Element>();
    private Coroutine m_currentCoroutine;
    private bool m_coroutineIsRunning;
    private TransmutationElement m_currentlyManagedElement;
    private bool m_currentlyTaking;
    private int m_currentlyManagedID;

    public int ElementsFilled { get {return m_elementsFilled;} }


    public List<int> ActivatedObjectsIDs { get {return m_activatedObjectsIDs; }}
    public List<TransmutationElement> ActivatedTransmutationElements { get { return m_activatedTransmutationElements; } }

    private void Update()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform slot in transmutationSlotsHolder)
        {
            
            slot.Find("Borders").Find("Element").GetComponent<Element>().TransmutationSlotElementFilled += UpdateElementState;

            Element transmutationSlot = slot.Find("Borders").Find("Element").GetComponent<Element>();
            Debug.Log("added " + transmutationSlot.TransmutationSlotID + " " + transmutationSlot);
            m_transmutationSlotsDictionary.Add(transmutationSlot.TransmutationSlotID, transmutationSlot);
        }
        
        foreach (Transform slot in baseElementsHolder)
        {
            //rewriteThere
            TransmutationElement transmutationElement = slot.GetComponent<TransmutationElement>();
            m_transmutationElementsDictionary.Add(transmutationElement.TransmutationSlotID, transmutationElement);
        }
    }

    public void AddActivatedObjectID(int addedID)
    {
        if (addedID != 0)
        {
            m_activatedObjectsIDs.Add(addedID);
        }
        
    }
    
    public void RemoveActivatedObjectID(int deletedID)
    {
        if (deletedID != 0)
        {
            m_activatedObjectsIDs.Remove(deletedID);
        }
        
    }
    
    
    void UpdateElementState(int transmutationSlotId)
    {
        Debug.Log("here " + m_transmutationSlotsDictionary[transmutationSlotId]);
        if (m_transmutationSlotsDictionary[transmutationSlotId].CustomID == 0)
        {
            m_elementsFilled--;
            if (m_elementsFilled < 0)
            {
                m_elementsFilled = 0;
            }
            m_activatedTransmutationElements.Remove(m_transmutationElementsDictionary[transmutationSlotId]);
            DisableElement(m_transmutationElementsDictionary[transmutationSlotId]);
        }
        else
        {
            m_elementsFilled++;
            m_activatedTransmutationElements.Add(m_transmutationElementsDictionary[transmutationSlotId]);
            VisualiseElement(m_transmutationElementsDictionary[transmutationSlotId], m_transmutationSlotsDictionary[transmutationSlotId].CustomID);
        }
    }

    public void ResetElementsList()
    {
        m_activatedTransmutationElements  = new List<TransmutationElement>();
        foreach (Element element in m_transmutationSlotsDictionary.Values)
        {
            element.CustomID = 0;
        }
        
    }

    void DisableElement(TransmutationElement transmutationElement)
    {
        Debug.Log("were are here 1");
        if (!m_coroutineIsRunning)
        {
            Debug.Log("start has ended2 " + m_currentCoroutine == null);
            m_currentCoroutine = StartCoroutine(HideWhenHandAnimationEnded(transmutationElement));
        }
        else
        {
            Debug.Log("coroutine is not null");
            if (m_currentCoroutine != null)
            {
                StopCoroutine(m_currentCoroutine);
            }
            m_currentCoroutine = null;
            m_coroutineIsRunning = false;
            //m_transmutationHandAnimator.InterruptAnimation();
            if (m_currentlyTaking)
            {
                ForceFinishTaking(m_currentlyManagedElement, m_currentlyManagedID);
            }
            else
            {
                ForceFinishHiding(m_currentlyManagedElement);
            }
        }
        
    }
    
    void VisualiseElement(TransmutationElement transmutationElement, int newID)
    {
        Debug.Log("were are here 2");
        if (!m_coroutineIsRunning)
        {
            Debug.Log("start has ended1 " + m_currentCoroutine == null);
            m_currentCoroutine = StartCoroutine(ShowWhenHandAnimationEnded(transmutationElement, newID));
        }
        else
        {
            Debug.Log("coroutine is not null");
            if (m_currentCoroutine != null)
            {
                StopCoroutine(m_currentCoroutine);
            }
            
            
            m_coroutineIsRunning = false;
            //m_transmutationHandAnimator.InterruptAnimation();
            if (m_currentlyTaking)
            {
                ForceFinishTaking(m_currentlyManagedElement, m_currentlyManagedID);
            }
            else
            {
                ForceFinishHiding(m_currentlyManagedElement);
            }
        }
        
    }

    void ForceFinishTaking(TransmutationElement transmutationElement, int newID)
    {
        transmutationElement.ShowObject(newID);
    }
    
    void ForceFinishHiding(TransmutationElement transmutationElement)
    {
        transmutationElement.HideVisibility();
    }

    IEnumerator HideWhenHandAnimationEnded(TransmutationElement transmutationElement)
    {
        m_coroutineIsRunning = true;
        Debug.Log("start has ended " + m_currentCoroutine == null);
        m_currentlyManagedElement = transmutationElement;
        m_currentlyTaking = true;
        m_currentlyManagedID = 0;
        //m_transmutationHandAnimator.ShowTakingObject();
        //yield return new WaitUntil(() => m_transmutationHandAnimator.AnimationFinished);
        
        transmutationElement.HideVisibility();
        //m_transmutationHandAnimator.AnimationFinished = false;
        
        m_currentCoroutine = null;
        m_coroutineIsRunning = false;
        Debug.Log("coroutine has ended " + m_currentCoroutine == null);
        yield return null;
    }
    
    IEnumerator ShowWhenHandAnimationEnded(TransmutationElement transmutationElement, int newID)
    {
        m_coroutineIsRunning = true;
        Debug.Log("start has ended " + m_currentCoroutine == null);
        m_currentlyManagedElement = transmutationElement;
        m_currentlyTaking = false;
        m_currentlyManagedID = newID;
        //m_transmutationHandAnimator.ShowPlacingObject();
        //yield return new WaitUntil(() => m_transmutationHandAnimator.AnimationFinished);
        transmutationElement.ShowObject(newID);
        //m_transmutationHandAnimator.AnimationFinished = false;
        m_currentCoroutine = null;
        m_coroutineIsRunning = false;
        Debug.Log("coroutine has ended " + m_currentCoroutine == null);
        yield return null;
    }
}
