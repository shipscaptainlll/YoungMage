using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationElementsManager : MonoBehaviour
{
    [SerializeField] private Transform baseElementsHolder;
    [SerializeField] private Transform transmutationSlotsHolder;
    [SerializeField] private TransmutationHandAnimator m_transmutationHandAnimator;
    private List<TransmutationElement> m_activatedTransmutationElements = new List<TransmutationElement>();
    private int m_elementsFilled;
    private Dictionary<int, TransmutationElement> m_transmutationElementsDictionary = new Dictionary<int, TransmutationElement>();
    private Dictionary<int, Element> m_transmutationSlotsDictionary = new Dictionary<int, Element>();
    private Coroutine m_currentCoroutine;
    private TransmutationElement m_currentlyManagedElement;
    private bool m_currentlyTaking;
    private int m_currentlyManagedID;

    public int ElementsFilled { get {return m_elementsFilled;} }
    
    
    public List<TransmutationElement> ActivatedTransmutationElements { get { return m_activatedTransmutationElements; } }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform slot in transmutationSlotsHolder)
        {
            
            slot.Find("Borders").Find("Element").GetComponent<DragHandler>().TransmutationSlotElementFilled += UpdateElementState;

            Element transmutationSlot = slot.Find("Borders").Find("Element").GetComponent<Element>();
            m_transmutationSlotsDictionary.Add(transmutationSlot.TransmutationSlotID, transmutationSlot);
        }
        
        foreach (Transform slot in baseElementsHolder)
        {
            //rewriteThere
            TransmutationElement transmutationElement = slot.GetComponent<TransmutationElement>();
            m_transmutationElementsDictionary.Add(transmutationElement.TransmutationSlotID, transmutationElement);
        }
    }
    
    
    void UpdateElementState(int transmutationSlotId)
    {
        if (m_transmutationSlotsDictionary[transmutationSlotId].CustomID == 0 ||
            m_transmutationSlotsDictionary[transmutationSlotId].AttachedCounter.GetComponent<ICounter>().Count == 0)
        {
            m_elementsFilled--;
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
        foreach (TransmutationElement element in m_activatedTransmutationElements)
        {
            element.ShowObject(0);;
        }
        m_activatedTransmutationElements  = new List<TransmutationElement>();
    }

    void DisableElement(TransmutationElement transmutationElement)
    {
        if (m_currentCoroutine == null)
        {
            m_currentCoroutine = StartCoroutine(HideWhenHandAnimationEnded(transmutationElement));
        }
        else
        {
            StopCoroutine(m_currentCoroutine);
            m_currentCoroutine = null;
            m_transmutationHandAnimator.InterruptAnimation();
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
        if (m_currentCoroutine == null)
        {
            m_currentCoroutine = StartCoroutine(ShowWhenHandAnimationEnded(transmutationElement, newID));
        }
        else
        {
            StopCoroutine(m_currentCoroutine);
            m_currentCoroutine = null;
            m_transmutationHandAnimator.InterruptAnimation();
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
        m_currentlyTaking = true;
        m_currentlyManagedID = 0;
        m_transmutationHandAnimator.ShowTakingObject();
        yield return new WaitUntil(() => m_transmutationHandAnimator.AnimationFinished);
        
        transmutationElement.HideVisibility();
        m_transmutationHandAnimator.AnimationFinished = false;
        m_currentCoroutine = null;
        yield return null;
    }
    
    IEnumerator ShowWhenHandAnimationEnded(TransmutationElement transmutationElement, int newID)
    {
        m_currentlyTaking = false;
        m_currentlyManagedID = newID;
        m_transmutationHandAnimator.ShowPlacingObject();
        yield return new WaitUntil(() => m_transmutationHandAnimator.AnimationFinished);
        transmutationElement.ShowObject(newID);
        m_transmutationHandAnimator.AnimationFinished = false;
        m_currentCoroutine = null;
        yield return null;
    }
}
