using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePanel : MonoBehaviour
{
    [Header("Basic")]
    [SerializeField] Transform savesHolder;
    [SerializeField] Transform loadsHolder;
    [SerializeField] ClickManager clickManager;
    [SerializeField]    float savesMaximumCount;
    float currentSavesCount = 0;
    

    [Header("SaveNewGame Settings")]
    [SerializeField] Transform newSaveTemplate;
    [SerializeField] Transform loadMenuTemplate;

    [Header("Autosave Settings")]
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] float autosaveRate;
    Coroutine autosaveCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        clickManager.FFiveClicked += AutoSave;
        miscPanel.AutosaveTimeChangeRequested += SetAutosaveRate;

        autosaveCoroutine = StartCoroutine(AutosaveCounter(autosaveRate));
    }

    // Update is called once per frame
    void Update()
    {
        
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
        newSavedGame.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = "Save #" + currentSavesCount;
        loadMenuCopy.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text = "Save #" + currentSavesCount;
        newSavedGame.name = "Save" + currentSavesCount;
        loadMenuCopy.name = "Load" + currentSavesCount;


        Debug.Log("New game saved");
    }

    public void RewriteExistingSave(Transform saveButtonTransform)
    {

        Debug.Log("Game was rewritten " + saveButtonTransform);
    }

    IEnumerator AutosaveCounter(float requiredAutosaveRate)
    {
        while (true)
        {
            float elapsed = 0;
            while (elapsed < requiredAutosaveRate * 5)
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
        
        Debug.Log("Game was autosaved");
    }

    public void SetAutosaveRate(float newAutosaveRate)
    {
        StopCoroutine(autosaveCoroutine);
        autosaveCoroutine = StartCoroutine(AutosaveCounter(newAutosaveRate));
        Debug.Log("New autosave rate was setted");
    }
}
