using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsActivator : MonoBehaviour
{
    [SerializeField] BookSpellsCaster bookSpellsCaster;

    void Update()
    {
        
    }

    public void CastMiningSpell()
    {
        if (bookSpellsCaster.CurrentSpell == "Mining") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastContactSkeleton()
    {
        //Debug.Log(bookSpellsCaster.CurrentSpell);
        if (bookSpellsCaster.CurrentSpell == "ContactSkeleton") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastContactMachinery()
    {
        if (bookSpellsCaster.CurrentSpell == "ContactMachinery") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastApplyObject()
    {
        if (bookSpellsCaster.CurrentSpell == "ApplyObject") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastThrowObject()
    {
        if (bookSpellsCaster.CurrentSpell == "ThrowObject") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastTornadoSpell()
    {
        if (bookSpellsCaster.CurrentSpell == "Tornado") { bookSpellsCaster.ActivateSpell(); }
    }

    public void CastNullSpell()
    {
        if (bookSpellsCaster.CurrentSpell == "NullSpell") { bookSpellsCaster.ActivateSpell(); }
    }

}
