using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerDataApplier
{
    static PersonMovement playerScriptLoaded;
    static CameraController cameraScriptLoaded;
    static PlayerData playerDataLoaded;

    public static void ApplyPlayerData(PersonMovement playerScript, CameraController cameraScript, PlayerData playerData)
    {
        UpdateData(playerScript, cameraScript, playerData);
        ApplyPlayerPosition();
        ApplyPlayerRotation();
        ApplyCameraRotation();
        DisconnectData();
    }

    static void UpdateData(PersonMovement playerScript, CameraController cameraScript, PlayerData playerData)
    {
        playerScriptLoaded = playerScript;
        cameraScriptLoaded = cameraScript;
        playerDataLoaded = playerData;
    }

    static void DisconnectData()
    {
        playerScriptLoaded = null;
        playerDataLoaded = null;
    }

    static void ApplyPlayerPosition()
    {
        Vector3 playerPosition = new Vector3(playerDataLoaded.position[0], playerDataLoaded.position[1], playerDataLoaded.position[2]);
        playerScriptLoaded.gameObject.SetActive(false);
        playerScriptLoaded.gameObject.transform.position = playerPosition;
        playerScriptLoaded.gameObject.SetActive(true);
    }

    static void ApplyPlayerRotation()
    {
        Vector3 playerRotation = new Vector3(playerDataLoaded.rotation[0], playerDataLoaded.rotation[1], playerDataLoaded.rotation[2]);
        playerScriptLoaded.gameObject.SetActive(false);
        playerScriptLoaded.gameObject.transform.rotation = Quaternion.Euler(playerRotation);
        playerScriptLoaded.gameObject.SetActive(true);
    }

    static void ApplyCameraRotation()
    {
        Vector3 cameraRotation = new Vector3(playerDataLoaded.cameraRotation[0], playerDataLoaded.cameraRotation[1], playerDataLoaded.cameraRotation[2]);
        Debug.Log(cameraScriptLoaded.gameObject.transform.localRotation.eulerAngles);
        cameraScriptLoaded.gameObject.SetActive(false);
        if (cameraRotation.x > 50) { cameraRotation.x -= 360; }
        cameraScriptLoaded.YRotation = cameraRotation.x;
        Debug.Log(cameraRotation);
        cameraScriptLoaded.gameObject.SetActive(true);
    }
}
