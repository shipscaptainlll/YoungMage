using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationResourceChoose : MonoBehaviour, IShowClickable, IObject
{
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    [SerializeField] CharacterOccupation _characterOccupation;
    [SerializeField] GameObject _choosenResourcesHolder;
    [SerializeField] ClickManager _clickManager;
    [SerializeField] float _updateSpeed;
    [SerializeField] ResourceBottleStorage _resourceBottleStorage;
    [SerializeField] Transform _resourcesPositionsHolder;

    [Header("Sounds Manager Settings")]
    [SerializeField] SoundManager soundManager;
    AudioSource openingSound;
    AudioSource rotatingSound;
    AudioSource applyElementSound;

    [Header("Alchemist Circles Rotation Settings")]
    [SerializeField] TransmutationCircleRotation transmutationCircleRotation;

    List<GameObject> _accessibleResources = new List<GameObject>();
    Transform _chosenResource;
    float _currentAngle = 0f;
    float _circleRadius = 0.25f;
    float _angleOffset = 0;
    float _deltaAngle;
    bool _transmutationEnabled;
    float _rotationAngle;
    bool _isRotating;
    string _name = "alchemist table";

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public Transform ChosenResource { get { return _chosenResource; } }

    public event Action<string> ObjectFound = delegate { };
    public event Action<string> ObjectUnfound = delegate { };
    public event Action<Transform> ResourceChosen = delegate { };
    public event Action<Transform> ResourceUnchosen = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.AmuletRequestedReset += ResetResourceChoosing;
        SubscribeOnEvents();
        openingSound = soundManager.LocateAudioSource("OnElementAlchtab", transform);
        rotatingSound = soundManager.LocateAudioSource("RotateElementAlchtab", transform);
        applyElementSound = soundManager.LocateAudioSource("ApplyElementAlchtab", transform);
    }


    void InitialiseInstance()
    {
        InitializeParameters();
        ResourcesStartConfigurations();
    }

    void InitializeParameters()
    {
        _transmutationEnabled = false;
    }
    void ResourcesStartConfigurations()
    {
        InitializeAccessibleResources();
        HideResourcePack();
    }

    void SubscribeOnEvents()
    {
        _characterOccupation.CharacterEngagedTransmutation += StartResourceChoosing;
        _characterOccupation.CharacterDisengagedTransmutation += StopResourceChoosing;
        _characterOccupation.CharacterResetedPack += ResetResourceChoosing;
        _characterOccupation.LMBClicked += ChooseResource;
        _characterOccupation.EnterClicked += ChooseResource;
        _clickManager.AClicked += RotateResourcesLeft;
        _clickManager.DClicked += RotateResourcesRight;
        _resourceBottleStorage.ResourcesCountChanged += InitializeAccessibleResources;
        _resourceBottleStorage.InstanceInitialised += InitialiseInstance;
    }

    void InitializeAccessibleResources()
    {
        if (!_transmutationEnabled) { _currentAngle = 0; transform.localRotation = Quaternion.Euler(new Vector3(0, _currentAngle, 0)); }
        VisualizeAccessibleResources();
        ResourcesPositionRecalculate();
        RotationSpeedRecalculate();

        if (!_transmutationEnabled) { HideResourcePack(); }
    }

    void VisualizeAccessibleResources()
    {
        foreach (Transform position in _resourcesPositionsHolder)
        {
            position.gameObject.GetComponent<MeshRenderer>().enabled = false;
            
        }

        if (_resourceBottleStorage.GetComponent<ResourceBottleStorage>().ActiveCounters != null)
            _accessibleResources.Clear();
        foreach (ResourceCounter resourceCounter in _resourceBottleStorage.GetComponent<ResourceBottleStorage>().ActiveCounters)
            {
                
                foreach (Transform resource in _resourcesPositionsHolder)
                {
                    if (resource.GetComponent<AlchemistTableResource>().ID == resourceCounter.ID)
                    {
                        
                        resource.gameObject.GetComponent<MeshRenderer>().enabled = true;


                    _accessibleResources.Add(resource.gameObject);
                    }
                }
            }
    }

    void ResourcesPositionRecalculate()
    {
        if (_resourceBottleStorage.GetComponent<ResourceBottleStorage>().ActiveCounters.Count > 0)
        {
            _deltaAngle = 360 / _resourceBottleStorage.GetComponent<ResourceBottleStorage>().ActiveCounters.Count;
            for (int i = 1; i <= _accessibleResources.Count; i++)
            {
                Transform resource = _accessibleResources[i - 1].transform;
                if (i * _deltaAngle == 360)
                {
                    _accessibleResources[i - 1].GetComponent<AlchemistTableResource>().AppointedAngle = 0;
                } else
                {
                    _accessibleResources[i - 1].GetComponent<AlchemistTableResource>().AppointedAngle = i * _deltaAngle;
                }
                float angle = 180 + i * _deltaAngle;
                float xPosition = _circleRadius * Mathf.Cos((angle * Mathf.PI) / 180);
                float zPosition = _circleRadius * Mathf.Sin((angle * Mathf.PI) / 180);

                resource.localPosition = new Vector3(xPosition, resource.localPosition.y, zPosition);
            }
        }
        
    }

    void RotationSpeedRecalculate()
    {
        if (_resourceBottleStorage.GetComponent<ResourceBottleStorage>().ActiveCounters.Count > 0)
        {
            _rotationAngle = 360 / _accessibleResources.Count;
        }
        
    }

    void StartResourceChoosing(Transform engagedResourcePack)
    {
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            transmutationCircleRotation.CircleChooseRotation();
            //Debug.Log("Hello there123");
            ShowResourcePack();
            ClearChosenVisualisation();
            if (ResourceUnchosen != null)
            {
                ResourceUnchosen(transform);
            }
            _transmutationEnabled = true;
        }
    }

    void StopResourceChoosing(Transform engagedResourcePack)
    {
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            HideResourcePack();
            VisualizeChosenResource();
            
            _transmutationEnabled = false;
        }
    }

    void ResetResourceChoosing(Transform engagedResourcePack)
    {
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            ResetChosenResource();
            HideResourcePack();
            _transmutationEnabled = false;
        }
    }

    void RotateResourcesLeft()
    {
        if (_transmutationEnabled && !_isRotating)
        {
            rotatingSound.Play();
            _isRotating = true;
            ChangeCurrentAngle(-_deltaAngle);
            StartCoroutine(Rotate(-_rotationAngle, _updateSpeed));
        }
    }

    void RotateResourcesRight()
    {
        if (_transmutationEnabled && !_isRotating)
        {
            rotatingSound.Play();
            _isRotating = true;
            ChangeCurrentAngle(_deltaAngle);
            StartCoroutine(Rotate(_rotationAngle, _updateSpeed));
        }
    }

    void ChangeCurrentAngle(float changeAmmount)
    {
        if (_currentAngle + changeAmmount >= 360)
        {
            _currentAngle += changeAmmount - 360;
        } else if (_currentAngle + changeAmmount < 0)
        {
            _currentAngle += changeAmmount + 360;
        } else
        {
            _currentAngle += changeAmmount;
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
        transform.parent.Find("OutlineResource").localRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, startYAngle + targetAngle, transform.eulerAngles.z));
        _isRotating = false;
    }    

    void VisualizeChosenResource()
    {
        ClearChosenVisualisation();
        GameObject targetObject = GetChoosenResource();
        transmutationCircleRotation.CircleChoosenRotation();
        //Debug.Log("Hello there12");
        foreach (Transform panel in _choosenResourcesHolder.transform)
        {
            if (panel.GetComponent<AlchemistTableResource>().ID == targetObject.transform.GetComponent<AlchemistTableResource>().ID)
            {
                panel.GetComponent<AlchemistTableResource>().gameObject.GetComponent<MeshRenderer>().enabled = true;
                _chosenResource = panel;
                if (ResourceChosen != null)
                {
                    ResourceChosen(transform);
                }
            }
        }
    }

    void ResetChosenResource()
    {
        ClearChosenVisualisation();
        GameObject targetObject = GetChoosenResource();
        foreach (Transform panel in _choosenResourcesHolder.transform)
        {
            if (panel.GetComponent<AlchemistTableResource>().ID == 0)
            {
                panel.GetComponent<AlchemistTableResource>().gameObject.GetComponent<MeshRenderer>().enabled = true;
                _chosenResource = panel;
                if (ResourceChosen != null)
                {
                    ResourceChosen(transform);
                }
            }
        }
    }

    void ClearChosenVisualisation()
    {
        foreach (Transform resource in _choosenResourcesHolder.transform)
        {
            resource.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        _chosenResource = null;
    }

    GameObject GetChoosenResource()
    {
        foreach (GameObject resource in _accessibleResources)
        {
            if (resource.transform.GetComponent<AlchemistTableResource>().AppointedAngle == _currentAngle)
            {
                return resource;
            }
        }
        return null;
    }

    

    void HideResourcePack()
    {
        
        
        foreach (GameObject resource in _accessibleResources)
        {
            resource.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

    void ChooseResource(Transform engagedResourcePack)
    {
        //Debug.Log(engagedResourcePack + " engaged resource pack");
        if (engagedResourcePack.parent.Find("ChooseResource") == transform)
        {
            applyElementSound.Play();
            //Debug.Log("found");
            StopResourceChoosing(engagedResourcePack);
        }
    }

    void ShowResourcePack()
    {
        
        foreach (GameObject resource in _accessibleResources)
        {
            //Debug.Log("Shown");
            resource.GetComponent<MeshRenderer>().enabled = true;
            resource.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Show()
    {
        openingSound.Play();
        transmutationCircleRotation.CircleLookRotation();
        //Debug.Log("Hello there1");
        if (!_transmutationEnabled && !_characterOccupation.IsOccupied)
        {
            foreach (GameObject resource in _accessibleResources)
            {
                resource.GetComponent<MeshRenderer>().enabled = true;
                resource.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                //if (resource.GetComponent<AlchemistTableResource>().ID )  

            }
            foreach (Transform panel in _choosenResourcesHolder.transform)
            {
                if (_chosenResource != null && _chosenResource.GetComponent<AlchemistTableResource>().ID == panel.GetComponent<AlchemistTableResource>().ID)
                {
                    panel.GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
        ObjectFound("TransmutationSlot");
    }

    public void Hide()
    {
        //Debug.Log("Hello there");
        transmutationCircleRotation.CircleDefaultRotation();
        if (!_transmutationEnabled)
        {
            foreach (GameObject resource in _accessibleResources)
            {
                resource.GetComponent<MeshRenderer>().enabled = false;
                resource.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;

            }
            foreach (Transform panel in _choosenResourcesHolder.transform)
            {
                if (_chosenResource != null && _chosenResource.GetComponent<AlchemistTableResource>().ID == panel.GetComponent<AlchemistTableResource>().ID)
                {
                    panel.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }
        ObjectUnfound("TransmutationSlot");
    }
}