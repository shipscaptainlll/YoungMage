using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReloadingInitialiser : MonoBehaviour
{
    [SerializeField] CameraController cameraController;

    public void MassiveReinitialiseAfterLoading()
    {
        cameraController.ReinitialiseAfterLoading();
    }
}
