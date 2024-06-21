 
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TargetPoint))]
public class SpriteHelp : Editor
{
    TargetPoint point;

    private void OnEnable()
    {
        point = (TargetPoint)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (point.GetSprite == null)
            return;

       
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GetAssetPath(point.GetSprite));

        
        if (texture != null)
        {
            float aspectRatio = (float)texture.width / texture.height;
            float height = 450f;
            float width = height * aspectRatio;

            GUILayout.Label("", GUILayout.Height(height), GUILayout.Width(width));
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture, ScaleMode.ScaleToFit);
        }
    }
}
