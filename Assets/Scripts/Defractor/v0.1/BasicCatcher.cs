using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCatcher : MonoBehaviour, ICatcher
{
    [SerializeField] string catcherTag;

    public string CatcherTag { get { return catcherTag; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
