using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MidasCollectorCatcher : MonoBehaviour
{
    [SerializeField] Material dematerializeMaterial;

    public event Action<int> ResourceEnteredCollector = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MidasResource>() != null)
        {
            int resourceID = other.GetComponent<GlobalResource>().ID;
            if (ResourceEnteredCollector != null) { ResourceEnteredCollector(resourceID); }
            StartCoroutine(DematerializeProduct(other.transform, 2));
        }
    }

    IEnumerator DematerializeProduct(Transform productTransform, float duration)
    {
        productTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.1f, 0);
        productTransform.GetComponent<Rigidbody>().useGravity = false;
        
        float elapsed = 0;
        MeshRenderer productMeshrenderer = productTransform.GetChild(0).GetComponent<MeshRenderer>();
        MeshRenderer secondProductMeshrenderer = productTransform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
        Material productMaterial = productMeshrenderer.material;
        Material secondProductMaterial = secondProductMeshrenderer.material;
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(0, 1, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            secondProductMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            secondProductMeshrenderer.material = secondProductMaterial;
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 1);
        secondProductMaterial.SetFloat("_Clip", 1);
        productMeshrenderer.material = productMaterial;
        secondProductMeshrenderer.material = secondProductMaterial;
        Destroy(productTransform.gameObject);
        
        yield return null;
    }
}
