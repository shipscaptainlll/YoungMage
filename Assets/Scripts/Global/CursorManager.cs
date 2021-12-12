using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] OpenClose inventoryOpenClose;
    bool somethingOpened;

    public bool SomethingOpened
    {
        get
        {
            return somethingOpened;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        somethingOpened = false;
    }

    private void Update()
    {
        if (CheckSomethingOpened())
        {
            Cursor.lockState = CursorLockMode.Confined;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public bool CheckSomethingOpened()
    {
        if (inventoryOpenClose.IsOpened)
        {
            somethingOpened = true;
            return true;
        } else { 
            somethingOpened = false; 
            return false; }
    }
}
