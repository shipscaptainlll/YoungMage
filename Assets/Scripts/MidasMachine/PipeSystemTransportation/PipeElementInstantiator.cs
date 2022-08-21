using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeElementInstantiator : MonoBehaviour
{
    [SerializeField] MidasPipesTransmission midasPipesTransmission;
    [SerializeField] Transform pipeObjectVariant;
    [SerializeField] Transform pipeCoinVariant;
    [SerializeField] Transform pipeSystemLibrary;
    System.Random random;
    float torqueForce = 100;
    Transform pipeObjectActual;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiatePipeObject()
    {
        pipeObjectActual = Instantiate(pipeObjectVariant, transform.position, transform.rotation);
        pipeObjectActual.parent = pipeSystemLibrary;
        pipeObjectActual.gameObject.SetActive(true);
        pipeObjectActual.GetComponent<PipeSystemElement>().MidasPipesTransmission = midasPipesTransmission;
        SetTorqueRotation();
        SetSize();
        SetAnimationSpeed();
    }

    public void InstantiateCoinObject()
    {
        pipeObjectActual = Instantiate(pipeCoinVariant, transform.position, transform.rotation);
        pipeObjectActual.parent = pipeSystemLibrary;
        pipeObjectActual.gameObject.SetActive(true);
        pipeObjectActual.GetComponent<PipeSystemElement>().MidasPipesTransmission = midasPipesTransmission;
        SetTorqueRotation();
        SetAnimationSpeed();
        StartCoroutine(MaaterializeProduct(pipeObjectActual, 1.5f));
    }

    void SetTorqueRotation()
    {
        float xRotation = (float) random.Next(-2, 2);
        float yRotation = (float) random.Next(-2, 2);
        float zRotation = (float) random.Next(-2, 2);
        pipeObjectActual.GetComponent<Rigidbody>().AddTorque(new Vector3(xRotation * torqueForce, yRotation * torqueForce, zRotation * torqueForce));
    }

    void SetSize()
    {
        float xAntiScale = ((float) random.Next(20, 100)) / 100;
        float yAntiScale = ((float) random.Next(20, 100)) / 100;
        float zAntiScale = ((float) random.Next(20, 100)) / 100;
        pipeObjectActual.localScale = new Vector3(pipeObjectActual.localScale.x * xAntiScale, pipeObjectActual.localScale.y * yAntiScale, pipeObjectActual.localScale.z * zAntiScale);
    }

    void SetAnimationSpeed()
    {
        float speedRandom = (float)random.Next(1, 4);
        pipeObjectActual.GetComponent<Animator>().speed = 1f / speedRandom;
    }

    IEnumerator MaaterializeProduct(Transform productTransform, float duration)
    {
        float elapsed = 0;
        MeshRenderer productMeshrenderer = productTransform.GetComponent<MeshRenderer>();
        Material productMaterial = productMeshrenderer.material;
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(1, 0, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0);
        productMeshrenderer.material = productMaterial;

        yield return null;
    }
}
