using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner1 : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;
    [SerializeField] float outlineScaleFactor;
    [SerializeField] Color outlineColor;
    [SerializeField] float rotationOffset;
    Renderer outlineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        //outlineRenderer.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, Quaternion.Euler(0,0,0), transform);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.transform.localScale = new Vector3(-1, -1, -1);
        //outlineObject.transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z));
        outlineObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        
        outlineObject.GetComponent<MeshRenderer>().enabled = false;
        outlineObject.GetComponent<Outliner>().enabled = false;
        //outlineObject.GetComponent<Collider>().enabled = false;
        

        rend.enabled = false;

        return rend;
    }
}
