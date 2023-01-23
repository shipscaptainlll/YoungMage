using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectsInstantiator : MonoBehaviour
{
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform collectableObjectsPool;
    [SerializeField] SacketClickController sacketClickController;
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
    }

    public void InstantiateLoadedCollectable(int objectId, Vector3 position, Quaternion rotation, int count)
    {
        sacketClickController.KickOutItem(objectId, position, rotation, count);
        /*
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);

        Transform objectReference = objectManager.TakeObject(objectId).transform;
        Debug.Log("id was " + objectId);

        var newObject = Instantiate(objectReference, position, rotation);

        newObject.gameObject.AddComponent<Rigidbody>();
        newObject.gameObject.AddComponent<Rotate>();
        Rotate rotator = newObject.gameObject.GetComponent<Rotate>(); rotator.RotationSpeed = 10; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = 1;

        newObject.GetComponent<OreCounter>().OreCount = count;
        Debug.Log("count was " + count);
        newObject.parent = collectableObjectsPool;
        */

    }
}
