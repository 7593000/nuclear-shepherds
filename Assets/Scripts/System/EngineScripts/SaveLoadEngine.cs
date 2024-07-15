
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public sealed class SaveLoadEngine
{
    private GameData _data;
    private string _fileName;
    private const string NAMEFILE = "NuclearShepherds";
 

    public void SaveData(GameData _data)
    {
        string dateAndTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
        _fileName = NAMEFILE + "-" + dateAndTime + ".fns";

        string path = Path.Combine(Application.persistentDataPath, _fileName);


        using FileStream stream = new(path, FileMode.Create);
        using BinaryWriter writer = new(stream);
        {
            writer.Write(_data.Brahmin);
            writer.Write(_data.Wave);
            writer.Write(_data.Coins);

            foreach (KeyValuePair<int, Dictionary<Vector3Int, int>> unitID in _data.UnitsData)
            {
                writer.Write(unitID.Key);

                writer.Write(unitID.Value.Count); // Записываем количество элементов в словаре

                foreach (KeyValuePair<Vector3Int, int> unitPositionAndLevel in unitID.Value)
                {


                    writer.Write(unitPositionAndLevel.Key.x);
                    writer.Write(unitPositionAndLevel.Key.y);
                    writer.Write(unitPositionAndLevel.Key.z);
                    writer.Write(unitPositionAndLevel.Value); // уровень
                }
            }
        }
    }




    /// <summary>
    /// Загрузка данных из файла сохранения
    /// </summary>
    /// 

    public GameData LoadData(string path)
    {
         
        Dictionary<int, Dictionary<Vector3Int, int>> unitPositionAndLevel = new();
        

        string fullPath = Path.Combine(Application.persistentDataPath, path);

        if (File.Exists(fullPath))
        {
            using FileStream stream = new(fullPath, FileMode.Open);
            using BinaryReader reader = new(stream);
            {
                try
                {
                    int brahmin = reader.ReadInt32();
                    int wave = reader.ReadInt32();
                    int coins = reader.ReadInt32();

                    while (stream.Position < stream.Length)
                    {
                        int unitID = reader.ReadInt32();
                        int count = reader.ReadInt32(); // количество записей в файле

                        if (!unitPositionAndLevel.ContainsKey(unitID))
                        {
                            unitPositionAndLevel[unitID] = new Dictionary<Vector3Int, int>();
                        }

                        for (int i = 0; i < count; i++)
                        {
                            Vector3Int unitPositionCell = new(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                            int level = reader.ReadInt32();

                            GameHub.Logger(unitPositionCell.ToString());
                            unitPositionAndLevel[unitID].Add(unitPositionCell, level);
                        }
                    }

                    GameData data = new(brahmin, wave, coins, unitPositionAndLevel);
                    return data;
                }
                catch (EndOfStreamException ex)
                {
                    GameHub.Logger("Ошибка чтения файла: " + ex.Message);
                    return null;
                }
            }
        }
        else
        {
            GameHub.Logger("Файл не найден: " + path);
            return null;
        }
    }

    public string[] GetAllSaveFiles()
    {
        if (Directory.Exists(Application.persistentDataPath))
        {
            string[] saveFiles = Directory.GetFiles(Application.persistentDataPath, "*.fns");
            for (int i = 0; i < saveFiles.Length; i++)
            {
                saveFiles[i] = Path.GetFileName(saveFiles[i]);
            }
            Array.Reverse(saveFiles);
            return saveFiles;
        }

        else
        {
            Debug.LogError("Не найден путь к сохранениям " + Application.persistentDataPath);
            return new string[0];
        }
    }

}