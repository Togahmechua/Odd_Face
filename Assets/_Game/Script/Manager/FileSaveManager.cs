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
    public void SaveTexture(Texture2D tex, string defaultFileName = "screenshot.png", string albumName = "MyGame")
    {
#if UNITY_ANDROID || UNITY_IOS
        // --- Save trực tiếp vào Gallery ---
        NativeGallery.SaveImageToGallery(tex, albumName, defaultFileName, (success, path) =>
        {
            if (!success)
                Debug.LogError("❌ Failed to save image to gallery!");
            else
                Debug.Log("✅ Saved to gallery: " + path);
        });
#elif UNITY_STANDALONE || UNITY_EDITOR
        // --- Hiển thị Save As dialog trên PC ---
        var extensionList = new[] { new ExtensionFilter("PNG Image", "png") };
        string path = StandaloneFileBrowser.SaveFilePanel("Save Screenshot", "", defaultFileName, extensionList);

        if (!string.IsNullOrEmpty(path))
        {
            byte[] pngData = tex.EncodeToPNG();
            File.WriteAllBytes(path, pngData);
            Debug.Log("✅ Saved to: " + path);
        }
        else
        {
            Debug.Log("⚠ Save canceled.");
        }
#endif
    }

    /// <summary>
    /// Convert UI Image to Texture2D nếu cần
    /// </summary>
    public Texture2D CaptureScreenToTexture()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        return tex;
    }


    // Cách save FileSaveManager.Instance.SaveTexture(screenCapture, "photo.png", "MyAlbum"); (Mobile)
}