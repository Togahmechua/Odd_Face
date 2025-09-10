using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvas : UICanvas
{
    [SerializeField] private Button startBtn;

    private void Start()
    {
        // Ban đầu disable button
        startBtn.interactable = false;

        // Theo dõi trạng thái load
        StartCoroutine(WaitForDataLoad());

        startBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, StartCanvas>(0.5f,
                () =>
                {
                    UIManager.Ins.OpenUI<MainCanvas>();
                    LevelManager.Ins.SpawnLevel();
                });
        });
    }

    private IEnumerator WaitForDataLoad()
    {
        // Chờ cho đến khi load xong
        yield return new WaitUntil(() => PartManager.Ins != null && PartManager.Ins.IsLoaded);

        // Khi load xong thì enable button
        startBtn.interactable = true;

        // Có thể thêm hiệu ứng hoặc text thay đổi
        Debug.Log("✅ Đã load xong, button sẵn sàng!");
    }
}
