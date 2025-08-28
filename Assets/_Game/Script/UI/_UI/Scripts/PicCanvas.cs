using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicCanvas : UICanvas
{
    [Header("Button")]
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button saveBtn;

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.TransitionUI<ChangeUICanvas, PicCanvas>(0.5f,
                () =>
                {
                    LevelManager.Ins.DespawnLevel();
                    UIManager.Ins.OpenUI<StartCanvas>();
                });
        });

        saveBtn.onClick.AddListener(() =>
        {
            //Save pic into ur device
        });
    }
}
