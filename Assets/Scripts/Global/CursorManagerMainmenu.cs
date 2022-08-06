using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagerMainmenu : MonoBehaviour
{
    [SerializeField] OpenClose inventoryOpenClose;
    [SerializeField] PanelsManagerMainmenu panelsManagerMainmenu;
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
        panelsManagerMainmenu.PanelsUpdated += CheckSomethingOpened;
        Cursor.lockState = CursorLockMode.Confined;
        somethingOpened = false;
    }

    void CheckSomethingOpened()
    {
        if (panelsManagerMainmenu.CurrentlyOpened != null)
        {
            Cursor.lockState = CursorLockMode.Confined;
            somethingOpened = true;
        }
        else { Cursor.lockState = CursorLockMode.Locked; somethingOpened = false; }
    }
}
