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
    public event Action<Transform> LMBClicked = delegate { };
    public event Action<Transform> EnterClicked = delegate { };
    public event Action<Transform> CharacterEngagedTransmutation = delegate { };
    public event Action<Transform> CharacterDisengagedTransmutation = delegate { };
    public event Action<Transform> CharacterResetedPack = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        _contactManager.AlchemistTableDetected += EngageObject;
        _clickManager.EscClicked += ResetPack;
        _clickManager.RMBClicked += ResetPack;
        //_clickManager.LMBClicked += NotifyLMBClicked;
        _clickManager.EnterClicked += NotifyEnterClicked;
    }

    void EngageObject(Transform engagedObject)
    {
        if (!_isOccupied)
        {
            //Debug.Log("Engaged alchemist table");
            _isOccupied = true;
            PreventCharacterMovement();
            GetObjectData(engagedObject);
            EnableObjectInteraction();
        } else if (_isOccupied)
        {
            DisengageObject();
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

    void ResetPack()
    {
        if (_isOccupied)
        {
            _isOccupied = false;
            EnableCharacterMovement();
            ResetObjectInteraction();
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

    void ResetObjectInteraction()
    {
        switch (_engagedObjectName)
        {
            case "alchemist table":
                if (CharacterResetedPack != null) CharacterResetedPack(_engagedObject);
                break;
        }
    }

    /*
    void NotifyLMBClicked()
    {
        switch (_engagedObjectName)
        {
            case "alchemist table":
                if (LMBClicked != null) LMBClicked(_engagedObject);
                DisengageObject();
                break;
        }
    }
    */
    void NotifyEnterClicked()
    {
        switch (_engagedObjectName)
        {
            case "alchemist table":
                if (EnterClicked != null) EnterClicked(_engagedObject);
                DisengageObject();
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
