using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPopupInstantiator : MonoBehaviour
{
    [SerializeField] ResourcesPopupDatabase resourcesPopupDatabase;
    [SerializeField] Transform popupBlockTemplate;
    [SerializeField] Transform parentLocaction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform InstantiatePopupBlock(int ID, int ammountAdded)
    {
        //Debug.Log("objectInstantiated");
        Transform newPopupBlock = Instantiate(popupBlockTemplate, transform.position, transform.rotation);
        newPopupBlock.gameObject.SetActive(true);
        newPopupBlock.SetParent(parentLocaction, true);
        newPopupBlock.GetComponent<ResourcesPopupBlock>().BlockID = ID;
        newPopupBlock.GetComponent<ResourcesPopupBlock>().Count = ammountAdded;
        resourcesPopupDatabase.AddToDictionary(ID, newPopupBlock);
        return newPopupBlock;
    }
}
