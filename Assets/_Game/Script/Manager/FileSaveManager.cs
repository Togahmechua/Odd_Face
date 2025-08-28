using UnityEngine;
using System.IO;

#if UNITY_STANDALONE || UNITY_EDITOR
using SFB; // StandaloneFileBrowser
#endif

public class FileSaveManager : Singleton<FileSaveManager>
{
    /// <summary>
    /// Save texture to device storage (PC = Save As, Mobile = Gallery)
    /// </summary>
    public void SaveTexture(Texture2D texture, string filename, string albumName = "MyApp")
    {
        byte[] bytes = texture.EncodeToPNG();

#if UNITY_ANDROID && !UNITY_EDITOR
        NativeGallery.SaveImageToGallery(bytes, albumName, filename);
        Debug.Log("✅ Saved to gallery (Android)");

#elif UNITY_IOS && !UNITY_EDITOR
        NativeGallery.SaveImageToGallery(bytes, albumName, filename);
        Debug.Log("✅ Saved to gallery (iOS)");

#elif UNITY_STANDALONE || UNITY_EDITOR
        string path = StandaloneFileBrowser.SaveFilePanel("Save Image", "", filename, "png");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllBytes(path, bytes);
            Debug.Log("💾 Saved to PC: " + path);
        }
        else
        {
            Debug.LogWarning("❌ Save cancelled.");
        }
#endif
    }

    /// <summary>
    /// Capture screen as Texture2D
    /// </summary>
    public Texture2D CaptureScreenToTexture()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
