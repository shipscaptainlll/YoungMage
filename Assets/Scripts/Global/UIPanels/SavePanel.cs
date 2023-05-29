using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] SaveSystemSerialization saveSystemSerialization;
    [SerializeField] TakeScreenShot takeScreenShot;
    [SerializeField] Transform savesHolder;
    [SerializeField] Transform loadsHolder;
    [SerializeField] ClickManager clickManager;
    [SerializeField]    float savesMaximumCount;
    [SerializeField] IngameTimer ingameTimer;
    [SerializeField] PortalOpener portalOpener;
    [SerializeField] SaveNameChanger saveNameChanger;
    [SerializeField] TurnOffVisualiser turnOffVisualiser;
    float currentSavesCount = 0;
    Transform lastSavedButton;
    public Transform LastSavedButton { get { return lastSavedButton; } }
    

    [Header("SaveNewGame Settings")]
    [SerializeField] Transform newSaveTemplate;
    [SerializeField] Transform loadMenuTemplate;

    [Header("Autosave Settings")]
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] float autosaveRate;
    Coroutine autosaveCoroutine;

    [Header("Saves Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource saveSound;

    bool isTutorialMode;
    private bool m_isTransmutationMode;

    public bool IsTutorialMode { get { return isTutorialMode; } set { isTutorialMode = value; } }
    public bool IsTransmutationMode { get { return m_isTransmutationMode; } set { m_isTransmutationMode = value; } }
    


    // Start is called before the first frame update
    void Start()
    {
        UploadSavedGames();
        saveSound = soundManager.FindSound("Save");
        clickManager.FFiveClicked += AutoSave;
        clickManager.FSixClicked += AutoLoad;
        miscPanel.AutosaveTimeChangeRequested += SetAutosaveRate;

        autosaveCoroutine = StartCoroutine(AutosaveCounter(autosaveRate));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            //saveSystemSerialization.SaveProgress();
            //AutoSave();
        }

        
    }

    public void SaveNewGame()
    {
        
        Transform newSavedGame = Instantiate(newSaveTemplate, savesHolder.position, savesHolder.rotation);
        Transform loadMenuCopy = Instantiate(loadMenuTemplate, loadsHolder.position, loadsHolder.rotation);
        newSavedGame.parent = savesHolder;
        loadMenuCopy.parent = loadsHolder;
        newSavedGame.SetAsFirstSibling();
        loadMenuCopy.SetAsFirstSibling();
        newSavedGame.localScale = new Vector3(1, 1, 1);
        loadMenuCopy.localScale = new Vector3(1, 1, 1);
        RectTransform originRect = newSaveTemplate.GetComponent<RectTransform>();
        newSavedGame.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
        loadMenuCopy.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
        newSavedGame.GetComponent<CanvasGroup>().alpha = 1;
        loadMenuCopy.GetComponent<CanvasGroup>().alpha = 1;

        currentSavesCount++;
        
        
        takeScreenShot.MakeScreenShot(newSavedGame.gameObject, loadMenuCopy.gameObject, (int) currentSavesCount, -1);
        newSavedGame.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + ingameTimer.GetTimeIngame();
        loadMenuCopy.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + ingameTimer.GetTimeIngame();
        saveSystemSerialization.SaveProgress(false);

        newSavedGame.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = " " + currentSavesCount;
        loadMenuCopy.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = " " + currentSavesCount;
        newSavedGame.name = "Save" + currentSavesCount;
        loadMenuCopy.name = "Load" + currentSavesCount;

        lastSavedButton = newSavedGame;
        //Debug.Log("New game saved");
    }

    public void FindSaveElement(int requiredIndex)
    {
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
    }


    public void RewriteExistingSave(Transform saveButtonTransform)
    {
        lastSavedButton = saveButtonTransform;
        int requiredIndex = GetPanelIndex(saveButtonTransform);
        Transform savePanel = null;
        Transform loadPanel = null;
        foreach (Transform element in savesHolder)
        {
            if (GetPanelIndex(element) == requiredIndex)
            {
                element.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + ingameTimer.GetTimeIngame();
                element.SetAsFirstSibling();
                savePanel = element;
                break;
            }
        }

        foreach (Transform element in loadsHolder)
        {
            if (GetPanelIndex(element) == requiredIndex)
            {
                element.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + ingameTimer.GetTimeIngame();
                element.SetAsFirstSibling();
                loadPanel = element;
                break;
            }
        }

        takeScreenShot.MakeScreenShot(savePanel.gameObject, loadPanel.gameObject, (int)requiredIndex, -1);

        saveSystemSerialization.ResaveProgress(requiredIndex);
        //Debug.Log("Game was rewritten " + saveButtonTransform);
    }

    int GetPanelIndex(Transform panel)
    {
        string loadText = panel.name;
        string loadNumber = Regex.Match(loadText, @"\d+").Value;
        int index = Int32.Parse(loadNumber);
        return index;
    }

    IEnumerator AutosaveCounter(float requiredAutosaveRate)
    {
        while (true)
        {
            float elapsed = 0;
            while (elapsed < requiredAutosaveRate * 50)
            {
                elapsed++;
                yield return new WaitForSeconds(1f);
            }
            AutoSave();
            yield return null;
        }
    }

    public void AutoSave()
    {
        if (portalOpener.PortalOpened || isTutorialMode)
        {
            turnOffVisualiser.JustShow();
            return;
        }
        saveSound.Play();
        if (saveSystemSerialization.NeverSaved)
        {
            SaveNewGame();
            saveSystemSerialization.NeverSaved = false;
            return;
        }
        if (lastSavedButton != null )
        {
            RewriteExistingSave(lastSavedButton);
        } else
        {
            SaveNewGame();
        }
        //saveSystemSerialization.AutoSaveProgress();
        //Debug.Log("Game was autosaved " + saveSystemSerialization.SaveDirectoryPath);
    }

    public void AutoLoad()
    {
        if (isTutorialMode)
        {
            turnOffVisualiser.JustShow();
            return;
        }
        saveSound.Play();
        saveSystemSerialization.LoadProgress(saveSystemSerialization.SaveDirectoryPath);
        //Debug.Log("Game was autosaved");
    }

    public void SetAutosaveRate(float newAutosaveRate)
    {
        StopCoroutine(autosaveCoroutine);
        autosaveCoroutine = StartCoroutine(AutosaveCounter(newAutosaveRate));
        //Debug.Log("New autosave rate was setted");
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
            Transform newSavedGame = Instantiate(newSaveTemplate, savesHolder.position, savesHolder.rotation);
            Transform loadMenuCopy = Instantiate(loadMenuTemplate, loadsHolder.position, loadsHolder.rotation);
            lastSavedButton = newSavedGame;
            newSavedGame.parent = savesHolder;
            loadMenuCopy.parent = loadsHolder;
            newSavedGame.SetAsFirstSibling();
            loadMenuCopy.SetAsFirstSibling();
            newSavedGame.localScale = new Vector3(1, 1, 1);
            loadMenuCopy.localScale = new Vector3(1, 1, 1);
            RectTransform originRect = newSaveTemplate.GetComponent<RectTransform>();
            newSavedGame.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
            loadMenuCopy.GetComponent<RectTransform>().sizeDelta = new Vector2(originRect.rect.width, originRect.rect.height);
            newSavedGame.GetComponent<CanvasGroup>().alpha = 1;
            loadMenuCopy.GetComponent<CanvasGroup>().alpha = 1;

            currentSavesCount++;
            newSavedGame.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = " " + element.Name;
            loadMenuCopy.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = " " + element.Name;
            newSavedGame.name = "Save" + element.Name;
            loadMenuCopy.name = "Load" + element.Name;
            byte[] data = File.ReadAllBytes(Application.persistentDataPath + "/Saves/" + element.Name + "/ScreenShot.png");
            Texture2D screenShotTexture = new Texture2D(Screen.width, Screen.height);
            screenShotTexture.LoadImage(data);
            Sprite screenshotSprite = Sprite.Create(screenShotTexture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));


            newSavedGame.gameObject.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;
            loadMenuCopy.gameObject.transform.Find("Borders").Find("Image").GetComponent<Image>().sprite = screenshotSprite;
            //Debug.Log("New save initialized");
            string path = Application.persistentDataPath + "/Saves/" + element.Name + "/gameData.fun";
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSaveData gameSaveData = formatter.Deserialize(stream) as GameSaveData;
            
            newSavedGame.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + gameSaveData.timeInGame;
            loadMenuCopy.Find("Content").Find("TimePlayed").Find("Text").GetComponent<Text>().text = " " + gameSaveData.timeInGame;
            //Debug.Log(gameSaveData.timeInGame + " we were in game");
            stream.Close();
        }
    }
}
