using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverSoundElement : MonoBehaviour
{
    [SerializeField] ElementType elementType;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource onHoverSound;


    enum ElementType
    {
        normalElement,
        subTable
    }

    // Start is called before the first frame update
    void Start()
    {
        if (elementType == ElementType.normalElement) { onHoverSound = soundManager.FindSound("OnNormalElement"); }
        else if (elementType == ElementType.subTable) { onHoverSound = soundManager.FindSound("SettingMainChange"); }
    }

    public void StartSound()
    {
        //Debug.Log("Found this one" + transform);
        onHoverSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
