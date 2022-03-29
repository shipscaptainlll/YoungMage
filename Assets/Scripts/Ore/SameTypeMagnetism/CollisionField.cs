using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionField : MonoBehaviour
{
    int id;

    public int ID { get { return id; } set { id = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent?.parent?.parent?.GetComponent<GlobalResource>() != null && other.transform.parent?.parent?.parent?.GetComponent<GlobalResource>().ID == id)
        {
            Debug.Log("hello there");
            Destroy(other.transform.parent.parent.parent.transform);
            Destroy(this.gameObject);
        }
    }
}
