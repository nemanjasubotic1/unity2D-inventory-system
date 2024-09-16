using System;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string dataDirPath = String.Empty;
    private string fileName = string.Empty;

    public FileDataHandler(string dataDirPath, string fileName)
    {
        this.dataDirPath = dataDirPath;
        this.fileName = fileName;
    }

    public void SaveGame(GameData gameData)
    {
        string fullPath = Path.Combine(dataDirPath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToSave = JsonUtility.ToJson(gameData);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                using StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(dataToSave);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public GameData LoadGame()
    {
        string fullPath = Path.Combine(dataDirPath, fileName);

        GameData gameData = null;

        try
        {
            if (File.Exists(fullPath))
            {
                string dataToLoad = String.Empty;

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    using StreamReader streamReader = new StreamReader(fileStream);
                    dataToLoad = streamReader.ReadToEnd();
                }

                gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        return gameData;
    }

}
