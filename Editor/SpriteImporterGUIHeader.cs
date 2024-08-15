using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace SpriteImporterAtlasGUI
{
    [InitializeOnLoad]
    internal static class SpriteImporterAtlasGUIHeader
    {
        static SpriteImporterAtlasGUIHeader()
        {
            Editor.finishedDefaultHeaderGUI += OnPostHeaderGUI;
        }

        private static void OnPostHeaderGUI(Editor editor)
        {
            var e = editor.target;
            if(e is TextureImporter { textureType: TextureImporterType.Sprite } textureImporter)
            {
                //Draw readonly editor
                EditorGUILayout.Space(10f);
                GUI.enabled = false;

                Sprite sprite = AssetDatabase.LoadAssetAtPath(textureImporter.assetPath, typeof(Sprite)) as Sprite;
                if (!AtlasFinder.TryGetSpriteAtlas(sprite, out var atlas))
                    GUI.color = Color.red;

                EditorGUILayout.ObjectField("Atlas", atlas, typeof(SpriteAtlas),false);
                GUI.color = Color.white;
                GUI.enabled = true;
            }
            
        }
    }
}