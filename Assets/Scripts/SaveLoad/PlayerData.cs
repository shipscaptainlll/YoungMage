using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float[] rotation;
    public float[] cameraRotation;

    public PlayerData (PersonMovement personMovement, CameraController cameraController)
    {
        GetPosition(personMovement);
        GetCameraRotation(cameraController);
        GetRotation(personMovement);
        
    }

    void GetPosition(PersonMovement personMovement)
    {
        position = new float[3];
        position[0] = personMovement.gameObject.transform.position.x;
        position[1] = personMovement.gameObject.transform.position.y;
        position[2] = personMovement.gameObject.transform.position.z;
    }

    void GetRotation(PersonMovement personMovement)
    {
        rotation = new float[3];
        rotation[0] = personMovement.gameObject.transform.eulerAngles.x; 
        rotation[1] = personMovement.gameObject.transform.eulerAngles.y;
        rotation[2] = personMovement.gameObject.transform.eulerAngles.z;
    }

    void GetCameraRotation(CameraController cameraController)
    {
        cameraRotation = new float[3];
        cameraRotation[0] = cameraController.gameObject.transform.rotation.eulerAngles.x;
        cameraRotation[1] = cameraController.gameObject.transform.rotation.eulerAngles.y;
        cameraRotation[2] = cameraController.gameObject.transform.rotation.eulerAngles.z;
    }
}
