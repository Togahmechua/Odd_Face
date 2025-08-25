using Coffee.UIExtensions;
using System.Collections.Generic;
using UnityEngine;

public class PointOnCanvas : MonoBehaviour
{
    private RectTransform canvasRect;

    [SerializeField] private Transform childHolder;
    [SerializeField] private List<UIParticle> uiParList = new List<UIParticle>();

    void Start()
    {
        // Lấy RectTransform của Canvas
        canvasRect = GetComponent<RectTransform>();

        // Load list particle từ childHolder
        RefreshParticleList();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click hoặc Tap
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                null, // vì Screen Space - Overlay
                out localPoint))
            {
                // Random 1 particle
                GameObject go = Particle();
                if (go != null)
                {
                    go.SetActive(true); // bật particle nếu đang tắt
                    go.transform.localPosition = localPoint;

                    Debug.Log($"[Click] LocalPos = {localPoint}, ScreenPos = {Input.mousePosition}, Spawned: {go.name}");
                }
                else
                {
                    Debug.LogWarning("Không có UIParticle nào trong list!");
                }
            }
        }
    }

    private GameObject Particle()
    {
        if (uiParList.Count <= 0) return null;
        int rand = Random.Range(0, uiParList.Count);
        return uiParList[rand].gameObject;
    }

    private void RefreshParticleList()
    {
        uiParList.Clear();
        if (childHolder == null) return;

        for (int i = 0; i < childHolder.childCount; i++)
        {
            UIParticle ui = childHolder.GetChild(i).GetComponent<UIParticle>();
            if (ui != null)
            {
                uiParList.Add(ui);
            }
        }
    }

    private void OnValidate()
    {
        // Auto refresh list khi chỉnh trong inspector
        RefreshParticleList();
    }
}
