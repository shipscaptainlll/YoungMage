using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneWordsDictionary : MonoBehaviour
{
    public RuneWordsSpell[] runeWordsSpells;

    void Awake()
    {
        foreach (RuneWordsSpell runeWordsSpell in runeWordsSpells)
        {
            Array.Resize(ref runeWordsSpell.spells, runeWordsSpell.maxLevels);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
