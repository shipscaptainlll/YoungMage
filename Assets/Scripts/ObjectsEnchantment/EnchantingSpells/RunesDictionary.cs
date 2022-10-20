using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RunesDictionary : MonoBehaviour
{
    public string[] runes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (string word in runes)
        {
            //Debug.Log(word);
        }

        StringBuilder wordFull = new StringBuilder();
        foreach (string word in runes)
        {
            //wordFull.Append(" " + word);
        }
        //Debug.Log(wordFull);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
