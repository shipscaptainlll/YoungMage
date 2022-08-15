using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationPanelsOpener : MonoBehaviour
{
    [SerializeField] Transform regenerationWallPanel;
    [SerializeField] Transform regenerationCastlePanel;
    [SerializeField] Transform regenerationBlacksmithPanel;

    Transform activeTransform;
    RegenerationElementOutline.RegenerationElementType activeType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnalyseClickedElement(Transform clickedObject)
    {
        if (clickedObject != null)
        {
            RegenerationElementOutline.RegenerationElementType encounteredType = clickedObject.GetComponent<RegenerationElementOutline>().ElementType;

            if (encounteredType != activeType)
            {
                if (activeTransform != null)
                {
                    HideObject(activeTransform);
                }

                switch (encounteredType)
                {

                    case RegenerationElementOutline.RegenerationElementType.wall:
                        ShowObject(regenerationWallPanel);
                        SaveActivePanel(regenerationWallPanel);
                        break;
                    case RegenerationElementOutline.RegenerationElementType.castle:
                        ShowObject(regenerationCastlePanel);
                        SaveActivePanel(regenerationCastlePanel);
                        break;
                    case RegenerationElementOutline.RegenerationElementType.house:
                        ShowObject(regenerationBlacksmithPanel);
                        SaveActivePanel(regenerationBlacksmithPanel);
                        break;

                }
            }
        }
    }

    void HideObject(Transform panel)
    {
        panel.localPosition = new Vector3(2300, 100, 0);
        panel.GetComponent<CanvasGroup>().alpha = 0;
    }

    void ShowObject(Transform panel)
    {
        panel.localPosition = new Vector3(750, 100, 0);
        panel.GetComponent<CanvasGroup>().alpha = 1;
    }

    void SaveActivePanel(Transform panel)
    {
        activeTransform = panel;
    }
}
