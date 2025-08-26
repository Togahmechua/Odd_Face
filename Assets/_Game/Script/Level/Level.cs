using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Canvas cv;
    [SerializeField] private List<PartObject> poList = new List<PartObject>();

    private int currentIndex = 0;

    private void Start()
    {
        cv.renderMode = RenderMode.ScreenSpaceCamera;
        cv.worldCamera = Camera.main;

        PartManager.Ins.RandomPart(transform);

        // giả sử PartManager đã add các PartObject vào poList
        ActiveNextPart();
    }

    public void AddToList(PartObject po)
    {
        poList.Add(po);
        po.onReleased += OnPartReleased; // gán callback
    }

    private void ActiveNextPart()
    {
        if (currentIndex >= poList.Count)
        {
            Debug.Log("Done!");
            // active toàn bộ
            foreach (var po in poList)
            {
                po.gameObject.SetActive(true);
            }
            return;
        }

        poList[currentIndex].gameObject.SetActive(true);

        UIManager.Ins.mainCanvas.ShowTxt(poList[currentIndex].gameObject.name);
    }

    private void OnPartReleased(PartObject po)
    {
        // tắt object vừa drag xong
        po.gameObject.SetActive(false);

        currentIndex++;
        ActiveNextPart();
    }
}
