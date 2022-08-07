using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class TornadoInstantiator : MonoBehaviour
{
    [SerializeField] VisualEffect tornadoVFX;
    RaycastHit[] hitObjects;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            hitObjects = Physics.RaycastAll(transform.position, transform.forward);
            foreach (var result in hitObjects)
            {
                if (result.transform.gameObject.layer == 6 && result.transform.name != "Terrain")
                {
                    tornadoVFX.transform.gameObject.SetActive(true);
                    tornadoVFX.Play();
                    return;
                }
            }
        }

        if (Input.GetKey(KeyCode.Y))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hitObjects = Physics.RaycastAll(transform.position, transform.forward);
            foreach (var result in hitObjects)
            {
                if (result.transform.gameObject.layer == 6 && result.transform.name != "Terrain")
                {
                    //Debug.Log(result.transform);
                    tornadoVFX.transform.position = result.point + new Vector3(0, 0.5f, 0);
                    return;
                } else { tornadoVFX.transform.position = tornadoVFX.transform.position; }
            }
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            tornadoVFX.gameObject.SetActive(false);

        }
    }
}
