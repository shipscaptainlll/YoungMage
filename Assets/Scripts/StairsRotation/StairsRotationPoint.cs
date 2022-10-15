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
            
            CameraController cameraController = other.GetComponent<PersonMovement>().MainCamera.GetComponent<CameraController>();

            if (!cameraController.IsOnStairs)
            {
                cameraController.IsOnStairs = true;
                Debug.Log("Character entered");
                if (EnterType == "Upper") { cameraController.UpperStairs = true; } else { cameraController.UpperStairs = false; }
            } else
            {
                cameraController.IsOnStairs = false;
                Debug.Log("Character left");
            }
            
        }
    }
}
