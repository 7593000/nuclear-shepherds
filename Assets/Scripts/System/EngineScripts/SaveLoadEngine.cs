
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[Serializable]
public sealed class SaveLoadEngine
{
    private GameData _data;
    private string _fileName;
    private int _idSaveData = 0;
    const int MAXSAVECOUNT = 5;
    public SaveLoadEngine( GameData data )
    {
        _data = data;
    }


    public void SaveData()
    {
        string DateAndTime = DateTime.Now.ToString( "dd-MM-yyyy_HH-mm" );
        _fileName = "NuclearShepherds-" + DateAndTime + ".fns";

        string path = Path.Combine( Application.persistentDataPath , _fileName );
        SavePlayerPrefs();
        using ( FileStream stream = new( path , FileMode.Create ) )
        using ( BinaryWriter writer = new( stream ) )
        {
            writer.Write( _data.Wave );
            writer.Write( _data.Coins );


            foreach ( KeyValuePair<int , Dictionary<UnityEngine.Vector3Int , int>> unitConfig in _data.UnitsData )
            {

                writer.Write( unitConfig.Key );

                foreach ( KeyValuePair<UnityEngine.Vector3Int , int> unitConfigValue in unitConfig.Value )
                {

                    writer.Write( unitConfigValue.Key.x );
                    writer.Write( unitConfigValue.Key.y );
                    writer.Write( unitConfigValue.Key.z );

                    writer.Write( unitConfigValue.Value );
                }

            }
        }



    }


    private void SavePlayerPrefs()
    {
 
         

            int currentCount = PlayerPrefs.GetInt( "countSave" , 0 );


            if ( currentCount >= MAXSAVECOUNT )
            {
                currentCount = 0;
            }

          
            string saveKey = currentCount.ToString();

          
            PlayerPrefs.SetString( saveKey , _fileName );

            
            currentCount++;

            
            PlayerPrefs.SetInt( "countSave" , currentCount );

      
        PlayerPrefs.Save();
        }
       
      
 


    public GameData LoadData( string path )
    {
        if ( File.Exists( path ) )
        {

        }
        return null;
    }
}