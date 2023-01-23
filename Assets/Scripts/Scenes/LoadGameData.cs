using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameData : MonoBehaviour
{
    SaveSystemSerialization saveSystemSerialization;
    int saveId = 0;

    public int SaveId { get { return saveId; } set { saveId = value; } }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += UploadGameData;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UploadGameData(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene") {
            saveSystemSerialization = GameObject.Find("SaveSystemSerialization").GetComponent<SaveSystemSerialization>();
            Debug.Log("hello there" + saveSystemSerialization);
            saveSystemSerialization.LoadProgress(saveId);
        }
        
    }
}
