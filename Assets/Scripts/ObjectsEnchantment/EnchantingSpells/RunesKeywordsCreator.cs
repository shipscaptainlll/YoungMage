using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class RunesKeywordsCreator : MonoBehaviour
{
    [SerializeField] VoiceReader voiceReader;
    [SerializeField] RunesDictionary runesDictionary;

    public void InitializeKeywordsVoicereader()
    {
        
        //string[] permutations = combineSpellWords(runesDictionary.runes);

        

        foreach (string spell in runesDictionary.runes)
        {
            Debug.Log(spell);
            voiceReader.Actions.Add(spell, 1);
        }
        Debug.Log(voiceReader.Actions.Count);
    }


    string[] combineSpellWords(string[] initialSpells)
    {
        string[] allCombinations = new string[0];
        for (int i = 0; i < initialSpells.Length; i++)
        {
            string[] permutations = GetStringPermutations(GetPermutationsWithRept<string>(initialSpells, i + 1), i + 1);
            int originalLength = allCombinations.Length;
            Array.Resize(ref allCombinations, allCombinations.Length + permutations.Length);
            Array.Copy(permutations, 0, allCombinations, originalLength, permutations.Length);
        }
        return allCombinations;
    }

    static IEnumerable<IEnumerable<T>>
    GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });
        return GetPermutationsWithRept(list, length - 1)
            .SelectMany(t => list,
            (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    string[] GetStringPermutations(IEnumerable<IEnumerable<string>> list, int length)
    {
        string[] combinations = new string[list.ToArray().Length];
        for (int i = 0; i < list.ToArray().Length; i++)
        {
            StringBuilder wordFull = new StringBuilder();
            var newArray = list.ToArray();
            var anotherArray = newArray[i].ToArray();
            for (int j = 0; j < length; j++)
            {
                string newRune = anotherArray.GetValue(j).ToString();
                if (j == 0)
                {
                    wordFull.Append(newRune);
                }
                else { wordFull.Append(" " + newRune); }

            }
            combinations[i] = wordFull.ToString();
        }
        return combinations;
    }

}
