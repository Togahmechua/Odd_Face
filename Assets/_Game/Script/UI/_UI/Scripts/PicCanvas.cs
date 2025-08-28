using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicCanvas : UICanvas
{
    [Header("Button")]
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button saveBtn;

    [Header("Reference")]
    [SerializeField] private PhotoCapture photoCapture;

    private void OnEnable()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.win);
    }

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, PicCanvas>(0.5f,
                () =>
                {
                    LevelManager.Ins.DespawnLevel();
                    UIManager.Ins.OpenUI<StartCanvas>();
                });
        });

        saveBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            //Save pic into ur device

            Texture2D photo = photoCapture.GetCapturedPhoto();
            if (photo != null)
            {
#if UNITY_ANDROID || UNITY_IOS
                FileSaveManager.Ins.SaveTexture(photo, "Odd Face", "MyAlbum");
#elif UNITY_STANDALONE || UNITY_EDITOR
                FileSaveManager.Ins.SaveTexture(photo, "photo.png");
#endif
            }
            else
            {
                Debug.LogWarning("No photo captured yet!");
            }
        });
    }
}
