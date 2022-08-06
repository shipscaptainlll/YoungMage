using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCharacterCatcher : MonoBehaviour
{
    int numberOfEnters;
    bool isEntered;


    public int ProgressParameter { get { return numberOfEnters; } }
    public event Action<int> CharacterEnteredDungeon = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            if (isEntered)
            {
                isEntered = false;
            }
            else
            {
                numberOfEnters++;
                isEntered = true;
                if (CharacterEnteredDungeon != null) { CharacterEnteredDungeon(numberOfEnters); }
            }
        }
    }
}
