using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationResourceChoose : MonoBehaviour
{
    [SerializeField] CharacterOccupation _characterOccupation;
    [SerializeField] ClickManager _clickManager;
    [SerializeField] float _updateSpeed;
    List<GameObject> _accessibleResources = new List<GameObject>();
    bool _transmutationEnabled;
    float _rotationAngle;
    bool _isRotating;
    

    
    // Start is called before the first frame update
    void Start()
    {
        InitializeParameters();
        InitializeAccessibleResources();
        SubscribeOnEvents();
    }

    void StartResourceChoosing(Transform engagedResourcePack)
    {
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            _transmutationEnabled = true;
        }
    }

    void StopResourceChoosing(Transform engagedResourcePack)
    {
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            _transmutationEnabled = false;
        }
    }

    void RotateResourcesLeft()
    {
        if (_transmutationEnabled && !_isRotating)
        {
            _isRotating = true;
            StartCoroutine(Rotate(-_rotationAngle, _updateSpeed));
        }
    }

    void RotateResourcesRight()
    {
        if (_transmutationEnabled && !_isRotating)
        {
            _isRotating = true;
            StartCoroutine(Rotate(_rotationAngle, _updateSpeed));
        }
    }

    IEnumerator Rotate(float angle, float updateSpeed)
    {
        float elapsed = 0;
        float startAngle = 0;
        float targetAngle = angle;
        float startYAngle = transform.eulerAngles.y;
        
        float currentAngle;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentAngle = Mathf.Lerp(startAngle, targetAngle, elapsed / updateSpeed);
            transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, startYAngle + currentAngle, transform.eulerAngles.z));
            yield return null;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, startYAngle + targetAngle, transform.eulerAngles.z));
        _isRotating = false;
    }    

    void InitializeParameters()
    {
        _transmutationEnabled = false;
    }

    void InitializeAccessibleResources()
    {
        _accessibleResources.Clear();
        foreach (Transform resource in transform)
        {
            _accessibleResources.Add(resource.gameObject);
        }
        RecalculateRotationAngle();
    }

    void RecalculateRotationAngle()
    {
        _rotationAngle = 360 / _accessibleResources.Count;
    }

    void SubscribeOnEvents()
    {
        _characterOccupation.CharacterEngagedTransmutation += StartResourceChoosing;
        _characterOccupation.CharacterDisengagedTransmutation += StopResourceChoosing;
        _clickManager.AClicked += RotateResourcesLeft;
        _clickManager.DClicked += RotateResourcesRight;
    }
}