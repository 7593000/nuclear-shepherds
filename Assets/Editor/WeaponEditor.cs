 
using UnityEditor;
using UnityEngine; 

[CustomEditor( typeof( CreateWeapons ) )]
public class WeaponEditor : Editor
{
    CreateWeapons weapon;

    private void OnEnable()
    {
        
        weapon = target as CreateWeapons;
    }
    public override void OnInspectorGUI()
    {
      
        base.OnInspectorGUI();
        
        if ( weapon.GetSprite == null )
            return;

        
        Texture2D texture = AssetPreview.GetAssetPreview( weapon.GetSprite );
        
        GUILayout.Label( "" , GUILayout.Height( 80 ) , GUILayout.Width( 80 ) );
        
        GUI.DrawTexture( GUILayoutUtility.GetLastRect() , texture );
    }
}
