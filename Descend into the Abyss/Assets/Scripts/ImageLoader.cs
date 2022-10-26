using System.IO;
using UnityEngine;
using UnityEditor;

public class ImageLoader : EditorWindow
{
    public static string LoadImage()
    {
        Texture2D texture = new(200,200,TextureFormat.ARGB32, false, true);
        texture.alphaIsTransparency = true;

        string path = EditorUtility.OpenFilePanel("Choose image", "", "png");
        
        if (path.Length == 0)
            return "";

        var fileContent = File.ReadAllBytes(path);
        texture.LoadImage(fileContent);

        string name = path[path.LastIndexOf('/')..];
        texture.name = name[..name.LastIndexOf('.')];
        texture.name.Replace(" ", "");

        AssetDatabase.CreateAsset(texture, "Assets/Resources/Sprites/Loaded/" + texture.name + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = texture;

        Sprite sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), new Vector2(0.5f, 0.5f), 100);
        AssetDatabase.CreateAsset(sprite, "Assets/Resources/Sprites/Loaded/" + "S" + texture.name + ".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = sprite;

        return "Assets/Resources/Sprites/Loaded/" + "S" + texture.name;
    }
}


