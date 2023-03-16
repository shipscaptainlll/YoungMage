using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFlowManager : MonoBehaviour
{
    [SerializeField] IntroEntering introEntering;
    [SerializeField] IntroScenesChanger scenesChanger;
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] IntroMessagesInstantiator messagesInstantiator;
    [SerializeField] CharactersEmotionsShower charactersEmotionsShower;
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform soldierModel;
    [SerializeField] Transform oldMageParticleSystem;
    [SerializeField] Animator oldMageAnimator;

    bool introFinished;

    public bool IntroFinished { get { return introFinished; } }

    void Start()
    {
        clickManager.VClicked += StartIntro;
    }

    public void StartIntro()
    {
        panelsManager.OpenIntroPanel();
        introEntering.EnterIntro();
        
        clickManager.LMBClicked += UpdateIntroFlow;
        clickManager.EscClicked += UpdateIntroFlow;
        clickManager.EnterClicked += UpdateIntroFlow;
        clickManager.RMBClicked += UpdateIntroFlow;
        clickManager.SpaceClicked += UpdateIntroFlow;

        UpdateIntroFlow();
        
    }

    public void UpdateIntroFlow()
    {
        
        if (!introFinished)
        {
            if (messagesInstantiator.IntroIndex == messagesInstantiator.NumberOfMessages) { ExitIntro(); return; }
            messagesInstantiator.ShowNextMessage();
            charactersEmotionsShower.ShowAnEmotion(messagesInstantiator.IntroIndex);
            if (messagesInstantiator.IntroIndex == 4 && scenesChanger.SceneIndex == 0) { skeletonArenaInstantiator.CreateIntroScene(); scenesChanger.ShowNextScene(); }
            if (messagesInstantiator.IntroIndex == 6 && scenesChanger.SceneIndex == 1) { scenesChanger.ShowNextScene(); }
            if (messagesInstantiator.IntroIndex == 8 && scenesChanger.SceneIndex == 2) { scenesChanger.ShowNextScene(); }
        }
    }

    public void ExitIntro()
    {
        
        clickManager.LMBClicked -= UpdateIntroFlow;
        clickManager.EscClicked -= UpdateIntroFlow;
        clickManager.EnterClicked -= UpdateIntroFlow;
        clickManager.RMBClicked -= UpdateIntroFlow;
        clickManager.SpaceClicked -= UpdateIntroFlow;

        oldMageParticleSystem.gameObject.SetActive(true);
        oldMageAnimator.Play("IdleSleeping");

        soldierModel.gameObject.SetActive(false);

        panelsManager.CloseIntroPanel();
        messagesInstantiator.HideMessages();
        introEntering.ExitIntro();
    }

}
