using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] Sprite nothing;
    [SerializeField] Sprite goldCoins;

    
    void Start()
    {
        
    }

    public Sprite TakeSprite(int customeID)
    {
        switch (customeID)
        {
            case 0:
                return nothing;
            case 1:
                return goldCoins;
        }
        return nothing;
    }

}
