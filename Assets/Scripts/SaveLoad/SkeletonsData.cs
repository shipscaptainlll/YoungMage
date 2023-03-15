using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkeletonData
{
    public float[][] positions;
    public float[][] rotations;
    public bool[] connectedToOre;
    public bool[] connectedToPerson;
    public bool[] isIdle;
    public bool[] isExcomunicated;
    public bool[] isTacklingDoor;
    public int[] indexerOreSaved;
    public int[] connectedDoorIndex;
    public string[] skeletonType;
    public bool[] equippedStoneHands;
    public bool[] equippedLeggings;
    public bool[] equippedPlateArmor;
    public bool[] equippedShoes;
    public bool[] equippedHelm;
    public bool[] equippedGloves;
    public bool[] equippedBracers;

    public SkeletonData(Transform skeletonsHolder, Transform oresHolder)
    {
        GetPositions(skeletonsHolder);
        GetRotations(skeletonsHolder);
        GetState(skeletonsHolder, oresHolder);
        GetSkeletonsEquipment(skeletonsHolder);
    }

    void GetPositions(Transform skeletonsHolder)
    {
        positions = new float[skeletonsHolder.childCount][];
        int indexer = 0;
        foreach (Transform skeleton in skeletonsHolder)
        {
            float[] position = new float[3];
            position[0] = skeleton.gameObject.transform.position.x;
            position[1] = skeleton.gameObject.transform.position.y;
            position[2] = skeleton.gameObject.transform.position.z;
            positions[indexer++] = position;
            Debug.Log("saved one skeleton positions");
        }
    }

    void GetRotations(Transform skeletonsHolder)
    {
        rotations = new float[skeletonsHolder.childCount][];
        int indexer = 0;
        foreach (Transform skeleton in skeletonsHolder)
        {
            float[] rotation = new float[3];
            rotation[0] = skeleton.gameObject.transform.eulerAngles.x;
            rotation[1] = skeleton.gameObject.transform.eulerAngles.y;
            rotation[2] = skeleton.gameObject.transform.eulerAngles.z;
            rotations[indexer++] = rotation;
            Debug.Log("saved one skeleton rotations");
        }
    }

    void GetState(Transform skeletonsHolder,Transform oresHolder)
    {
        
        int indexer = 0;
        connectedToOre = new bool[skeletonsHolder.childCount];
        connectedToPerson = new bool[skeletonsHolder.childCount];
        isIdle = new bool[skeletonsHolder.childCount];
        isExcomunicated = new bool[skeletonsHolder.childCount];
        isTacklingDoor = new bool[skeletonsHolder.childCount];
        indexerOreSaved = new int[skeletonsHolder.childCount];
        connectedDoorIndex = new int[skeletonsHolder.childCount];
        skeletonType = new string[skeletonsHolder.childCount];
        foreach (Transform skeleton in skeletonsHolder)
        {
            SkeletonBehavior loadedSkeletonBehavior = skeleton.GetComponent<SkeletonBehavior>();

            connectedToOre[indexer] = false;
            connectedToPerson[indexer] = false;
            isIdle[indexer] = false;
            isExcomunicated[indexer] = false;
            isTacklingDoor[indexer] = false;
            indexerOreSaved[indexer] = 0;
            skeletonType[indexer] = GetSkeletonType(loadedSkeletonBehavior);

            if (!loadedSkeletonBehavior.IsConjured)
            {
                isIdle[indexer] = true;
                Debug.Log("skeleton was idle");
            } else if (loadedSkeletonBehavior.NavigationTarget != null && loadedSkeletonBehavior.NavigationTarget.GetComponent<PersonMovement>() != null)
            {
                Debug.Log("skeleton was following mage");
                connectedToPerson[indexer] = true;
            } else if (loadedSkeletonBehavior.NavigationTarget != null && loadedSkeletonBehavior.NavigationTarget.GetComponent<IOre>() != null)
            {
                Debug.Log("skeleton was ore connected");
                connectedToOre[indexer] = true;
                int indexerOre = 0;
                foreach (Transform row in oresHolder)
                {
                    foreach (Transform ore in row)
                    {
                        if (loadedSkeletonBehavior.NavigationTarget == ore.GetChild(1))
                        {
                            indexerOreSaved[indexer] = indexerOre;
                            Debug.Log("saved one skeleton state");
                            Debug.Log("ore number was " + indexerOre);
                            return;
                        }
                        indexerOre++;
                    }
                }
            } else if (loadedSkeletonBehavior.NavigationTarget != null && loadedSkeletonBehavior.NavigationTarget.parent.name == "SkeletonPositions")
            {
                Debug.Log("skeleton was tackling door");
                isTacklingDoor[indexer] = true;

                connectedDoorIndex[indexer] = loadedSkeletonBehavior.NavigationTarget.parent.parent.GetComponent<DoorTacklingManager>().DoorLevel;
            } else if (loadedSkeletonBehavior.BeingUnconjured)
            {
                Debug.Log("skeleton was unconjured");
                isExcomunicated[indexer] = true;
            }
            indexer++;
            Debug.Log("saved one skeleton state");
        }
    }

    string GetSkeletonType(SkeletonBehavior skeletonBehavior)
    {
        Debug.Log("our " + skeletonBehavior.transform + " skeleton is small " + skeletonBehavior.IsSmallSkeleton + " our skeleton is big " + skeletonBehavior.IsBigSkeleton + " our skeleton is lizard " + skeletonBehavior.IsLizardSkeleton);
        if (skeletonBehavior.IsSmallSkeleton)
        {
            return "smallSkeleton";
        } else if (skeletonBehavior.IsBigSkeleton)
        {
            return "bigSkeleton";
        } else if (skeletonBehavior.IsLizardSkeleton)
        {
            return "lizardSkeleton";
        } else
        {
            return "";
        }
    }

    void GetSkeletonsEquipment(Transform skeletonsHolder)
    {
        equippedStoneHands = new bool[skeletonsHolder.childCount];
        equippedLeggings = new bool[skeletonsHolder.childCount];
        equippedPlateArmor = new bool[skeletonsHolder.childCount];
        equippedShoes = new bool[skeletonsHolder.childCount];
        equippedHelm = new bool[skeletonsHolder.childCount];
        equippedGloves = new bool[skeletonsHolder.childCount];
        equippedBracers = new bool[skeletonsHolder.childCount];
        int indexer = 0;
        foreach (Transform skeleton in skeletonsHolder)
        {
            AttachedItemsManager attachedItemsManager = skeleton.GetComponent<AttachedItemsManager>();
            equippedStoneHands[indexer] = attachedItemsManager.StoneHandsEquiped;
            equippedLeggings[indexer] = attachedItemsManager.LeggingsEquiped;
            equippedPlateArmor[indexer] = attachedItemsManager.ChainMailEquiped;
            equippedShoes[indexer] = attachedItemsManager.BootsEquiped;
            equippedHelm[indexer] = attachedItemsManager.HelmEquiped;
            equippedGloves[indexer] = attachedItemsManager.GlovesEquiped;
            equippedBracers[indexer] = attachedItemsManager.VambraceEquiped;

            indexer++;

            Debug.Log("saved one skeleton equipment");
        }
    }

}
