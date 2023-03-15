using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachedItemsManager : MonoBehaviour
{
    [SerializeField] Transform stoneHands;
    [SerializeField] Transform leggings;
    [SerializeField] Transform chainMail;
    [SerializeField] Transform chainMailDecor;
    [SerializeField] Transform chainMailDesign;
    [SerializeField] Transform boots;
    [SerializeField] Transform helm;
    [SerializeField] Transform gloves;
    [SerializeField] Transform shoulders;
    [SerializeField] Transform vambrace;
    bool stoneHandsEquiped;
    bool leggingsEquiped;
    bool chainMailEquiped;
    bool bootsEquiped;
    bool helmEquiped;
    bool glovesEquiped;
    bool vambraceEquiped;

    public bool StoneHandsEquiped { get { return stoneHandsEquiped; } }
    public bool LeggingsEquiped { get { return leggingsEquiped; } }
    public bool ChainMailEquiped { get { return chainMailEquiped; } }
    public bool BootsEquiped { get { return bootsEquiped; } }
    public bool HelmEquiped { get { return helmEquiped; } }
    public bool GlovesEquiped { get { return glovesEquiped; } }
    public bool VambraceEquiped { get { return vambraceEquiped; } }


    public void EquipStoneHands()
    {
        stoneHandsEquiped = true;
        stoneHands.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(stoneHands, 2f));
    }

    public void DeequipStoneHands()
    {
        stoneHandsEquiped = false;
        stoneHands.gameObject.SetActive(false);

    }

    public void EquipLeggings()
    {
        leggingsEquiped = true;
        leggings.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(leggings, 2f));
    }

    public void DeequipLeggings()
    {
        leggingsEquiped = false;
        leggings.gameObject.SetActive(false);
    }

    public void EquipChainMail()
    {
        chainMailEquiped = true;
        chainMail.gameObject.SetActive(true);
        chainMailDecor.gameObject.SetActive(true);
        chainMailDesign.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(chainMail, 2f));
    }

    public void DeequipChainMail()
    {
        chainMailEquiped = false;
        chainMail.gameObject.SetActive(false);
        chainMailDecor.gameObject.SetActive(false);
        chainMailDesign.gameObject.SetActive(false);
    }

    public void EquipBoots()
    {
        bootsEquiped = true;
        boots.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(boots, 2f));
    }

    public void DeequipBoots()
    {
        bootsEquiped = false;
        boots.gameObject.SetActive(false);
    }

    public void EquipHelm()
    {
        helmEquiped = true;
        helm.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(helm, 2f));
    }

    public void DeequipHelm()
    {
        helmEquiped = false;
        helm.gameObject.SetActive(false);
    }

    public void EquipGloves()
    {
        //glovesEquiped = true;
        //gloves.gameObject.SetActive(true);
    }

    public void DeequipGloves()
    {
        //glovesEquiped = false;
        //gloves.gameObject.SetActive(false);
    }

    public void EquipVambrace()
    {
        vambraceEquiped = true;
        vambrace.gameObject.SetActive(true);
        shoulders.gameObject.SetActive(true);
        //StartCoroutine(MaterializeItem(vambrace, 2f));
    }

    public void DeequipVambrace()
    {
        vambraceEquiped = false;
        vambrace.gameObject.SetActive(false);
        shoulders.gameObject.SetActive(false);
    }

    IEnumerator MaterializeItem(Transform productTransform, float duration)
    {
        //productTransform.GetChild(0).GetComponent<SkeletonItem>().BeingEdited = true;
        float elapsed = 0;
        SkinnedMeshRenderer productMeshrenderer = productTransform.GetComponent<SkinnedMeshRenderer>();
        Material productMaterial = productMeshrenderer.material;
        productMaterial.SetFloat("_Clip", 0.7f);
        productMeshrenderer.material = productMaterial;
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(0.7f, 0.05f, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0f);
        productMeshrenderer.material = productMaterial;
        yield return null;
    }

}
