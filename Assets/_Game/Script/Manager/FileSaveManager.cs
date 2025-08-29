using UnityEngine;
using System.IO;
using System;

#if UNITY_STANDALONE || UNITY_EDITOR
using SFB; // StandaloneFileBrowser
#endif

public class FileSaveManager : Singleton<FileSaveManager>
{
    /// <summary>
    /// Save texture to device storage (PC = Save As, Mobile = Gallery)
    /// </summary>
    public void SaveTexture(Texture2D texture, string filename, string albumName = "MyApp", Action<bool, string> callback = null)
    {
        if (texture == null)
        {
            Debug.LogWarning("Texture is null, cannot save!");
            callback?.Invoke(false, null);
            return;
        }

        byte[] bytes = texture.EncodeToPNG();

#if UNITY_ANDROID && !UNITY_EDITOR
        // Request permission first
        NativeGallery.RequestPermissionAsync(permission =>
        {
            if (permission == NativeGallery.Permission.Granted)
            {
                NativeGallery.SaveImageToGallery(bytes, albumName, filename, (success, path) =>
                {
                    if (success)
                        Debug.Log("✅ Saved to gallery (Android): " + path);
                    else
                        Debug.LogWarning("❌ Failed to save to gallery (Android)");
                    callback?.Invoke(success, path);
                });
            }
            else
            {
                Debug.LogWarning("❌ Permission denied (Android)");
                callback?.Invoke(false, null);
            }
        }, NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);

#elif UNITY_IOS && !UNITY_EDITOR
        NativeGallery.SaveImageToGallery(bytes, albumName, filename, (success, path) =>
        {
            if (success)
                Debug.Log("✅ Saved to gallery (iOS): " + path);
            else
                Debug.LogWarning("❌ Failed to save to gallery (iOS)");
            callback?.Invoke(success, path);
        });

#elif UNITY_STANDALONE || UNITY_EDITOR
        string path = StandaloneFileBrowser.SaveFilePanel("Save Image", "", filename, "png");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllBytes(path, bytes);
            Debug.Log("💾 Saved to PC: " + path);
            callback?.Invoke(true, path);
        }
        else
        {
            Debug.LogWarning("❌ Save cancelled.");
            callback?.Invoke(false, null);
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
