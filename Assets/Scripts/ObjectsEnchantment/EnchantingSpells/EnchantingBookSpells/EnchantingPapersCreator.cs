using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnchantingPapersCreator : MonoBehaviour
{
    [SerializeField] RuneWordsDictionary runeWordsDictionary;
    [SerializeField] RuneWordsCreator runeWordsCreator;
    [SerializeField] Transform papersHolder;
    [SerializeField] Transform paperExample;

    List<Transform> papers = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        //runeWordsCreator.RuneWordsCreated += InstantiatePapers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiatePapers()
    {
        Debug.Log(runeWordsDictionary.runeWordsSpells.Length);
        foreach (RuneWordsSpell spellObject in runeWordsDictionary.runeWordsSpells)
        {
            foreach (string spell in spellObject.spells)
            {
                InstantiatePaper(spellObject, spell);
            }
        }
    }

    void InstantiatePaper(RuneWordsSpell spellObject, string spell)
    {
        Transform newPaper = Instantiate(paperExample, papersHolder.transform);
        newPaper.gameObject.SetActive(true);
        newPaper.position = paperExample.position + new Vector3(0, 0, papersHolder.childCount);
        newPaper.GetComponent<EnchantingPaper>().SetUpPaper(spellObject, spell);
    }
}
