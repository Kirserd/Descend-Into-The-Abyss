using SFB;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Collections;

[System.Obsolete]
public class ImageLoader : MonoBehaviour
{
    private byte[] _textureBytes;
    public string LoadImagePath()
    {
        var extensions = new[] 
        {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
            new ExtensionFilter("All Files", "*" ),
        };
        string ImagePath = null;
        foreach (string path in StandaloneFileBrowser.OpenFilePanel("Open Image", "", extensions, true))
            ImagePath = path;

        StartCoroutine(EncodeTextureToPNG(ImagePath));
        return ImagePath;
    }

    public string PackImage(string ElementName)
    {
        if (File.Exists(Application.dataPath + "/DatabaseElements/" + ElementName + ".png") || _textureBytes == null )
            return null;

        File.WriteAllBytes(Application.dataPath + "/DatabaseElements/" + ElementName + ".png", _textureBytes);
        return Application.dataPath + "/DatabaseElements/" + ElementName + ".png";
    }
    
    public void VisualizeImageOn(RawImage Image, string Path) => StartCoroutine(VisualizeImageRoutine(Image, Path));
    
    private IEnumerator VisualizeImageRoutine(RawImage Image, string Path)
    {
        var loader = new WWW(Path);
        yield return loader;
        Image.texture = loader.texture;
    }

    private IEnumerator EncodeTextureToPNG(string Path)
    {
        if (Path == null || Path == "")
            yield break;

        var loader = new WWW(Path);
        yield return loader;
        _textureBytes = loader.texture.EncodeToPNG();
    }
}


