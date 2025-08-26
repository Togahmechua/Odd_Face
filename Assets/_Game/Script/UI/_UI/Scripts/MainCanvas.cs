using EasyTextEffects;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private TextEffect txtEff;

    private void Start()
    {
        UIManager.Ins.mainCanvas = this;

        pauseBtn.onClick.AddListener(() =>
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.OpenUI<PauseCanvas>();
            UIManager.Ins.CloseUI<MainCanvas>();
        });
    }

    public void ShowTxt(string name)
    {
        txt.text = name;
        txtEff.Refresh();
    }
}
