using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System;

public class RuneWordsCreator : MonoBehaviour
{
    [SerializeField] RunesDictionary runesDictionary;
    [SerializeField] RuneWordsDictionary runeWordsDictionary;

    System.Random rand;

    public event Action RuneWordsCreated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        foreach (RuneWordsSpell runeWordsSpell in runeWordsDictionary.runeWordsSpells)
        {
            //Debug.Log(runeWordsSpell.mainSpellType);
            if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.main)
            {
                //Debug.Log(runeWordsSpell.mainSpellType);
            } else if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.secondary)
            {
                //Debug.Log(runeWordsSpell.secondarySpellType);
            }
            else if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.thirdly)
            {
                //Debug.Log(runeWordsSpell.thirdlySpellType);
            }
        }
        CreateRuneWords();
        //ShowSpellsDevelopers();
    }

    void CreateRuneWords()
    {
        foreach (RuneWordsSpell runeWordsSpell in runeWordsDictionary.runeWordsSpells)
        {
            for (int i = 0; i < runeWordsSpell.maxLevels; i++)
            {
                string newSpell = GetRandomRunes((i + 1) * runeWordsSpell.firstLevelRunes);
                runeWordsSpell.spells[i] = newSpell;
            }
        }
        if (RuneWordsCreated != null) { RuneWordsCreated(); }
    }

    string GetRandomRunes(int runesNumber)
    {
        StringBuilder wordFull = new StringBuilder();
        for (int i = 0; i < runesNumber; i++)
        {
            string newRune = runesDictionary.runes[rand.Next(0, runesDictionary.runes.Length)];
            if (i == 0)
            {
                wordFull.Append(newRune);
            } else { wordFull.Append(" " + newRune); }
            
        }
        return wordFull.ToString();
    }

    void ShowSpellsDevelopers()
    {
        foreach (RuneWordsSpell runeWordsSpell in runeWordsDictionary.runeWordsSpells)
        {
            if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.main)
            {
                Debug.Log("Spell type: " + runeWordsSpell.runeSpellType + " " + runeWordsSpell.mainSpellType);
                Debug.Log("Array size: " + runeWordsSpell.spells.Length);
            }
            else if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.secondary)
            {
                Debug.Log("Spell type: " + runeWordsSpell.runeSpellType + " " + runeWordsSpell.secondarySpellType);
                Debug.Log("Array size: " + runeWordsSpell.spells.Length);
            }
            else if (runeWordsSpell.runeSpellType == RuneWordsSpell.RuneSpellType.thirdly)
            {
                Debug.Log("Spell type: " + runeWordsSpell.runeSpellType + " " + runeWordsSpell.thirdlySpellType);
                Debug.Log("Array size: " + runeWordsSpell.spells.Length);
            }
            for (int i = 0; i < runeWordsSpell.spells.Length; i++)
            {
                Debug.Log((i + 1) + " " + runeWordsSpell.spells[i]);
            }
        }
    }
}
