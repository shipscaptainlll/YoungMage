using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorResource : MonoBehaviour
{
    [SerializeField] DestroyableObjects destroyableObjects;
    SoundManager soundManager;
    AudioSource destructionSound;
    public SoundManager SoundManagerScript { set { soundManager = value; } }
    
    int id;
    GameObject destroyableVersion;

    public DestroyableObjects DestroyableObjects { set { destroyableObjects = value; } }
    public int ID { get { return id; } set { id = value; } }


    public event Action<Transform> objectContactedDefractor = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hello by " + other);
        if (other.GetComponent<RotatingCones>() != null)
        {
            Debug.Log("triggered by " + transform);
            InstantiateDestroyableVersion();
            if (objectContactedDefractor != null) { objectContactedDefractor(transform); }
            
        }
    }

    void InstantiateDestroyableVersion()
    {
        destroyableVersion = Instantiate(destroyableObjects.TakeObject(id), transform.position, transform.rotation);
        destructionSound = soundManager.LocateAudioSource("RockoreDestruction", destroyableVersion.transform);
        destructionSound.Play();
        destroyableVersion.transform.position = transform.position;
        destroyableVersion.AddComponent<DestroyableObject>();
    }
}
