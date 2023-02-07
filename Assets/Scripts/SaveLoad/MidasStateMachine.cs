using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasStateMachine : MonoBehaviour
{
    [SerializeField] Transform inPool;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] MidasCollectorCatcher midasCollectorCatcher;
    [SerializeField] Transform materialsPool;
    [SerializeField] Transform coinsPool;
    [SerializeField] PipeElementInstantiator pipeElementInstantiator;
    [SerializeField] MidasConversionProcess midasConversionProcess;
    [SerializeField] MidasCoinsCatcher midasCoinsCatcher;


    public Transform InPool { get { return inPool; } }
    public Transform MaterialsPool { get { return materialsPool; } }
    public Transform CoinsPool { get { return coinsPool; } }

    public void ApplyInletObjects(MidasData midasData)
    {
        int indexer = 0;
        while (indexer < midasData.inletObjectsPositions.Length)
        {
            Vector3 objectPosition = new Vector3(midasData.inletObjectsPositions[indexer][0], midasData.inletObjectsPositions[indexer][1], midasData.inletObjectsPositions[indexer][2]);
            Vector3 objectRotation = new Vector3(midasData.inletObjectsRotations[indexer][0], midasData.inletObjectsRotations[indexer][1], midasData.inletObjectsRotations[indexer][2]);
            KickOutItem(midasData.inletObjectsIDs[indexer], objectPosition, Quaternion.Euler(objectRotation));
            indexer++;
        }
    }

    public void ApplyMaterialsObjects(MidasData midasData)
    {
        int indexer = 0;
        while (indexer < midasData.pipeMaterialsPositions.Length)
        {
            Vector3 objectPosition = new Vector3(midasData.pipeMaterialsPositions[indexer][0], midasData.pipeMaterialsPositions[indexer][1], midasData.pipeMaterialsPositions[indexer][2]);
            Vector3 objectRotation = new Vector3(midasData.pipeMaterialsRotations[indexer][0], midasData.pipeMaterialsRotations[indexer][1], midasData.pipeMaterialsRotations[indexer][2]);
            float second = midasData.pipeMaterialsSeconds[indexer];
            pipeElementInstantiator.InstantiatePipeObject(objectPosition, Quaternion.Euler(objectRotation), second);
            indexer++;
        }
    }

    public void ApplyCoinsObjects(MidasData midasData)
    {
        int indexer = 0;
        while (indexer < midasData.pipeCoinsPositions.Length)
        {
            Vector3 objectPosition = new Vector3(midasData.pipeCoinsPositions[indexer][0], midasData.pipeCoinsPositions[indexer][1], midasData.pipeCoinsPositions[indexer][2]);
            Vector3 objectRotation = new Vector3(midasData.pipeCoinsRotations[indexer][0], midasData.pipeCoinsRotations[indexer][1], midasData.pipeCoinsRotations[indexer][2]);
            float second = midasData.pipeCoinsSeconds[indexer];
            pipeElementInstantiator.InstantiateCoinObject(objectPosition, Quaternion.Euler(objectRotation), second);
            indexer++;
        }
    }

    public bool GetCircleState()
    {
        return appearanceTransmutationCircle.CircleShown;
    }

    public void ShowCircle()
    {
        appearanceTransmutationCircle.ImmediateCircleDisappearance();
        appearanceTransmutationCircle.CircleAppearance();
        midasConversionProcess.ManuallyActivateDelay();
    }

    public void HideCircle()
    {
        appearanceTransmutationCircle.ImmediateCircleDisappearance();
    }

    public void ClearMidasObjectsState()
    {
        foreach (Transform element in inPool)
        {
            Destroy(element.gameObject);
        }
    }

    public void ClearMaterialsObjectsState()
    {
        foreach (Transform element in materialsPool)
        {
            Destroy(element.gameObject);
        }
    }

    public void ClearCoinsObjectsState()
    {
        foreach (Transform element in coinsPool)
        {
            Destroy(element.gameObject);
        }
    }

    public void KickOutItem(int objectId, Vector3 position, Quaternion rotation)
    {
        Debug.Log(transform);


        if (objectManager.TakeObject(objectId) != null)
        {
            Transform objectReference = objectManager.TakeObject(objectId).transform;

            var newObject = Instantiate(objectReference, position, rotation);
            //newObject.gameObject.AddComponent<Rigidbody>();
            //newObject.gameObject.AddComponent<Rotate>();
            //Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = 1;

            newObject.gameObject.AddComponent<GlobalResource>();
            //newObject.gameObject.GetComponent<GlobalResource>().WasCollected = true;
            //newObject.gameObject.GetComponent<GlobalResource>().TargetLayerMask = midasLayerHolder.GetComponent<LayerMaskSettings>().TargetLayer;
            //newObject.gameObject.GetComponent<GlobalResource>().ID = objectId;
            newObject.gameObject.GetComponent<GlobalResource>().GlobalSoundManager = soundManager;
            newObject.gameObject.GetComponent<MidasResource>().Uploaded = true;
            midasCollectorCatcher.DematerializeUploaded(newObject);
            //newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * (Mathf.Cos(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) + xAngleOffset) * xForcePower);
            //newObject.gameObject.GetComponent<Rigidbody>().angularDrag = 0.75f;
            //newObject.gameObject.GetComponent<Rigidbody>().drag = 0.4f;
            //newObject.gameObject.GetComponent<Rigidbody>().AddTorque(100 * xTorque, 100 * yTorque, 100 * zTorque);
            //if (-cameraController.YRotation > 0)
            //{
            //    newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * yForcePower);
            //}
            //else { newObject.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * Mathf.Sin(Mathf.Abs(((-cameraController.YRotation) * Mathf.PI) / 180)) * -yForcePower); }

            //KickedOutItems.Add(newObject);
            //newObject.GetComponent<OreCounter>().OreCount = count;
            //Debug.Log("count was " + count);
            //newObject.parent = contactableObjectsPool;
        }

    }

    public int GetCoinsCount()
    {
        return midasCoinsCatcher.CoinsCount;
    }

    public void ApplyCoinsCount(MidasData midasData)
    {
        midasCoinsCatcher.CoinsCount = 0;
        midasCoinsCatcher.CountCoins();
        midasCoinsCatcher.CoinsCount = midasData.coinsAmmount;
        midasCoinsCatcher.CountCoins();
    }

}
