using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameData : MonoBehaviour
{
    SaveSystemSerialization saveSystemSerialization;
    public static LoadGameData instance;
    int saveId = 0;

    public int SaveId { get { return saveId; } set { saveId = value; } }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        SceneManager.sceneLoaded += UploadGameData;
    }

    void UploadGameData(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene") {
            saveSystemSerialization = GameObject.Find("SaveSystemSerialization").GetComponent<SaveSystemSerialization>();
            Debug.Log("hello there" + saveSystemSerialization);
            Debug.Log("just loaded load number " + saveId);
            saveSystemSerialization.LoadProgress(saveId);
        }
        
    }
}
