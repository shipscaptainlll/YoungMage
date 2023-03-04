using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Linq;

public class SaveSystemSerialization : MonoBehaviour
{
    [SerializeField] PersonMovement mainCharacterScript;
    [SerializeField] CameraController mainCharacterCamera;
    [SerializeField] SavePanel savePanel;
    [SerializeField] Transform timeHolder;
    [SerializeField] Transform oresHolder;
    [SerializeField] Transform countersHolder;
    [SerializeField] Transform mainInventoryHolder;
    [SerializeField] Transform innerQuickaccessHolder;
    [SerializeField] Transform outerQuickaccessHolder;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] Transform skeletonsIndoorHolder;
    [SerializeField] Transform outerSmallSkeletonsHolder;
    [SerializeField] SkeletonHouseInstantiator skeletonHouseInstantiator;
    [SerializeField] Transform tackledDoor;
    [SerializeField] Transform collectablesHolder;
    [SerializeField] CollectableObjectsInstantiator collectableObjectsInstantiator;
    [SerializeField] CollectableObjectsDeleter collectableObjectsDeleter;
    [SerializeField] DefractorStateMachine defractorStateMachine;
    [SerializeField] MidasStateMachine midasStateMachine;
    [SerializeField] CityUpgradeStateMachine cityUpgradeStateMachine;
    [SerializeField] DoorsStateMachine doorsStateMachine;
    [SerializeField] SkeletonsDeleter skeletonsDeleter;
    [SerializeField] CastlePositionsManager castlePositionsManager;
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    int saveDirectoryPath;
    string gameDataPath;
    string playerPath;
    string orePath;
    string countersPath;
    string inventoryPath;
    string indoorSkeletonsPath;
    string collectablePath;
    string defractorDataPath;
    string midasDataPath;
    string cityUpgradeDataPath;
    string doorsDataPath;
    string outerSmallSkeletonsPath;
    string arrowCatapultsPath;
    string stoneCatapultsPath;
    string outdoorSkeletonsPath;
    
    int maxId;
    bool neverSaved = false;

    public bool NeverSaved { get { return neverSaved; } set { neverSaved = value; } }

    public int SaveDirectoryPath { get { return saveDirectoryPath; } set { saveDirectoryPath = value; } }

    private void Awake()
    {
        InstantiateSavingDirectories();
    }

    private void Start()
    {
        saveDirectoryPath = GetLastsaveID();
        maxId = GetNextID();
        //Debug.Log("Max id is " + maxId);
        UpdatePaths();

    }

    public void AutoSaveProgress()
    {
        SaveProgress(true);
    }

    public void ResaveProgress(int saveId)
    {
        RenameLastsaveID(saveId);
        saveDirectoryPath = saveId;
        UpdatePaths();
        SaveProgress(true);
    }

    public void SaveProgress(bool autosaving)
    {
        if (saveDirectoryPath != -1)
        {
            if (autosaving)
            {

            }
            else
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/Saves/" + maxId);
                RenameLastsaveID();
                maxId = GetNextID();
                UpdatePaths();
                Debug.Log("created new game save ");
            }
        }

        if (saveDirectoryPath == -1)
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/" + maxId);
            RenameLastsaveID();
            maxId = GetNextID();
            UpdatePaths();
            Debug.Log("save directory path was -1 ");
        }

        //Debug.Log("currently updating " + saveDirectoryPath);

        //savePanel.AutosaveGame(saveDirectoryPath);

        GameDataSaver.SaveGameData(timeHolder, gameDataPath);

        PlayerDataSaver.SavePlayerData(mainCharacterScript, mainCharacterCamera, playerPath);
        
        OreDataSaver.SaveOreData(oresHolder, orePath);
        
        ItemsCounterDataSaver.SaveItemsData(countersHolder, countersPath);
        
        InventoryDataSaver.SaveInventoryData(mainInventoryHolder, innerQuickaccessHolder, outerQuickaccessHolder, quickAccessHandController, inventoryPath);
        
        SkeletonsDataSaver.SaveSkeletonData(skeletonsIndoorHolder, oresHolder, indoorSkeletonsPath);

        OuterSmallSkeletonsDataSaver.SaveSkeletonData(outerSmallSkeletonsHolder, outerSmallSkeletonsPath);

        CollectableDataSaver.SaveCollectableData(collectablesHolder, collectablePath);

        DefractorDataSaver.SaveDefractorData(defractorStateMachine, defractorDataPath);

        MidasDataSaver.SaveMidasData(midasStateMachine, midasDataPath);

        CityUpgradeDataSaver.SaveCityUpgradeData(cityUpgradeStateMachine, cityUpgradeDataPath);

        DoorsDataSaver.SaveDoorsData(doorsStateMachine, doorsDataPath);

        //Debug.Log("game was saved");
    }

    public void LoadProgress(int saveId)
    {
        if (saveId == -1 && saveDirectoryPath != -1)
        {
            LoadingProgress();
        } else if (saveId > 0 && saveDirectoryPath != -1)
        {
            int cacheDirectoryPath = saveDirectoryPath;
            saveDirectoryPath = saveId;
            UpdatePaths();

            LoadingProgress();

            saveDirectoryPath = saveId;
            UpdatePaths();
        } else
        {
            neverSaved = true;
            Debug.Log("Never saved is true");
        }
    }

    void LoadingProgress()
    {
        DoorsDataApplier.ApplyDoorsData(doorsStateMachine, DoorsDataSaver.LoadDoorsData(doorsDataPath));

        GameDataApplier.ApplyGameData(timeHolder, GameDataSaver.LoadGameData(gameDataPath));

        PlayerDataApplier.ApplyPlayerData(mainCharacterScript, mainCharacterCamera, PlayerDataSaver.LoadPlayerData(playerPath));

        OreDataApplier.ApplyOreData(oresHolder, OreDataSaver.LoadOreData(orePath));
        StartCoroutine(LoadGameDelayed());
    }

    IEnumerator LoadGameDelayed()
    {
        yield return new WaitForSeconds(0.05f);

        ItemsCounterDataApplier.ApplyCountersData(countersHolder, ItemsCounterDataSaver.LoadItemsData(countersPath));

        InventoryDataApplier.ApplyInventoryData(mainInventoryHolder, innerQuickaccessHolder, outerQuickaccessHolder, quickAccessHandController, InventoryDataSaver.LoadInventoryData(inventoryPath));

        SkeletonsDataApplier.ApplySkeletonsData(skeletonsIndoorHolder, skeletonHouseInstantiator, mainCharacterScript, oresHolder, tackledDoor, SkeletonsDataSaver.LoadSkeletonData(indoorSkeletonsPath), skeletonsDeleter);

        OuterSmallSkeletonsDataApplier.ApplySkeletonsData(outerSmallSkeletonsHolder, OuterSmallSkeletonsDataSaver.LoadSkeletonData(outerSmallSkeletonsPath), skeletonsDeleter, castlePositionsManager, skeletonArenaInstantiator);

        CollectableDataApplier.ApplyCollectableData(collectableObjectsDeleter, collectableObjectsInstantiator, CollectableDataSaver.LoadCollectableData(collectablePath));

        DefractorDataApplier.ApplyDefractorData(defractorStateMachine, DefractorDataSaver.LoadDefractorData(defractorDataPath));

        MidasDataDataApplier.ApplyMidasData(midasStateMachine, MidasDataSaver.LoadMidasData(midasDataPath));

        CityUpgradeDataApplier.ApplyCityUpgradeData(cityUpgradeStateMachine, CityUpgradeDataSaver.LoadCityUpgradeData(cityUpgradeDataPath));

        

        Debug.Log("game was loaded");
    }

    int GetLastsaveID()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/lastSave");
        if (dir.GetDirectories().Length > 0)
        {
            var result = dir.GetDirectories().OrderBy(t => t.LastWriteTime).ToList();
            //Debug.Log("Last save is " + result[0].Name);
            return System.Int32.Parse(result[0].Name);
        } else
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/lastSave/-1");
            return -1;
        }
        
    }

    void RenameLastsaveID()
    {
        Directory.Move(Application.persistentDataPath + "/lastSave" + "/" + saveDirectoryPath, Application.persistentDataPath + "/lastSave" + "/" + maxId);
        saveDirectoryPath = maxId;
    }

    void RenameLastsaveID(int newId)
    {
        if (saveDirectoryPath == newId)
        {
            return;
        }
        Directory.Move(Application.persistentDataPath + "/lastSave" + "/" + saveDirectoryPath, Application.persistentDataPath + "/lastSave" + "/" + newId);
        saveDirectoryPath = newId;
    }

    void UpdatePaths()
    {
        gameDataPath = GetPath("gameData");
        playerPath = GetPath("player");
        orePath = GetPath("ore");
        countersPath = GetPath("counters");
        inventoryPath = GetPath("inventory");
        indoorSkeletonsPath = GetPath("indoorSkeleton");
        collectablePath = GetPath("collectable");
        defractorDataPath = GetPath("defractorData");
        arrowCatapultsPath = GetPath("arrowCatapult");
        stoneCatapultsPath = GetPath("stoneCatapult");
        outdoorSkeletonsPath = GetPath("outdoorSkeleton");
        midasDataPath = GetPath("midasDataPath");
        cityUpgradeDataPath = GetPath("cityUpgradeDataPath");
        doorsDataPath = GetPath("doorsDataPath");
        outerSmallSkeletonsPath = GetPath("outerSmallSkeletonsPath");
    }

    string GetPath(string subName)
    {
        string path = Application.persistentDataPath + "/Saves/" + "/" + saveDirectoryPath + "/" + subName + ".fun";
        return path;
    }

    int GetNextID()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/Saves");
        int maxId = 0;
        foreach (var element in dir.GetDirectories())
        {
            int foundId = System.Int32.Parse(element.Name.ToString());
            if (foundId > maxId)
            {
                maxId = foundId;
            }
            //Debug.Log(foundId);
        }
        return maxId + 1;
    }

    void InstantiateSavingDirectories()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Application.persistentDataPath + "/lastSave");
        if (!Directory.Exists(Application.persistentDataPath + "/lastSave"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/lastSave");
        } else
        {
            //Debug.Log("Directory: lastSave already exists ");
        }
    }
}
