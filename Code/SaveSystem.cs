using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class SaveSystem
{
  public static void SavePlayer(PlayerHealth player)
    {
        BinaryFormatter binary = new BinaryFormatter();
        string path = Application.persistentDataPath + "/starWarsCon";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        binary.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/starWarsCon";
        if(File.Exists(path))
        {
            PlayerHealth playerHealth = new PlayerHealth();
            BinaryFormatter binary = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = binary.Deserialize(stream) as PlayerData;
            PlayerData datahealth = new PlayerData(playerHealth);
            data.health = 100f; // default value
            
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }


}
