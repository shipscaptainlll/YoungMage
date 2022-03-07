using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEVisualiser : MonoBehaviour
{
    [SerializeField] PotentialProductVisualisation potentialProductVisualisation;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    [SerializeField] EClickVariations eClickVariations;
    [SerializeField] Fade fadeEffects;
    bool isActive;

    public bool IsActive
    {
        get { return isActive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        eClickVariations.EneteredTransmutationMode += TemporarilyEnable;
        eClickVariations.LeftTransmutationMode += TemporarilyDisable;
        eClickVariations.EnteredChooseMode += JustShow;
        eClickVariations.LeftChooseMode += JustHide;
        eClickVariations.EnteredAmulet += JustShow;
        eClickVariations.LeftAmulet += JustHide;
        potentialProductVisualisation.potentialProductVisualised += EnableObject;
        potentialProductVisualisation.potentialProductUnvisualised += DisableObject;
        potentialProductAppearance.ObjectCreated += TemporarilyDisable;
        //potentialProductAppearance.ObjectTeleported += TemporarilyEnable;
    }

    void JustShow()
    {
        fadeEffects.StartEffects();
        
    }

    void JustHide()
    {
        fadeEffects.ResetEffects();

    }


    void DisableObject()
    {
        isActive = false;
        fadeEffects.ResetEffects();
    }

    void EnableObject()
    {
        //Debug.Log("Enabled");
        isActive = true;
        fadeEffects.StartEffects();
    }

    void TemporarilyDisable()
    {
        if (!eClickVariations.IsTransmutating || isActive)
        {
            fadeEffects.ResetEffects();
        }
    }

    void TemporarilyEnable()
    {
        if (eClickVariations.IsTransmutating && isActive)
        {
            fadeEffects.StartEffects();
        }
    }
}
