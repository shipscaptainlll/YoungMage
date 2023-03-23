using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            CombineMeshes();
        }
    }

    void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        var meshFilter = transform.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine);
        //GetComponent<MeshCollider>().sharedMesh = meshFilter.mesh;
        transform.gameObject.SetActive(true);

        //transform.localScale = new Vector3(1, 1, 1);
        //transform.rotation = Quaternion.identity;
        //transform.position = Vector3.zero;
    }
}
