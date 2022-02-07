using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOccupation : MonoBehaviour
{
    [SerializeField] ContactManager _contactManager;
    [SerializeField] ClickManager _clickManager;
    string _engagedObjectName;
    Transform _engagedObject;
    bool _isOccupied;

    public event Action CharacterEngagedSomething = delegate { };
    public event Action CharacterDisengagedSomething = delegate { };
    public event Action<Transform> CharacterEngagedTransmutation = delegate { };
    public event Action<Transform> CharacterDisengagedTransmutation = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        _contactManager.AlchemistTableDetected += EngageObject;
        _clickManager.EscClicked += DisengageObject;
        _clickManager.RMBClicked += DisengageObject;
    }

    void EngageObject(Transform engagedObject)
    {
        if (!_isOccupied)
        {
            _isOccupied = true;
            PreventCharacterMovement();
            GetObjectData(engagedObject);
            EnableObjectInteraction();
        }
    }

    void DisengageObject()
    {
        if (_isOccupied)
        {
            _isOccupied = false;
            EnableCharacterMovement();
            DisableObjectInteraction();
            ClearObjectData();
        }
    }

    void GetObjectData(Transform engagedObject)
    {
        _engagedObject = engagedObject;
        _engagedObjectName = engagedObject.GetComponent<IObject>().Name;
    }

    void ClearObjectData()
    {
        _engagedObject = null;
        _engagedObjectName = "";
    }

    void EnableObjectInteraction()
    {
        switch (_engagedObjectName)
        {
            
            case "alchemist table":
                if (CharacterEngagedTransmutation != null) CharacterEngagedTransmutation(_engagedObject);
                break;
        }
    }

    void DisableObjectInteraction()
    {
        switch (_engagedObjectName)
        {
            case "alchemist table":
                if (CharacterDisengagedTransmutation != null) CharacterDisengagedTransmutation(_engagedObject);
                break;
        }
    }

    void PreventCharacterMovement()
    {
        if (CharacterEngagedSomething != null)
        {
            CharacterEngagedSomething();
        }
    }

    void EnableCharacterMovement()
    {
        if (CharacterDisengagedSomething != null)
        {
            CharacterDisengagedSomething();
        }
    }
}
