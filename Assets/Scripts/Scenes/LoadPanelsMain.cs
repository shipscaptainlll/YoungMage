using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanelsMain : MonoBehaviour
{
    [Header("Basic")]
    //[SerializeField] SaveSystemSerialization saveSystemSerialization;
    [SerializeField] Transform loadsHolder;
    [SerializeField] LoadGameData loadGameData;
    [SerializeField] PanelsManagerMainmenu panelsManagerMainmenu;
    float currentSavesCount = 0;
    int lastSaveIndex;


    [Header("Load Settings")]
    [SerializeField] Transform loadMenuTemplate;




    // Start is called before the first frame update
    void Start()
    {
        lastSaveIndex = GetLastsaveID();
        UploadSavedGames();
    }

    public void FindSaveElement(int requiredIndex)
    {
        /*
        foreach (Transform element in savesHolder)
        {
            if (GetPanelIndex(element) == requiredIndex)
            {
                //element.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = "Time played " + ingameTimer.GetTimeIngame();
                //element.SetAsFirstSibling();
                lastSavedButton = element;
                break;
            }
        }
        */
    }

    int GetPanelIndex(Transform panel)
    {
        string loadText = panel.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text;
        string loadNumber = Regex.Match(loadText, @"\d+").Value;
        int index = Int32.Parse(loadNumber);
        return index;
    }

    public void AutoLoad()
    {
        //saveSystemSerialization.LoadProgress(saveSystemSerialization.SaveDirectoryPath);
        loadGameData.SaveId = lastSaveIndex;
        panelsManagerMainmenu.OpenNewgamePanel();
        //Debug.Log("Game was autosaved");
    }

    void UploadSavedGames()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/Saves");
        //Debug.Log("New save initialized1");
        var result = dir.GetDirectories().OrderBy(t => t.LastWriteTime).ToList();

        foreach (var element in result)
        {
            //Debug.Log(element.Name);
        }

        foreach (var element in result)
        {
            //Debug.Log("New save initialized2");
            Transform loadMenuCopy = Instantiate(loadMenuTemplate, loadsHolder.position, loadsHolder.rotation);

            loadMenuCopy.parent = loadsHolder;

            loadMenuCopy.position = loadsHolder.position;

            loadMenuCopy.rotation = loadsHolder.rotation;

            loadMenuCopy.localScale = loadMenuTemplate.localScale;

            loadMenuCopy.SetAsFirstSibling();

            RectTransform originRect = loadMenuTemplate.GetComponent<RectTransform>();

            loadMenuCopy.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);

            loadMenuCopy.GetComponent<CanvasGroup>().alpha = 1;

            currentSavesCount++;

            loadMenuCopy.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = "Save #" + element.Name;

            loadMenuCopy.name = "Load" + element.Name;
            byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/Saves/" + element.Name + "/ScreenShot.png");
            Texture2D screenShotTexture = new Texture2D(Screen.width, Screen.height);
            screenShotTexture.LoadImage(data);
            Sprite screenshotSprite = Sprite.Create(screenShotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));


            loadMenuCopy.gameObject.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;
            //Debug.Log("New save initialized");
            string path = Application.persistentDataPath + "/Saves/" + element.Name + "/gameData.fun";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSaveData gameSaveData = formatter.Deserialize(stream) as GameSaveData;

            loadMenuCopy.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = "Time played " + gameSaveData.timeInGame;
            //Debug.Log(gameSaveData.timeInGame + " we were in game");
            stream.Close();
        }
    }

    public void LoadGame(Transform buttonTransform)
    {
        string loadText = buttonTransform.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text;
        string loadNumber = Regex.Match(loadText, @"\d+").Value;
        int index = Int32.Parse(loadNumber);
        //FindSaveElement(index);
        loadGameData.SaveId = index;
        panelsManagerMainmenu.OpenNewgamePanel();
        //saveSystemSerialization.LoadProgress(index);
        //Debug.Log("was loaded + " + index);
    }

    public void LoadLastGame()
    {
        AutoLoad();
    }

    int GetLastsaveID()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/lastSave");
        if (dir.GetDirectories().Length > 0)
        {
            var result = dir.GetDirectories().OrderBy(t => t.LastWriteTime).ToList();
            Debug.Log("Last save is " + result[0].Name);
            return System.Int32.Parse(result[0].Name);
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/lastSave/-1");
            return -1;
        }

    }
}
