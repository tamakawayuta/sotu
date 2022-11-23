using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;


public static class SpriteSeparator
{
    public static Dictionary<string, Rect> name2RectMap = new Dictionary<string, Rect>() {
        {"thumbnail",new Rect(0,0,500,500) },
        {"face", new Rect(0,0,800,800) },
    };

    [MenuItem("Assets/Sprite/Separate")]
    public static void SeparateSprite()
    {
        IEnumerable<Texture> targets = Selection.objects.OfType<Texture>();
        if (!targets.Any())
        {
            Debug.LogWarning("Please selecting textures.");
            return;
        }

        foreach(Texture target in targets)
        {
            Separate(AssetDatabase.GetAssetPath(target));
        }
    }

    public static void Separate(string texturePath)
    {
        TextureImporter importer = TextureImporter.GetAtPath(texturePath) as TextureImporter;

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Multiple;
        importer.filterMode = FilterMode.Point;
        EditorUtility.SetDirty(importer);
        AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);

        SpriteMetaData[] sprites = name2RectMap.Keys.Select(
            name => new SpriteMetaData
            {
                name = name,
                rect = name2RectMap[name]
            }
        ).ToArray();

        importer.spritesheet = sprites;
        EditorUtility.SetDirty(importer);
        AssetDatabase.ImportAsset(texturePath, ImportAssetOptions.ForceUpdate);
    }
}
