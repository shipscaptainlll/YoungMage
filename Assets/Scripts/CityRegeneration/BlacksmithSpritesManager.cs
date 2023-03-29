using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithSpritesManager : MonoBehaviour
{
    [SerializeField] Sprite woodenWall;
    [SerializeField] Sprite metalWall;
    [SerializeField] Sprite spikedWall;

    public static Dictionary<int, Sprite> WallDefenceSprites = new Dictionary<int, Sprite>();
    void Awake()
    {
        FillDictionary();
    }

    public static Sprite GetSprite(int itemID)
    {
        return WallDefenceSprites[itemID];
    }

    void FillDictionary()
    {
        WallDefenceSprites.Add(1, woodenWall);
        WallDefenceSprites.Add(2, metalWall);
        WallDefenceSprites.Add(3, spikedWall);
    }
}
