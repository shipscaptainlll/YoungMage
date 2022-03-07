using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseResourceVisualizer : MonoBehaviour
{
    List<GameObject> _accessibleResources;

    // Start is called before the first frame update
    void Start()
    {
        //InitializeAccessibleResources();
    }

    // Update is called once per frame
    void Update()
    {
        //RotateResources();
    }

    void RotateResources()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }

    void InitializeAccessibleResources()
    {
        _accessibleResources.Clear();
        foreach (Transform resource in transform)
        {
            _accessibleResources.Add(resource.gameObject);
        }
    }
}
