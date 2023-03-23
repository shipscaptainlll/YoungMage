using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroEntering : MonoBehaviour
{
    [Header("Needed upper scripts")]
    [SerializeField] ContactManager contactManager;
    [SerializeField] ClickManager clickManager;

    [Header("Camera")]
    [SerializeField] Transform cameraDestination;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform cameraStartingPoint;
    [SerializeField] Transform lookAtTransform;
    static float staticMinimalDistance;
    CameraController camera;

    [Header("Other scripts")]
    [SerializeField] PersonMovement personMovement;
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] IntroScenesChanger introScenesChanger;
    [SerializeField] BookSpellsCaster bookSpellsCaster;
    [SerializeField] IntroMessagesInstantiator introMessagesInstantiator;
    [SerializeField] UIBlocker uIBlocker;
    [SerializeField] private Animator m_oldmageAnimator;


    [Header("Cache - delete safely")]
    [SerializeField] bool initiateOnStart;

    Coroutine cameraRepositioningCoroutine;

    bool isEscaping;
    bool isEntering;
    bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        camera = cameraTransform.GetComponent<CameraController>();
        
    }

    public void EnterIntro()
    {
        if (!isActive)
        {
            HideSpellsBook();
            camera.IntroMode = true;
            camera.enabled = false;
            isActive = true;
            isEntering = true;
            uIBlocker.BlockUI();
            uIBlocker.BlockQuickAccessPanel();
            uIBlocker.BlockSavingLoading();
            personMovement.enabled = false;

            introScenesChanger.UpdateCameraPosition();
            isEntering = false;

            camera.enabled = true;
            camera.CityRegenerationMode = true;
        }
    }

    public void ExitIntro()
    {
        if (isActive && !isEntering)
        {
            ShowSpellsBook();
            isEscaping = true;
            camera.IntroMode = false;
            

            cameraTransform.position = new Vector3(cameraStartingPoint.position.x, cameraStartingPoint.position.y, cameraStartingPoint.position.z);

            isEscaping = false;
            isActive = false;
            uIBlocker.UnBlockUI();
            uIBlocker.UnBlockQuickAccessPanel();
            uIBlocker.UnblockSavingLoading();
            camera.CityRegenerationMode = false;
            personMovement.enabled = true;
            m_oldmageAnimator.Play("IdleSleeping");
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

}
