using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationTableStateMachine : MonoBehaviour
{
    [SerializeField] Transform elementsHolder;
    [SerializeField] PotentialProductVisualisation potentialProductVisualisation;

    public Transform ElementsHolder { get { return elementsHolder; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int GetPotentialProductID(PotentialProductVisualisation potentialProductVisualisation)
    {
        return potentialProductVisualisation.CurrentProductID;
    }

}
