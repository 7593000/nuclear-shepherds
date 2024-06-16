 
using UnityEditor;
using UnityEngine; 

[CustomEditor( typeof( UnitConfig ) )]
public class EnemyEditor : Editor
{
    UnitConfig enemy;

    private void OnEnable()
    {
        
        enemy = target as UnitConfig;
    }
    public override void OnInspectorGUI()
    {
      
        base.OnInspectorGUI();
        
        if ( enemy.GetSprite == null )
            return;

        
        Texture2D texture = AssetPreview.GetAssetPreview( enemy.GetSprite );
        
        GUILayout.Label( "" , GUILayout.Height( 136) , GUILayout.Width( 64 ) );
        
        GUI.DrawTexture( GUILayoutUtility.GetLastRect() , texture );
    }
}
