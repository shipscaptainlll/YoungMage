using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPopupUpdater : MonoBehaviour
{
    [SerializeField] ResourcesPopupDatabase resourcesPopupDatabase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePopupBlock(int ID, int ammountAdded)
    {
        //Debug.Log("ObjectUpdated");
        resourcesPopupDatabase.ActiveDatabase[ID].GetComponent<ResourcesPopupBlock>().Count += ammountAdded;
    }
}
