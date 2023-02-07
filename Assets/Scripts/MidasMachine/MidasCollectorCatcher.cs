using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MidasCollectorCatcher : MonoBehaviour
{
    [SerializeField] Material dematerializeMaterial;
    [SerializeField] SoundManager soundManager;
    [SerializeField] Transform incomeObjectsPool;
    AudioSource dissolvingSound;
    AudioSource waterFallinSound;

    System.Random rand;
    public event Action<int> ResourceEnteredCollector = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MidasResource>() != null && !other.GetComponent<MidasResource>().BeingDissolved)
        {
            Debug.Log("Catched " + other);
            other.GetComponent<MidasResource>().BeingDissolved = true;
            ApplySounds(other.transform);
            int resourceID = other.GetComponent<GlobalResource>().ID;
            if (ResourceEnteredCollector != null) { ResourceEnteredCollector(resourceID); }
            other.GetComponent<ConnectableResource>().DissolvingDestruction();
            other.GetComponent<MidasResource>().DissolvingDestruction();
            other.transform.Find("LevitationPS").gameObject.SetActive(false);
            other.transform.parent = incomeObjectsPool;
            StartCoroutine(DematerializeProduct(other.transform, 4));
            Debug.Log(other);
            if (other.transform.GetChild(0).GetComponent<OreLevitator>() != null) { other.transform.GetChild(0).GetComponent<OreLevitator>().DeactivateLevitation(); }
        }
    }

    void ApplySounds(Transform appliedToObject)
    {
        int random = rand.Next(1, 4);
        if (random == 1) { waterFallinSound = soundManager.LocateAudioSource("WaterFallinSoundFirst", appliedToObject); }
        else if (random == 2) { waterFallinSound = soundManager.LocateAudioSource("WaterFallinSoundSecond", appliedToObject); }
        else if (random > 2) { waterFallinSound = soundManager.LocateAudioSource("WaterFallinSoundThird", appliedToObject); }
        dissolvingSound = soundManager.LocateAudioSource("DissolvingObject", appliedToObject);
        dissolvingSound.Play();
        waterFallinSound.Play();
    }

    public void DematerializeUploaded(Transform uploadedOre)
    {
        uploadedOre.GetComponent<MidasResource>().BeingDissolved = true;
        ApplySounds(uploadedOre.transform);
        uploadedOre.GetComponent<ConnectableResource>().DissolvingDestruction();
        uploadedOre.GetComponent<MidasResource>().DissolvingDestruction();
        uploadedOre.Find("LevitationPS").gameObject.SetActive(false);
        uploadedOre.transform.parent = incomeObjectsPool;
        StartCoroutine(DematerializeProduct(uploadedOre, 4));
        if (uploadedOre.transform.GetChild(0).GetComponent<OreLevitator>() != null) { uploadedOre.transform.GetChild(0).GetComponent<OreLevitator>().DeactivateLevitation(); }
        
    }

    IEnumerator DematerializeProduct(Transform productTransform, float duration)
    {
        //productTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.1f, 0);
        //productTransform.GetComponent<Rigidbody>().useGravity = false;
        
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
