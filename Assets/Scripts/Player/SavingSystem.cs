using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavingSystem
{
    public static void SavePlayer (PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.bin";
        FileStream fs = new FileStream(path, FileMode.Create);
        data.passedLevel = Mathf.Max(1, data.passedLevel);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.bin";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            PlayerData data = bf.Deserialize(fs) as PlayerData;
            fs.Close();
            //Debug.Log(data.levelPassed);
            
            return data;
        } else
        {
            SavePlayer(new PlayerData(1));
            Debug.Log("Cannot found: " + path);
            return new PlayerData();
        }
        
    }
}
