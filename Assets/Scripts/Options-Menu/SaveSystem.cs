// Here's a way I found to save your data with Binary Formatters so that your game data is protected which prevents any changes to for example a save file.

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveData(Options options)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/settings.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        OptionsData data = new OptionsData(options);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static OptionsData LoadData()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OptionsData data = formatter.Deserialize(stream) as OptionsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Savefile not found in " + path);
            return null;
        }
    }
}
