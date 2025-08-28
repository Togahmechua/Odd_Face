using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCanvas : UICanvas
{
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button takePicBtn;

    private void OnEnable()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.win);
    }

    private void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, FinishCanvas>(0.6f,
                 () =>
                 {
                     LevelManager.Ins.DespawnLevel();
                     UIManager.Ins.OpenUI<MainCanvas>();
                     LevelManager.Ins.SpawnLevel();
                 });
        });

        takePicBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.CloseUI<FinishCanvas>();
            UIManager.Ins.OpenUI<PicCanvas>();
        });
    }
}
