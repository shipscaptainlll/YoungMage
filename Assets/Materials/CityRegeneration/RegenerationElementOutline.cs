using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationElementOutline : MonoBehaviour
{
    [SerializeField] RegenerationElementType elementType;
    Material firstHousesMaterial;
    Material secondHousesMaterial;
    Material wallMaterial;
    Material firstCastleMaterial;
    Material secondCastleMaterial;
    
    Color firstHousesColor;
    Color secondHousesColor;
    Color wallColor;
    Color firstCastleColor;
    Color secondCastleColor;

    Coroutine housesCoroutine;
    Coroutine wallsCoroutine;
    Coroutine castleCoroutine;
    public enum RegenerationElementType
    {
        house,
        wall,
        castle
    }

    public RegenerationElementType ElementType { get { return elementType; } }
    // Start is called before the first frame update
    void Start()
    {
        if (elementType == RegenerationElementType.house)
        {
            firstHousesMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            secondHousesMaterial = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            
            firstHousesColor = new Color(0.3773585f, 0.22f, 0);
            secondHousesColor = new Color(0.3773585f, 0.22f, 0);
        }
        else if (elementType == RegenerationElementType.wall)
        {
            wallMaterial = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            wallColor = new Color(0.3773585f, 0.22f, 0);
        }
        else if (elementType == RegenerationElementType.castle)
        {
            firstCastleMaterial = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            secondCastleMaterial = gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().sharedMaterial;
            firstCastleColor = new Color(0.3773585f, 0.22f, 0);
            secondCastleColor = new Color(0.3773585f, 0.22f, 0);
        }
        firstHousesMaterial.SetFloat("_Multiplier", 0);
        secondHousesMaterial.SetFloat("_Multiplier", 0);
        wallMaterial.SetFloat("_Multiplier", 0);
        firstCastleMaterial.SetFloat("_Multiplier", 0);
        secondCastleMaterial.SetFloat("_Multiplier", 0);
    }

    public void StartShowingOutline()
    {
        if (elementType == RegenerationElementType.house)
        {
            Transform housesHolder = gameObject.transform.parent;
            foreach (Transform house in housesHolder)
            {
                if (housesCoroutine != null) { StopCoroutine(housesCoroutine); }
                housesCoroutine = StartCoroutine(ChangeOutlineIntensity(firstHousesMaterial.GetFloat("_Multiplier"), 0.58f, 0.6f));
            }
        } else if (elementType == RegenerationElementType.wall)
        {
            if (wallsCoroutine != null) { StopCoroutine(wallsCoroutine); }
            wallsCoroutine = StartCoroutine(ChangeOutlineIntensity(wallMaterial.GetFloat("_Multiplier"), 0.58f, 0.3f));
        } else if (elementType == RegenerationElementType.castle)
        {
            if (castleCoroutine != null) { StopCoroutine(castleCoroutine); }
            castleCoroutine = StartCoroutine(ChangeOutlineIntensity(firstCastleMaterial.GetFloat("_Multiplier"), 0.58f, 0.3f));
        }
    }

    public void StopShowingOutline()
    {
        if (elementType == RegenerationElementType.house)
        {
            Transform housesHolder = gameObject.transform.parent;
            foreach (Transform house in housesHolder)
            {
                if (housesCoroutine != null) { StopCoroutine(housesCoroutine); }
                housesCoroutine = StartCoroutine(ChangeOutlineIntensity(firstHousesMaterial.GetFloat("_Multiplier"), 0, 0.4f));
            }
        }
        else if (elementType == RegenerationElementType.wall)
        {
            if (wallsCoroutine != null) { StopCoroutine(wallsCoroutine); }
            wallsCoroutine = StartCoroutine(ChangeOutlineIntensity(wallMaterial.GetFloat("_Multiplier"), 0, 0.3f));
        }
        else if (elementType == RegenerationElementType.castle)
        {
            if (castleCoroutine != null) { StopCoroutine(castleCoroutine); }
            castleCoroutine = StartCoroutine(ChangeOutlineIntensity(firstCastleMaterial.GetFloat("_Multiplier"), 0, 0.3f));
        }
    }

    IEnumerator ChangeOutlineIntensity(float startIntensity, float targetIntensity, float delay)
    {
        float elapsed = 0;
        float maxTime = delay;
        float currentIntensity;
        Material firstMaterial = null;
        Material secondMaterial = null;
        if (elementType == RegenerationElementType.house)
        {
            firstMaterial = firstHousesMaterial;
            secondMaterial = secondHousesMaterial;
        }
        else if (elementType == RegenerationElementType.wall)
        {
            firstMaterial = wallMaterial;
            secondMaterial = wallMaterial;
        }
        else if (elementType == RegenerationElementType.castle)
        {
            firstMaterial = firstCastleMaterial;
            secondMaterial = secondCastleMaterial;
        }
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            currentIntensity = Mathf.Lerp(startIntensity, targetIntensity, elapsed / maxTime);
            firstMaterial.SetFloat("_Multiplier", currentIntensity);
            secondMaterial.SetFloat("_Multiplier", currentIntensity);

            yield return null;
        }

        firstMaterial.SetFloat("_Multiplier", targetIntensity);
        secondMaterial.SetFloat("_Multiplier", targetIntensity);

        if (elementType == RegenerationElementType.house)
        {
            housesCoroutine = null;
        }
        else if (elementType == RegenerationElementType.wall)
        {
            wallsCoroutine = null;
        }
        else if (elementType == RegenerationElementType.castle)
        {
            castleCoroutine = null;
        }
    }
}
