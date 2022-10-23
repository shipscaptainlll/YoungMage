using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunesSprites : MonoBehaviour
{
    public RuneSprite[] runeSprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetSprite(string runeName)
    {
        foreach (RuneSprite runeElement in runeSprites)
        {
            if (runeName == runeElement.name) { return runeElement.sprite; }
        }
        return null;
    }
}

[System.Serializable]
public class RuneSprite
{
    public string name;
    public Sprite sprite;
}