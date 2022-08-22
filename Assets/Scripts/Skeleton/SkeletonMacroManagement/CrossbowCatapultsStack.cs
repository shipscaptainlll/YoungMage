using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowCatapultsStack : MonoBehaviour
{
    [SerializeField] CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator;
    List<Transform> catapultsStack = new List<Transform>();//in field


    public List<Transform> CatapultsStack { get { return catapultsStack; } }
    // Start is called before the first frame update
    void Start()
    {
        crossbowCatapultArenaInstantiator.CatapultInstantiated += SaveCatapult;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveCatapult(Transform newCatapult)
    {
        catapultsStack.Add(newCatapult);

    }

    void DeleteCatapult(Transform deletedCatapult)
    {
        catapultsStack.Remove(deletedCatapult);
        Debug.Log("Count in lists: " + catapultsStack.Count);
        Destroy(deletedCatapult.gameObject);
    }
}
