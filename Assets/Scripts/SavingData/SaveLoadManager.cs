using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Manages saving and loading the player's position.
/// </summary>
public static class SaveLoadManager
{
    static string dataPath = Application.persistentDataPath + "/playerdata.ogdog";

    public static void SavePlayerPosition(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(dataPath, FileMode.Create);

        Debug.Log(Application.persistentDataPath);

        bf.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Load the saved player's position.
    /// </summary>
    /// <returns>The saved player position.</returns>
    public static Vector2 LoadPlayerPosition()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(dataPath, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;
            stream.Close();

            return new Vector2(data.position[0], data.position[1]);
        }
        else
            return new Vector2();
    }

}

/// <summary>
/// Player data saved as an object.
/// </summary>
[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData(Vector2 pos)
    {
        position = new float[2];
        position[0] = pos.x;
        position[1] = pos.y;
    }
}