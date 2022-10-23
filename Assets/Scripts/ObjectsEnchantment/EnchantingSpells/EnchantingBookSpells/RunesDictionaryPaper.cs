using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class RunesDictionaryPaper : MonoBehaviour
{
    [SerializeField] Transform spellRunesHolder;
    [SerializeField] Transform spellRuneExample;
    [SerializeField] RunesSprites runesSprites;
    [SerializeField] RunesDictionary runesDictionary;
    [SerializeField] RunesBlinkerPaper runesBlinkerPaper;
    string[] spellRunes;

    void Start()
    {
        StringBuilder allSpells = new StringBuilder();
        for (int i = 0; i < runesDictionary.runes.Length; i++)
        {
            if (i == 0) { allSpells.Append(runesDictionary.runes[i]); }
            else { allSpells.Append(" " + runesDictionary.runes[i]); }
        }
        
        SetUpPaper(allSpells.ToString());
    }

    public void SetUpPaper(string spell)
    {
        InitializeSpellRunes(spell);
        CreateRunesHolder(spell);
    }

    void InitializeSpellRunes(string spell)
    {
        //spell = spell.ToLower();
        spellRunes = spell.Split(' ');
    }

    void CreateRunesHolder(string spell)
    {
        int holderCount = 0;
        foreach (string spellRune in spellRunes)
        {
            //Debug.Log(spellRune);
            if (spellRunesHolder.GetChild(holderCount).childCount > 4) { holderCount++; }
            Transform newSpellRune = Instantiate(spellRuneExample, spellRunesHolder.GetChild(holderCount));
            newSpellRune.name = spellRune;
            runesBlinkerPaper.runes.Add(newSpellRune);
            newSpellRune.gameObject.SetActive(true);
            newSpellRune.Find("Name").GetComponent<Text>().text = spellRune;
            newSpellRune.Find("Image").GetComponent<Image>().sprite = runesSprites.GetSprite(spellRune);
        }
    }
}
