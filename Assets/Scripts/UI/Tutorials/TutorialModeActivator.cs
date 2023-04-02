using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialModeActivator : MonoBehaviour
{
    [SerializeField] PersonMovement personMovement;
    [SerializeField] CameraController cameraController;
    bool modeActive;

    public bool ModeActive { get { return modeActive; } }

    public void ApplyTutorialReadingMode(int tutorialID)
    {
        modeActive = true;
        personMovement.TutorialModeActivated = true;
        if (tutorialID != 10
            && tutorialID != 11
            && tutorialID != 12
            && tutorialID != 13)
        {
            cameraController.TutorialModeActivated = true;
        }
        
        CursorManager.ForceCursorEnabled();
    }

    public void DisengageTutorialReadingMode()
    {
        modeActive = false;
        personMovement.TutorialModeActivated = false;
        cameraController.TutorialModeActivated = false;
        CursorManager.ForceCursorDisabled();
    }
}
