using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;

    [Header("Flash Effect")]
    [SerializeField] private Image flashImage;   // UI Image màu trắng full màn
    [SerializeField] private float flashDuration = 0.2f;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Button Active")]
    [SerializeField] private GameObject[] allBtns;

    private Texture2D screenCapture;
    private bool viewingPhoto;

    private void OnEnable()
    {
        viewingPhoto = false;
        cameraUI.gameObject.SetActive(true);

        for (int i = 0; i < allBtns.Length; i++)
        {
            allBtns[i].SetActive(false);
        }
    }

    private void OnDisable()
    {
        RemovePhoto();
    }

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // Ban đầu tắt flash
        if (flashImage != null)
        {
            Color c = flashImage.color;
            c.a = 0f;
            flashImage.color = c;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!viewingPhoto)
            {
                StartCoroutine(CapturePhoto());
            }
        }
    }

    private IEnumerator CapturePhoto()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.takePic);

        cameraUI.SetActive(false);
        viewingPhoto = true;

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();

        // Bật flash fade
        StartCoroutine(DoFlash());

        fadingAnimation.Play(CacheString.TAG_Fade);

        for (int i = 0; i < allBtns.Length; i++)
        {
            allBtns[i].SetActive(true);
        }
    }

    private void ShowPhoto()
    {
        viewingPhoto = true;

        Sprite photoSPrite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSPrite;

        photoFrame.SetActive(true);
    }

    private void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
    }

    // Flash trắng fade-out
    private IEnumerator DoFlash()
    {
        if (flashImage == null) yield break;

        Color c = flashImage.color;

        // Bật alpha = 1
        c.a = 1f;
        flashImage.color = c;

        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / flashDuration);
            c.a = alpha;
            flashImage.color = c;
            yield return null;
        }

        // đảm bảo tắt hẳn
        c.a = 0f;
        flashImage.color = c;
    }

    public Texture2D GetCapturedPhoto()
    {
        return screenCapture;
    }
}
