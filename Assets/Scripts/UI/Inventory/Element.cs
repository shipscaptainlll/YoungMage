using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [SerializeField] ItemsList itemsList;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    Transform attachedCounter;
    Text textBox;

    public int CustomID
    {
        get
        {
            return customID;
        }

        set
        {
            customID = value;
            updateElement();
        }
    }

    public Transform AttachedCounter
    {
        get
        {
            return attachedCounter;
        }
        set
        {
            if (attachedCounter != null)
            {
                attachedCounter.GetComponent<ICounter>().AmountChanged -= updateCounter;
            }
            attachedCounter = value;
            if (attachedCounter != null)
            {
                attachedCounter.GetComponent<ICounter>().AmountChanged += updateCounter;
            }
        }
    }



    void Start()
    {
        //nullCustomID = 0;
        //customID = nullCustomID;
        attachedCounter = null;
        textBox = transform.parent.Find("AmountCounter").GetComponent<Text>();
    }

    void updateElement()
    {
        updateImage();
    }

    void updateImage()
    {
        transform.GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
    }

    void updateCounter(int count)
    {
        textBox.text = count.ToString();
    }
}
