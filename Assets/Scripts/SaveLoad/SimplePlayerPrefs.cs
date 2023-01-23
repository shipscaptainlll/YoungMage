using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerPrefs : MonoBehaviour
{
    [SerializeField] GameObject unityGameObject;
    PersonMovement unit;

    private void Awake()
    {
        unit = unityGameObject.GetComponent<PersonMovement>();

        //PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        Vector3 playerPosition = unityGameObject.transform.position;
        PlayerPrefs.SetFloat("playerPositionX", playerPosition.x);
        PlayerPrefs.SetFloat("playerPositionY", playerPosition.y);
        PlayerPrefs.SetFloat("playerPositionZ", playerPosition.z);
        PlayerPrefs.Save();
        Debug.Log("found position is " + playerPosition);
    }

    void Load()
    {
        if (PlayerPrefs.HasKey("playerPositionX"))
        {
            float playerPositionX = PlayerPrefs.GetFloat("playerPositionX");
            float playerPositionY = PlayerPrefs.GetFloat("playerPositionY");
            float playerPositionZ = PlayerPrefs.GetFloat("playerPositionZ");
            Vector3 playerPosition = new Vector3(playerPositionX, playerPositionY, playerPositionZ);
            Debug.Log("saved position was " + playerPosition);
            
            unityGameObject.gameObject.SetActive(false);
            unityGameObject.transform.position = playerPosition;
            unityGameObject.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No save found, man");
        }
    }
}
