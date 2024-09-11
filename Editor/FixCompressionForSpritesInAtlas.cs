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
                    var settings = textureImporter.GetDefaultPlatformTextureSettings();
                    //if (settings.format != TextureImporterFormat.RGBA32)
                    {
                        settings.format = TextureImporterFormat.RGBA32;
                        textureImporter.SetPlatformTextureSettings(settings);
                        //SetUnCompressed(textureImporter, "Standalone");
                        //SetUnCompressed(textureImporter, "Web");
                        //SetUnCompressed(textureImporter, "iPhone");
                        //SetUnCompressed(textureImporter, "Android");
                        //SetUnCompressed(textureImporter, "WebGL");
                        Debug.Log($"Sprite {sprite.name} set Uncompressed",sprite);
                        EditorUtility.SetDirty(textureImporter);
                    }
                }
            }
        }

        /*
        static void SetUnCompressed(TextureImporter textureImporter, string platform)
        {
            var settings = textureImporter.GetPlatformTextureSettings(platform);
            settings.format = TextureImporterFormat.RGBA32;
            settings.overridden = true;
            textureImporter.SetPlatformTextureSettings(settings);
        }
        */
    }
}