using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorProduct : MonoBehaviour
{
    Transform countersHolder;
    int id;

    public int ID { get { return id; } set { id = value; } }
    public Transform CountersHolder { set { countersHolder = value; } }
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
        if (other.GetComponent<ICatcher>() != null && other.GetComponent<ICatcher>().CatcherTag == "DefractoringLine")
        {
            Destroy(gameObject);
        }
        if (other.GetComponent<ICatcher>() != null && other.GetComponent<ICatcher>().CatcherTag == "OutletLine")
        {
            //Debug.Log(countersHolder);
            foreach (Transform element in countersHolder)
            {
                if (element.GetComponent<ICounter>().ID == id)
                {
                    element.GetComponent<ICounter>().AddResource(1);
                    //Debug.Log("Aded to counter some resource " + element);
                }
            }
            Destroy(gameObject);
        }
    }
}
