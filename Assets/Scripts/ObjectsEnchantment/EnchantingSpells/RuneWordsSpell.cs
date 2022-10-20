using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneWordsSpell
{
    public enum RuneSpellType
    {
        main,
        secondary,
        thirdly
    }

    public enum MainSpellType
    {
        damagePercent,
        criticalChancePercent,
        atackSpeedPercent,
        criticalAtackDamage,
        rustingRootsStack,
        doubleDamageChance
    }

    public enum SecondarySpellType
    {
        fireElement,
        earthElement,
        waterElement,
        windElement,
        lavaElement,
        magicElement,
        doubleResourceChance
    }

    public enum ThirdlySpellType
    {
        butterflies
    }

    public int maxLevels;
    public int firstLevelRunes;
    public RuneSpellType runeSpellType;
    public MainSpellType mainSpellType;
    public SecondarySpellType secondarySpellType;
    public ThirdlySpellType thirdlySpellType;
    public string[] spells;
}
