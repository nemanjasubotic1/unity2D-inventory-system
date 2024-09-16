using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    [SerializeField] private string fileName;

    private FileDataHandler fileDataHandler;
    private GameData gameData;
    private List<ISavebleData> savebleDatas;

    private void Awake()
    {
        fileDataHandler = new FileDataHandler(@"C:\Users\Rajko\Desktop\Platformer2DMoja", fileName);

        savebleDatas = GetSavebleDatas();
    }

    private void Start()
    {
        LoadGame();
    }

    private void NewGame()
    {
        gameData = new GameData();
    }

    private void LoadGame()
    {
        gameData = fileDataHandler.LoadGame();

        if (gameData == null)
        {
            NewGame();
        }
        else
        {
            foreach (ISavebleData savebleData in savebleDatas)
            {
                savebleData.LoadGame(gameData);
            }
        }
    }

    private void SaveGame()
    {
        foreach (ISavebleData savebleData in savebleDatas)
        {
            savebleData.SaveGame(ref gameData);
        }

        fileDataHandler.SaveGame(gameData);
    }

    private List<ISavebleData> GetSavebleDatas()
    {
        IEnumerable<ISavebleData> savebleDatas = FindObjectsOfType<MonoBehaviour>().OfType<ISavebleData>();

        return new List<ISavebleData>(savebleDatas);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

}
