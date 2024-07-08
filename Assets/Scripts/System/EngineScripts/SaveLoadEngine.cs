
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public sealed class SaveLoadEngine
{
    private GameData _data;
    private string _fileName;
    private const int MAXSAVECOUNT = 5;

    public SaveLoadEngine(GameData data)
    {
        _data = data;
    }

    public int GetMaxSaveData => MAXSAVECOUNT;

    public void SaveData()
    {
        string DateAndTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
        _fileName = "NuclearShepherds-" + DateAndTime + ".fns";

        string path = Path.Combine(Application.persistentDataPath, _fileName);
        SavePrefs();
        using FileStream stream = new(path, FileMode.Create);
        using BinaryWriter writer = new(stream);
        writer.Write(_data.Wave);
        writer.Write(_data.Coins);


        //foreach ( KeyValuePair<int , Dictionary<UnityEngine.Vector3Int , int>> unitConfig in _data.UnitsData )
        //{

        //    writer.Write( unitConfig.Key );

        //    foreach ( KeyValuePair<UnityEngine.Vector3Int , int> unitConfigValue in unitConfig.Value )
        //    {

        //        writer.Write( unitConfigValue.Key.x );
        //        writer.Write( unitConfigValue.Key.y );
        //        writer.Write( unitConfigValue.Key.z );

        //        writer.Write( unitConfigValue.Value );
        //    }

        //}



    }

    public string LoadData()
    {
        if (PlayerPrefs.HasKey("saveGame"))
        {
            return PlayerPrefs.GetString("saveGame");

        }
        else return null;
    }

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

    public GameData LoadData(string path)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, path);
        if (File.Exists(fullPath))
        {
            GameHub.Logger("OK FILE: " + path);

            using FileStream stream = new FileStream(fullPath, FileMode.Open);
            using BinaryReader reader = new BinaryReader(stream);
            try
            {
                int wave = reader.ReadInt32();
                int coins = reader.ReadInt32();

               

                GameData data = new GameData(wave, coins);
                return data;
            }
            catch (EndOfStreamException ex)
            {
                GameHub.Logger("Error reading file: " + ex.Message);
                return null;
            }
        }
        else
        {
            GameHub.Logger("File not found: " + path);
            return null;
        }
    }
}