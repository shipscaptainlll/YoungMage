using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningInitiator : MonoBehaviour
{
    [SerializeField] DoorOpener door;

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.transform);
        if (collider.transform.GetComponent<PersonMovement>() != null)
        {
            door.OpenTheDoor();
            //Debug.Log("player entered initiator");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.GetComponent<PersonMovement>() != null)
        {
            Debug.Log("player left initiator");
        }
    }
}
