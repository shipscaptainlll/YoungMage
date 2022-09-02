using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsRotationPoint : MonoBehaviour
{
    [SerializeField] string EnterType;
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
        //Debug.Log(other);
        if (other.GetComponent<PersonMovement>() != null)
        {
            //Debug.Log("Character entered");
            CameraController cameraController = other.transform.Find("Main Camera").GetComponent<CameraController>();
            if (!cameraController.IsOnStairs)
            {
                cameraController.IsOnStairs = true;
                if (EnterType == "Upper") { cameraController.UpperStairs = true; } else { cameraController.UpperStairs = false; }
            } else
            {
                cameraController.IsOnStairs = false;
            }
            
        }
    }
}
