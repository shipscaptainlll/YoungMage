using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoadRememberer : MonoBehaviour
{
    public static MainMenuLoadRememberer instance;
    public int nextLoadedID;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckGameSceneLoaded(scene);
    }

    public void CheckGameSceneLoaded(Scene scene)
    {
        Debug.Log("just loaded scene " + scene.name);
    }

}
