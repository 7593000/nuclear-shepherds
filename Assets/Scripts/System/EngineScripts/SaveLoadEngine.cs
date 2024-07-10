
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public sealed class SaveLoadEngine
{
    private GameData _data;
    private string _fileName;
    private const string NAMEFILE = "NuclearShepherds";
    private const int MAXSAVECOUNT = 5;

    public SaveLoadEngine(GameData data)
    {
        _data = data;
    }

    public int GetMaxSaveData => MAXSAVECOUNT;
    public void SaveData()
    {
        string dateAndTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
        _fileName = NAMEFILE+"-" + dateAndTime + ".fns";

        string path = Path.Combine(Application.persistentDataPath, _fileName);
        SavePrefs();

        using FileStream stream = new(path, FileMode.Create);
        using BinaryWriter writer = new(stream);
        {
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



    public string LoadData()
    {
        if (PlayerPrefs.HasKey("saveGame"))
        {
            return PlayerPrefs.GetString("saveGame");

        }
        else return null;
    }


    /// <summary>
    /// Вспомогательное сохранение имен сохранений 
    /// </summary>
    private void SavePrefs()
    {
        List<string> saveGame = new();


        int currentCount = PlayerPrefs.GetInt("countSave", 0);


        if (PlayerPrefs.HasKey("saveGame"))
        {
            string arrPrefs = PlayerPrefs.GetString("saveGame");
            string[] words = arrPrefs.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);


            saveGame.AddRange(words);
        }


        if (saveGame.Count >= MAXSAVECOUNT)
        {
            saveGame[currentCount % MAXSAVECOUNT] = _fileName; // Замена старого сохранения по кругу
        }
        else
        {
            saveGame.Add(_fileName);
        }


        currentCount++;
        PlayerPrefs.SetInt("countSave", currentCount);


        string stringSave = string.Join(",", saveGame);
        PlayerPrefs.SetString("saveGame", stringSave);
        PlayerPrefs.Save();


        GameHub.Logger(stringSave);
    }

    /// <summary>
    /// Загрузка данных из файла сохранения
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
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

                    GameData data = new(wave, coins, unitPositionAndLevel);
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
}