using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

namespace SpriteImporterAtlasGUI
{
    internal static class AtlasFinder
    {
        private static readonly List<SpriteAtlas> _cachedAtlases = new List<SpriteAtlas>();
        

        public static void ClearCache()
        {
            _cachedAtlases.Clear();
        }

        private static void CacheAllAtlases()
        {
            AssetDatabase.FindAssets("t:spriteatlas")
                .ToList()
                .ForEach(x => _cachedAtlases.Add(AssetDatabase.LoadAssetAtPath<SpriteAtlas>(AssetDatabase.GUIDToAssetPath(x))));
        }

        public static bool TryGetSpriteAtlas(Sprite sprite,out SpriteAtlas atlas)
        {
            atlas = null;
            if (_cachedAtlases.Count == 0)
                CacheAllAtlases();
            
            if (sprite == null)
                return false;

            foreach (var a in _cachedAtlases)
            {
                if (a.CanBindTo(sprite))
                {
                    atlas = a;
                    return true;
                }
            }

            return false;
        }
    }
}