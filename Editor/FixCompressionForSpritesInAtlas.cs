using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SpriteImporterAtlasGUI
{
    public class FixCompressionForSpritesInAtlas
    {
        [MenuItem("Tools/Atlas/SetNoneCompressionForSpritesInAtlas")]
        static void Fix()
        {
            var allSprites = AssetDatabase.FindAssets("t:sprite")
                .Select(x => AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(x)));

            foreach (var sprite in allSprites)
            {
                if (AtlasFinder.TryGetSpriteAtlas(sprite, out _))
                {
                    var path = AssetDatabase.GetAssetPath(sprite);
                    var textureImporter =(TextureImporter)AssetImporter.GetAtPath(path);
                    if (textureImporter.textureCompression != TextureImporterCompression.Uncompressed)
                    {
                        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
                        Debug.Log($"Sprite {sprite.name} set Uncompressed",sprite);
                    }
                }
            }
        }
    }
}