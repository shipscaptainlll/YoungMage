using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUINotificator : MonoBehaviour
{
    [SerializeField] Transform UIHolder;
    [SerializeField] Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Notify(string description)
    {
        Transform newNotification = Instantiate(this.transform);
        newNotification.parent = UIHolder;
        newNotification.position = Input.mousePosition + new Vector3(250, 40, 0);
        newNotification.GetChild(0).GetChild(0).GetComponent<Text>().text = description;
        StartCoroutine(MoveNotification(newNotification, 0, 100, 0.4f));
        
    }

    IEnumerator MoveNotification(Transform element, float startPosition, float endPosition, float delay)
    {
        float elapsed = delay;
        float targetTime = 3;
        Vector3 yStartPosition = element.position;
        float yPosition = startPosition;
        while (elapsed < targetTime)
        {
            elapsed += Time.deltaTime;
            yPosition = Mathf.Lerp(startPosition, endPosition, elapsed / targetTime);
            element.position = yStartPosition + new Vector3(0, yPosition, 0);
            yield return null;
        }
        element.position = yStartPosition + new Vector3(0, endPosition, 0);
        HideNotification(element);
    }

    void HideNotification(Transform element)
    {
        Destroy(element.gameObject);
    }
}
