using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] SaveSystemSerialization saveSystemSerialization;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            saveSystemSerialization.LoadProgress(1);
        }
    }

    public void LoadGame(Transform buttonTransform)
    {
        Debug.Log("was loaded + " + buttonTransform);
    }

    public void LoadLastGame()
    {

    }
}
