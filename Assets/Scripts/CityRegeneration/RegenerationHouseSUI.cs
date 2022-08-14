using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationHouseSUI : MonoBehaviour, IRegenerationSUI
{
    [SerializeField] Transform transformSUI;

    bool isActive;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transformSUI.position = Input.mousePosition;
        }
    }

    public void Activate()
    {
        isActive = true;
        transformSUI.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Deactivate()
    {
        isActive = false;
        transformSUI.GetComponent<CanvasGroup>().alpha = 0;
        transformSUI.position = new Vector3(0, 0, 0);
    }
}
