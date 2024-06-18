 
using UnityEditor;
using UnityEngine; 

[CustomEditor( typeof( WeaponsConfig ) )]
public class WeaponEditor : Editor
{
    WeaponsConfig weapon;

    private void OnEnable()
    {
        
        weapon = target as WeaponsConfig;
    }
    public override void OnInspectorGUI()
    {
      
        base.OnInspectorGUI();
        
        if ( weapon.GetSprite == null )
            return;

        
        Texture2D texture = AssetPreview.GetAssetPreview( weapon.GetSprite );
        
        GUILayout.Label( "" , GUILayout.Height( 200 ) , GUILayout.Width(200) );
        
        GUI.DrawTexture( GUILayoutUtility.GetLastRect() , texture );
    }
}
