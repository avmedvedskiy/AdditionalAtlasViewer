using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.U2D;

namespace SpriteImporterAtlasGUI
{
    [InitializeOnLoad]
    internal static class SpriteImporterAtlasGUIHeader
    {
        private static List<SpriteAtlas> _cachedAtlases = new List<SpriteAtlas>();

        static SpriteImporterAtlasGUIHeader()
        {
            Editor.finishedDefaultHeaderGUI += OnPostHeaderGUI;
        }        

        internal static void ClearCache()
        {
            _cachedAtlases.Clear();
        }

        private static void OnPostHeaderGUI(Editor editor)
        {
            foreach (var e in editor.targets)
            {
                if(e is TextureImporter textureImporter && textureImporter.textureType == TextureImporterType.Sprite)
                {
                    //Draw readonly editor
                    EditorGUILayout.Space(10f);
                    GUI.enabled = false;

                    Sprite sprite = AssetDatabase.LoadAssetAtPath(textureImporter.assetPath, typeof(Sprite)) as Sprite;
                    SpriteAtlas atlas = GetSpriteAtlas(sprite);
                    if(atlas == null)
                        GUI.color = Color.red;

                    EditorGUILayout.ObjectField("Atlas", atlas, typeof(SpriteAtlas),false);

                    GUI.enabled = true;
                }
            }
        }

        private static void CacheAllAtlases()
        {
            AssetDatabase.FindAssets("t:spriteatlas")
                .ToList()
                .ForEach(x => _cachedAtlases.Add(AssetDatabase.LoadAssetAtPath<SpriteAtlas>(AssetDatabase.GUIDToAssetPath(x))));
        }

        private static SpriteAtlas GetSpriteAtlas(Sprite sprite)
        {
            if (_cachedAtlases.Count == 0)
                CacheAllAtlases();

            foreach (var atlas in _cachedAtlases)
            {
                if (atlas.CanBindTo(sprite))
                    return atlas;
            }

            return null;
        }
    }
}