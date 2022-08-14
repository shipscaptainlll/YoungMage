using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TakeScreenShot : MonoBehaviour
{
    [SerializeField] Transform saveMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //MakeScreenShot();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //MakeScreenShot();
        }
    }

    public void MakeScreenShot(GameObject referenceSavePanel, int saveId)
    {
        StartCoroutine(LoadTexture(referenceSavePanel, saveId));
    }

    IEnumerator LoadTexture(GameObject referenceSavePanel, int saveId)
    {
        saveMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return new WaitForFixedUpdate();
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png");
        
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        saveMenuPanel.GetComponent<CanvasGroup>().alpha = 1;
        byte[] data = File.ReadAllBytes(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png");
        Texture2D screenShotTexture = new Texture2D(Screen.width, Screen.height);
        screenShotTexture.LoadImage(data);
        Sprite screenshotSprite = Sprite.Create(screenShotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
        screenshotSprite.name = "SavedScreenShot" + saveId;
        referenceSavePanel.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;

        yield return null;
    }


}
