using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClosingInitiator : MonoBehaviour
{
    [SerializeField] MagicDoor magicDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.transform);
        if (collider.transform.GetComponent<PersonMovement>() != null)
        {
            magicDoor.CloseTheDoor();
            magicDoor.PlayerInInitiator = true;
            Debug.Log("player entered initiator");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.GetComponent<PersonMovement>() != null)
        {
            magicDoor.PlayerInInitiator = false;
            Debug.Log("player left initiator");
        }
    }
}
