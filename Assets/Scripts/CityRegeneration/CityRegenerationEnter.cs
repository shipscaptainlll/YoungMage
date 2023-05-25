using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityRegenerationEnter : MonoBehaviour
{
    [Header("Needed upper scripts")]
    [SerializeField] ContactManager contactManager;
    [SerializeField] ClickManager clickManager;
    [SerializeField] private QuickAccessHandController m_quickAccessHandController;

    [Header("Camera")]
    [SerializeField] Transform cameraDestination;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform cameraStartingPoint;
    [SerializeField] Transform lookAtTransform;
    [SerializeField] float cameraMinimalDistance;
    static float staticMinimalDistance;
    CameraController camera; 

    [Header("Other scripts")]
    [SerializeField] PersonMovement personMovement;

    [SerializeField] private Transform m_playerPositionEnter;
    [SerializeField] private Transform m_cameraTransformLookAt;
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] BookSpellsCaster bookSpellsCaster;

    [SerializeField] ParticleSystem enterPartycleSystem;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] Transform floatingSoundHolder;
    AudioSource floatingSound;
    AudioSource zoomInSound;
    AudioSource zoomOutSound;

    [Header("Cache - delete safely")]
    [SerializeField] Transform cityWallUpgrade;
    [SerializeField] Transform cityCastleUpgrade;
    [SerializeField] Transform cityBlacksmithUpgrade;

    Coroutine cameraRepositioningCoroutine;

    bool isEscaping;
    bool isEntering;
    bool isActive;

    public static float CameraMinimalDistance { get { return staticMinimalDistance; } }
    // Start is called before the first frame update
    void Start()
    {
        staticMinimalDistance = cameraMinimalDistance;
        camera = cameraTransform.GetComponent<CameraController>();
        contactManager.CityRegenerationEntered += EnterCityRegeneration;
        clickManager.EscClicked += ExitCityRegeneration;
        floatingSound = soundManager.LocateAudioSource("FloatingCastle", floatingSoundHolder);
        zoomInSound = soundManager.FindSound("CameraZoomIn");
        zoomOutSound = soundManager.FindSound("CameraZoomOut");
        floatingSound.Play();
        //Debug.Log("ready");
    }

    void EnterCityRegeneration()
    {
        Debug.Log("Here2");
        if (!isActive && m_quickAccessHandController.CurrentCustomID == 10)
        {
            
            
            
            Debug.Log("Here1");
            zoomInSound.Play();
            HideSpellsBook();
            cityWallUpgrade.GetComponent<CanvasGroup>().alpha = 1;
            cityCastleUpgrade.GetComponent<CanvasGroup>().alpha = 1;
            cityBlacksmithUpgrade.GetComponent<CanvasGroup>().alpha = 1;
            enterPartycleSystem.gameObject.SetActive(true);
            enterPartycleSystem.Play();
            personMovement.transform.position = m_playerPositionEnter.position;
            camera.enabled = false;
            isActive = true;
            isEntering = true;
            panelsManager.EscapeMenuBlocked = true;
            personMovement.TurnOffSounds();
            personMovement.Occupied = true;
            personMovement.TurnOffSounds();
            //personMovement.enabled = false;
            quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
            if (cameraRepositioningCoroutine != null) { 
                StopCoroutine(cameraRepositioningCoroutine);
                cameraRepositioningCoroutine = null;
            }
            cameraRepositioningCoroutine = StartCoroutine(CameraToDestination(cameraTransform, cameraDestination, 1.2f));
        }
    }

    public void ShowSpellsBook()
    {
        bookSpellsCaster.ShowSpellsBook();
    }

    public void HideSpellsBook()
    {
        bookSpellsCaster.HideSpellsBook();
    }

    IEnumerator CameraToDestination(Transform startPoint, Transform endPoint, float delay)
    {
        float elapsed = 0;
        float maxTime = delay;
        float xStartPosition = startPoint.position.x;
        float yStartPosition = startPoint.position.y;
        float zStartPosition = startPoint.position.z;
        float xPosition = startPoint.position.x;
        float yPosition = startPoint.position.y;
        float zPosition = startPoint.position.z;
        
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            
            xPosition = Mathf.Lerp(xStartPosition, endPoint.position.x, elapsed / maxTime);
            yPosition = Mathf.Lerp(yStartPosition, endPoint.position.y, elapsed / maxTime);
            zPosition = Mathf.Lerp(zStartPosition, endPoint.position.z, elapsed / maxTime);
            cameraTransform.position = new Vector3(xPosition, yPosition, zPosition);
            m_cameraTransformLookAt.transform.LookAt(lookAtTransform);
            yield return null;
        }
        cameraTransform.position = new Vector3(endPoint.position.x, endPoint.position.y, endPoint.position.z);
        cameraRepositioningCoroutine = null;
        if (isEntering) { 
            isEntering = false;
            CursorManager.ForceCursorEnabled();
            CityRegenerationMouse.IsActive = true;
            enterPartycleSystem.gameObject.SetActive(false);
            camera.enabled = true;
            camera.CityRegenerationMode = true;
        } else if (isEscaping) { 
            isEscaping = false;
            isActive = false;
            panelsManager.EscapeMenuBlocked = false;
            camera.CityRegenerationMode = false;
            
            //personMovement.enabled = true;
            personMovement.Occupied = false;
            m_cameraTransformLookAt.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //camera.CityRegenerationMode = false;
            quickAccessPanel.GetComponent<CanvasGroup>().alpha = 1;
        }
        yield return null;
    }

    public void ExitCityRegeneration()
    {
        if (isActive && !isEntering)
        {
            Debug.Log("left city regeneration");
            zoomOutSound.Play();
            ShowSpellsBook();
            cityWallUpgrade.GetComponent<CanvasGroup>().alpha = 0;
            cityCastleUpgrade.GetComponent<CanvasGroup>().alpha = 0;
            cityBlacksmithUpgrade.GetComponent<CanvasGroup>().alpha = 0;
            isEscaping = true;
            
            CursorManager.ForceCursorDisabled();
            CityRegenerationMouse.IsActive = false;
            if (cameraRepositioningCoroutine != null)
            {
                StopCoroutine(cameraRepositioningCoroutine);
                cameraRepositioningCoroutine = null;
            }
            cameraRepositioningCoroutine = StartCoroutine(CameraToDestination(cameraTransform, cameraStartingPoint, 0.6f));
        }
    }
}
