using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacketMagnetDatabase : MonoBehaviour
{
    [SerializeField] SacketFollowerVector sacketFollowerVector;
    List<Transform> contactedObjects = new List<Transform>();
    bool active;

    public bool Active { get { return active; } set { active = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(active);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (active && other.gameObject.layer == 14)
        {
            Debug.Log(" enteered ");
            contactedObjects.Add(other.transform);
            sacketFollowerVector.objectHello = other.transform;
            foreach (Transform element in contactedObjects)
            {
                Debug.Log(element);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (active && other.gameObject.layer == 14)
        {
            Debug.Log(" leaved ");
            contactedObjects.Remove(other.transform);
        }
    }

    public void ResetLists()
    {
        active = false;
        foreach(Transform element in contactedObjects)
        {
            //element.GetComponent<GlobalResource>().ResetMagneticField();
        }
        contactedObjects.Clear();
    }
}
