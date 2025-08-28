using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCanvas : UICanvas
{
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button takePicBtn;

    private void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.TransitionUI<ChangeUICanvas, PauseCanvas>(0.6f,
                 () =>
                 {
                     LevelManager.Ins.DespawnLevel();
                     UIManager.Ins.OpenUI<MainCanvas>();
                     LevelManager.Ins.SpawnLevel();
                 });
        });

        takePicBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<FinishCanvas>();
            UIManager.Ins.OpenUI<PicCanvas>();
        });
    }
}
