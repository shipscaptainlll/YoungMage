using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScenesChanger : MonoBehaviour
{
    [SerializeField] Transform cameraPositionsHolder;
    [SerializeField] Transform cameraLookAtHolder;
    [SerializeField] Transform cameraTransform;
    int sceneIndex;

    public int SceneIndex { get { return sceneIndex; } }

    void Start()
    {
        sceneIndex = 0;
    }

    public void ShowNextScene()
    {
        sceneIndex++;
        UpdateCameraPosition();
    }

    public void UpdateCameraPosition()
    {
        cameraTransform.position = new Vector3(cameraPositionsHolder.GetChild(sceneIndex).position.x, cameraPositionsHolder.GetChild(sceneIndex).position.y, cameraPositionsHolder.GetChild(sceneIndex).position.z);
        cameraTransform.LookAt(cameraLookAtHolder.GetChild(sceneIndex));
    }
}
