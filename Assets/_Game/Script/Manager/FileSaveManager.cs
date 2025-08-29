using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_STANDALONE || UNITY_EDITOR
using SFB;
#endif

public class FileSaveManager : Singleton<FileSaveManager>
{
    // WebGL JS bridge
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DownloadFile(string filename, string base64);
#endif

    /// <summary>
    /// Save Texture2D to device (PC: Save As, Mobile: Gallery, WebGL: download via browser)
    /// </summary>
    public void SaveTexture(Texture2D texture, string filename, string albumName = "MyApp", Action<bool, string> callback = null)
    {
        if (texture == null)
        {
            Debug.LogWarning("SaveTexture: texture is null");
            callback?.Invoke(false, null);
            return;
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL: convert to base64 and send to browser
        byte[] bytes = GetTextureBytesSafe(texture, false);
        string base64 = Convert.ToBase64String(bytes);
        try
        {
            DownloadFile(filename, base64);
            Debug.Log("WebGL: download triggered for " + filename);
            callback?.Invoke(true, filename);
        }
        catch (Exception ex)
        {
            Debug.LogWarning("WebGL: save failed - " + ex);
            callback?.Invoke(false, null);
        }

#elif UNITY_ANDROID && !UNITY_EDITOR
        // Android: use NativeGallery (package must be present)
        byte[] bytesAndroid = GetTextureBytesSafe(texture, false);
        NativeGallery.RequestPermissionAsync(permission =>
        {
            if (permission == NativeGallery.Permission.Granted)
            {
                NativeGallery.SaveImageToGallery(bytesAndroid, albumName, filename, (success, path) =>
                {
                    Debug.Log(success ? $"Saved to gallery (Android): {path}" : "Failed to save to gallery (Android)");
                    callback?.Invoke(success, path);
                });
            }
            else
            {
                Debug.LogWarning("Permission denied (Android)");
                callback?.Invoke(false, null);
            }
        }, NativeGallery.PermissionType.Write, NativeGallery.MediaType.Image);

#elif UNITY_IOS && !UNITY_EDITOR
        // iOS: use NativeGallery
        byte[] bytesIos = GetTextureBytesSafe(texture, false);
        NativeGallery.SaveImageToGallery(bytesIos, albumName, filename, (success, path) =>
        {
            Debug.Log(success ? $"Saved to gallery (iOS): {path}" : "Failed to save to gallery (iOS)");
            callback?.Invoke(success, path);
        });

#elif UNITY_STANDALONE || UNITY_EDITOR
        // PC / Editor
        byte[] bytesPc = GetTextureBytesSafe(texture, false);
        string path = StandaloneFileBrowser.SaveFilePanel("Save Image", "", filename, "png");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllBytes(path, bytesPc);
            Debug.Log("Saved to PC: " + path);
            callback?.Invoke(true, path);
        }
        else
        {
            Debug.LogWarning("Save cancelled");
            callback?.Invoke(false, null);
        }
#endif
    }

    // Helper: return readable PNG/JPG bytes (makes a readable copy if needed)
    private byte[] GetTextureBytesSafe(Texture2D texture, bool asJpeg)
    {
        try
        {
            if (texture.isReadable)
            {
                return asJpeg ? texture.EncodeToJPG(100) : texture.EncodeToPNG();
            }
        }
        catch { /* fallthrough to copy method */ }

        // Create a readable copy
        RenderTexture rt = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(texture, rt);
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        Texture2D copy = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        copy.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        copy.Apply();
        RenderTexture.active = prev;
        RenderTexture.ReleaseTemporary(rt);

        byte[] bytes = asJpeg ? copy.EncodeToJPG(100) : copy.EncodeToPNG();
        UnityEngine.Object.Destroy(copy);
        return bytes;
    }

    /// <summary>
    /// Capture screen and return Texture2D (synchronous call must be in coroutine WaitForEndOfFrame)
    /// </summary>
    public Texture2D CaptureScreenToTexture()
    {
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();
        return tex;
    }
}
