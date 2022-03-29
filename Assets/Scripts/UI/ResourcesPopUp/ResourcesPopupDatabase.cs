using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPopupDatabase : MonoBehaviour
{
    Dictionary<int, Transform> activeDatabase = new Dictionary<int, Transform>();

    public Dictionary<int, Transform> ActiveDatabase { get { return activeDatabase; } }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfInside(int ID)
    {
        bool isInside = false;
        if (activeDatabase.ContainsKey(ID))
        {
            isInside = true;
        }
        return isInside;
    }

    public void AddToDictionary(int ID, Transform newPopupBlock)
    {
        ActiveDatabase.Add(ID, newPopupBlock);
        newPopupBlock.GetComponent<ResourcesPopupBlock>().PreparedForAutodestruction += Excomunicado;
    }

    public void Excomunicado(Transform preparedObject)
    {
        preparedObject.GetComponent<ResourcesPopupBlock>().PreparedForAutodestruction -= Excomunicado;
        ActiveDatabase.Remove(preparedObject.GetComponent<ResourcesPopupBlock>().BlockID);
        Destroy(preparedObject.gameObject);
    }
}
