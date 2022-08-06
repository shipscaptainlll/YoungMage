using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSUI : MonoBehaviour, IObjectSUI
{
    [SerializeField] string objectName;
    [SerializeField] Sprite objectImage;
    [SerializeField] string objectType;
    [SerializeField] int count;
    public string ObjectName { get { return objectName; } }
    public Sprite ObjectImage { get { return objectImage; } }
    public string ObjectType { get { return objectType; } }
    public int Count { get { return count; } set { count = value; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
