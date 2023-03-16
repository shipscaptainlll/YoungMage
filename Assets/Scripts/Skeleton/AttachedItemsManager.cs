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
    [SerializeField] SkeletonDamageManager skeletonDamageManager;
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
        skeletonDamageManager.UpdateCurrentDamage(1);
        stoneHands.gameObject.SetActive(true);
        Debug.Log("applied stone hands");
        //StartCoroutine(MaterializeItem(stoneHands, 2f));
    }

    public void DeequipStoneHands()
    {
        stoneHandsEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
        stoneHands.gameObject.SetActive(false);

    }

    public void EquipLeggings()
    {
        leggingsEquiped = true;
        skeletonDamageManager.UpdateCurrentDamage(1);
        leggings.gameObject.SetActive(true);
        Debug.Log("applied leggings");
        //StartCoroutine(MaterializeItem(leggings, 2f));
    }

    public void DeequipLeggings()
    {
        leggingsEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
        leggings.gameObject.SetActive(false);
    }

    public void EquipChainMail()
    {
        chainMailEquiped = true;
        skeletonDamageManager.UpdateCurrentDamage(1);
        chainMail.gameObject.SetActive(true);
        chainMailDecor.gameObject.SetActive(true);
        chainMailDesign.gameObject.SetActive(true);
        Debug.Log("applied chain mail");
        //StartCoroutine(MaterializeItem(chainMail, 2f));
    }

    public void DeequipChainMail()
    {
        chainMailEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
        chainMail.gameObject.SetActive(false);
        chainMailDecor.gameObject.SetActive(false);
        chainMailDesign.gameObject.SetActive(false);
    }

    public void EquipBoots()
    {
        bootsEquiped = true;
        skeletonDamageManager.UpdateCurrentDamage(1);
        boots.gameObject.SetActive(true);
        Debug.Log("applied boots");
        //StartCoroutine(MaterializeItem(boots, 2f));
    }

    public void DeequipBoots()
    {
        bootsEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
        boots.gameObject.SetActive(false);
    }

    public void EquipHelm()
    {
        helmEquiped = true;
        skeletonDamageManager.UpdateCurrentDamage(1);
        helm.gameObject.SetActive(true);
        Debug.Log("applied helm");
        //StartCoroutine(MaterializeItem(helm, 2f));
    }

    public void DeequipHelm()
    {
        helmEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
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
        skeletonDamageManager.UpdateCurrentDamage(1);
        vambrace.gameObject.SetActive(true);
        shoulders.gameObject.SetActive(true);
        Debug.Log("applied vambrace");
        //StartCoroutine(MaterializeItem(vambrace, 2f));
    }

    public void DeequipVambrace()
    {
        vambraceEquiped = false;
        skeletonDamageManager.UpdateCurrentDamage(-1);
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
