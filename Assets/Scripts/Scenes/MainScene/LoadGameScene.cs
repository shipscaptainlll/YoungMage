using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    [SerializeField] Transform progressCanvas;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject backgroundEnvironment;
    float targetFill;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, targetFill, Time.deltaTime);
    }

    void FixedUpdate()
    {
        
    }

    public async void ShowGameScene()
    {
        backgroundEnvironment.SetActive(false);
        progressCanvas.GetComponent<CanvasGroup>().alpha = 1.0f;
        Debug.Log("starting there");
        var loadedScene = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
        loadedScene.allowSceneActivation = false;
        progressCanvas.gameObject.SetActive(true);
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        do
        {
            await Task.Delay(100);
            targetFill = loadedScene.progress;
        }
        while (loadedScene.progress < 0.9f);

        loadedScene.allowSceneActivation = true;

        
    }
}
