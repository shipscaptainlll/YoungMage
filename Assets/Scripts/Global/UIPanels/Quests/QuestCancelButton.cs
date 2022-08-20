using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCancelButton : MonoBehaviour
{

    bool isActive;
    Image cancelButtonBorders;
    Transform cancelImageBorders;

    QuestElement currentQuestElement;

    public QuestElement CurrentQuestElement { get { return currentQuestElement; } set { currentQuestElement = value; } }
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        cancelButtonBorders = transform.GetComponent<Image>();
        cancelImageBorders = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateQuestCancelation()
    {
        if (isActive)
        {
            DeactivateButton();
            CancelQuest();
            RestartCancelButton();
            Debug.Log("quest has been canceled");
        }
    }

    void CancelQuest()
    {
        currentQuestElement.StopQuest();
    }

    void RestartCancelButton()
    {
        StartCoroutine(InitiateTimerRefill(5, 1));
    }

    IEnumerator InitiateTimerRefill(float delay, float targetValue)
    {
        float elapsed = 0;
        float currentValue = 0;
        cancelImageBorders.GetComponent<CanvasGroup>().alpha = 0;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentValue = Mathf.Lerp(0, targetValue, elapsed / delay);
            cancelButtonBorders.fillAmount = currentValue;
            
            yield return null;
        }
        cancelButtonBorders.fillAmount = targetValue;
        cancelImageBorders.GetComponent<CanvasGroup>().alpha = 1;
        ActivateButton();
        yield return null;
    }

    void ActivateButton()
    {
        isActive = true;
    }

    void DeactivateButton()
    {
        isActive = false;
    }
}
