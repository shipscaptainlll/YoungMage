using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpellsNotifier : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] BookSpellsCaster bookSpellsCaster;
    [SerializeField] CameraController cameraController;
    [SerializeField] QuickAccessHandController quickAccessHandController;

    Transform currentObject;
    // Start is called before the first frame update
    void Start()
    {
        quickAccessHandController.ObjectHandsChanged += EvaluateObject;
    }

    void Update()
    {
        DetectNewObjects();
    }

    void DetectNewObjects()
    {
        if (currentObject != cameraController.ObservedObject.transform) { currentObject = cameraController.ObservedObject.transform; EvaluateObject();  }
    }

    void EvaluateObject()
    {
        //Debug.Log(currentObject);
        if (currentObject == null)
        {
            if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        } else if (currentObject.GetComponent<IOre>() != null)
        {
            if (SkeletonConnected() && BookInHands()) { CastMiningSpell(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.parent != null && currentObject.parent.GetComponent<IOre>() != null)
        {
            if (SkeletonConnected() && BookInHands()) { CastMiningSpell(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.name == "MinesDoor")
        {
            if (SkeletonConnected() && BookInHands()) { CastMiningSpell(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.GetComponent<Skeleton>() != null)
        {
            //Debug.Log(currentObject + "skeleton1");
            if (BookInHands()) { CastContactSkeleton(); }
            else if (ItemInHands()) { CastApplyObject(); }
            else if (ObjectInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.parent != null && currentObject.parent.GetComponent<Skeleton>() != null)
        {
            //Debug.Log(currentObject + "skeleton");
            if (BookInHands()) { CastContactSkeleton(); }
            else if (ItemInHands()) { CastApplyObject(); }
            else if (ObjectInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.GetComponent<SkeletonItem>() != null)
        {
            if (BookInHands() || ItemInHands()) { CastApplyObject(); }
            else if (ObjectInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.parent != null && currentObject.parent.GetComponent<Defractor>() != null)
        {
            if (BookInHands()) { CastContactMachinery(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.parent != null && currentObject.parent.GetComponent<MidasCauldron>() != null)
        {
            if (BookInHands()) { CastContactMachinery(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.GetComponent<TransmutationResourceChoose>() != null || currentObject.GetComponent<AlchemistPotentialProduct>() != null || currentObject.GetComponent<TransmutationResourcePack>() != null)
        {
            Debug.Log("hello there seeing transmutation");
            if (BookInHands()) { CastContactMachinery(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.GetComponent<CityRegenerationEnter>() != null)
        {
            if (BookInHands()) { CastContactMachinery(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }
        else if (currentObject.GetComponent<GlobalResource>() != null)
        {
            Debug.Log("found resource");
            Debug.Log(BookInHands());
            if (BookInHands()) { CastTornadoSpell(); }
            else if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        } else
        {
            if (ObjectInHands() || ItemInHands()) { CastThrowObject(); } else { CastIdle(); }
        }

    }

    void CastMiningSpell()
    {
        bookSpellsCaster.CastSpell("Mining");
    }

    void CastContactSkeleton()
    {
        bookSpellsCaster.CastSpell("ContactSkeleton");
    }

    void CastContactMachinery()
    {
        bookSpellsCaster.CastSpell("ContactMachinery");
    }

    void CastApplyObject()
    {
        bookSpellsCaster.CastSpell("ApplyObject");
    }

    void CastThrowObject()
    {
        bookSpellsCaster.CastSpell("ThrowObject");
    }

    void CastTornadoSpell()
    {
        bookSpellsCaster.CastSpell("Tornado");
    }

    void CastIdle()
    {
        bookSpellsCaster.CastSpell("NullSpell");
    }

    bool BookInHands()
    {
        return (quickAccessHandController.CurrentCustomID == 10);
    }

    bool ItemInHands()
    {
        return (quickAccessHandController.CurrentCustomID == 11
            || quickAccessHandController.CurrentCustomID == 12
            || quickAccessHandController.CurrentCustomID == 13
            || quickAccessHandController.CurrentCustomID == 14
            || quickAccessHandController.CurrentCustomID == 15
            || quickAccessHandController.CurrentCustomID == 16
            || quickAccessHandController.CurrentCustomID == 17);
    }

    bool ObjectInHands()
    {
        return (quickAccessHandController.CurrentCustomID == 1
            || quickAccessHandController.CurrentCustomID == 2
            || quickAccessHandController.CurrentCustomID == 3
            || quickAccessHandController.CurrentCustomID == 4
            || quickAccessHandController.CurrentCustomID == 5
            || quickAccessHandController.CurrentCustomID == 6
            || quickAccessHandController.CurrentCustomID == 7
            || quickAccessHandController.CurrentCustomID == 8
            || quickAccessHandController.CurrentCustomID == 9
            || quickAccessHandController.CurrentCustomID == 18
            || quickAccessHandController.CurrentCustomID == 19
            || quickAccessHandController.CurrentCustomID == 20
            || quickAccessHandController.CurrentCustomID == 21
            || quickAccessHandController.CurrentCustomID == 22
            || quickAccessHandController.CurrentCustomID == 23
            || quickAccessHandController.CurrentCustomID == 24 
            || quickAccessHandController.CurrentCustomID == 25);
    }

    bool SkeletonConnected()
    {
        return (contactManager.ContactedSkeleton != null);
    }
}
