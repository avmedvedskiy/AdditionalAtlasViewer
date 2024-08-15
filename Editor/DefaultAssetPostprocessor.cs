using UnityEditor;

namespace SpriteImporterAtlasGUI
{
    internal class DefaultAssetPostprocessor : AssetPostprocessor
    {
        
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            AtlasFinder.ClearCache();
        }
    }
}