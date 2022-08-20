using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;
using UnityEngine.Events;
using System;

public class TornadoInstantiator : MonoBehaviour
{
    [SerializeField] VisualEffect tornadoVFX;
    [SerializeField] Transform body;
    [SerializeField] Vector3 offsetRotation;
    RaycastHit[] hitObjects;
    RaycastHit hit;

    int tornadoCountQuests;
    public int TornadoCountQuests { get { return tornadoCountQuests; } }
    public event Action<int> TornadoInstantiatedQuests = delegate { };
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("hello there");
            tornadoCountQuests++;
            if (TornadoInstantiatedQuests != null) { TornadoInstantiatedQuests(TornadoCountQuests); }

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
                    tornadoVFX.transform.rotation = Quaternion.Euler(body.rotation.eulerAngles + offsetRotation);
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
