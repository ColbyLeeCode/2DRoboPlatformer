using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadData : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.gm;
        //Debug.Log(gameManager.GetCurrentLevel());
    }

    public void Save()
    {
        if(!Directory.Exists(Application.dataPath + "SavedGames"))
         Directory.CreateDirectory(Application.dataPath + "/SavedGames");

        BinaryFormatter bf = new BinaryFormatter();

        FileStream fs = File.Create(Application.dataPath + "/SavedGames/save.rd");

        SaveManager saveManager = new SaveManager();

        saveManager.currentLevel = gameManager.GetCurrentLevel();       

        bf.Serialize(fs, saveManager);

        Debug.Log("Saved Current Level: " + gameManager.GetCurrentLevel());

        fs.Close();

        
    }

    public void LoadData()
    {
        if (File.Exists(Application.dataPath + "/SavedGames/save.rd"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream fs = File.Open(Application.dataPath + "/SavedGames/save.rd", FileMode.Open);

            SaveManager saveManager = (SaveManager)bf.Deserialize(fs);

            fs.Close();

            gameManager.LoadLastSave(saveManager.currentLevel);
        }

        if (!File.Exists(Application.dataPath + "/SavedGames/save.rd"))
            Debug.Log("No save file found!");


    }
}

[System.Serializable]
class SaveManager
{
    public int currentLevel;
}

