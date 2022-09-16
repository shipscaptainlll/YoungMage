using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCatcher : MonoBehaviour, ICatcher
{
    [SerializeField] string catcherTag;
    [SerializeField] SoundManager soundManager;
    AudioSource teleportationSound;

    public string CatcherTag { get { return catcherTag; } }
    // Start is called before the first frame update
    void Start()
    {
        teleportationSound = soundManager.LocateAudioSource("GoingThroughPortal", transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CatchSound()
    {
        teleportationSound.Play();
        //Debug.Log("Catched");
    }
}
