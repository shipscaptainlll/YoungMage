using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TakeScreenShot : MonoBehaviour
{
    [SerializeField] Transform saveMenuPanel;
    [SerializeField] SaveSystemSerialization saveSystemSerialization;
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

    public void MakeScreenShot(GameObject referenceSavePanel, GameObject referenceLoadPanel, int saveId, int mainSaveId)
    {
        StartCoroutine(LoadTexture(referenceSavePanel, referenceLoadPanel, saveId, mainSaveId));
    }

    IEnumerator LoadTexture(GameObject referenceSavePanel, GameObject referenceLoadPanel, int saveId, int mainSaveId)
    {
        saveMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return new WaitForFixedUpdate();
        if (File.Exists(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png"))
        {
            File.Delete(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png");
        }

        if (File.Exists(Application.persistentDataPath + "/Saves/" + saveSystemSerialization.SaveDirectoryPath + "/ScreenShot.png"))
        {
            File.Delete(Application.persistentDataPath + "/Saves/" + saveSystemSerialization.SaveDirectoryPath + "/ScreenShot.png");
        }
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png");
        
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;
        yield return 0;

        
        
        saveMenuPanel.GetComponent<CanvasGroup>().alpha = 1;
        byte[] data = File.ReadAllBytes(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png");
        File.Move(Application.dataPath + "/Resources/Screenshots/SavedGames/" + "SavedScreenShot" + saveId + ".png", Application.persistentDataPath + "/Saves/" + saveSystemSerialization.SaveDirectoryPath + "/ScreenShot.png");
        Texture2D screenShotTexture = new Texture2D(Screen.width, Screen.height);
        screenShotTexture.LoadImage(data);
        Sprite screenshotSprite = Sprite.Create(screenShotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));

        if (mainSaveId != -1)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/Saves");
            foreach (var element in dir.GetFiles())
            {
                if (element.Name == mainSaveId.ToString())
                {
                    Texture2D cacheTexture = screenshotSprite.texture;
                    byte[] cacheByteArray = cacheTexture.EncodeToPNG();
                    File.WriteAllBytes(Application.persistentDataPath + "/Saves" + "/" + element.Name + "/savePNG.pang", cacheByteArray);
                }
            }
        }
        
        screenshotSprite.name = "SavedScreenShot" + saveId;
        referenceSavePanel.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;
        referenceLoadPanel.gameObject.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;

        yield return null;
    }


}
