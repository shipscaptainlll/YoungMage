using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnchantingPaper : MonoBehaviour
{
    [SerializeField] Transform spellRunesHolder;
    [SerializeField] Transform spellRuneExample;
    [SerializeField] RunesSprites runesSprites;
    string[] spellRunes;


    public void SetUpPaper(RuneWordsSpell spellObject, string spell)
    {
        InitializeSpellRunes(spell);
        CreateRunesHolder();
    }

    void InitializeSpellRunes(string spell)
    {
        //spell = spell.ToLower();
        spellRunes = spell.Split(' ');
    }

    void CreateRunesHolder()
    {
        int holderCount = 0;
        foreach (string spellRune in spellRunes)
        {
            //Debug.Log(spellRune);
            if (spellRunesHolder.GetChild(holderCount).childCount > 4) { holderCount++; }
            Transform newSpellRune = Instantiate(spellRuneExample, spellRunesHolder.GetChild(holderCount));
            newSpellRune.Find("Name").GetComponent<Text>().text = spellRune;
            newSpellRune.Find("Image").GetComponent<Image>().sprite = runesSprites.GetSprite(spellRune);
        }
    }
}
