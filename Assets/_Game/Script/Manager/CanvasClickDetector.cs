using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasClickDetector : Singleton<CanvasClickDetector>
{
    private RectTransform canvasRect;
    private readonly List<ICanvasClickHandler> handlers = new List<ICanvasClickHandler>();

    private void Awake()
    {
        canvasRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click chuột hoặc tap
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                Input.mousePosition,
                null,
                out localPoint
            );

            // Gửi event tới tất cả handler
            foreach (var handler in handlers)
            {
                handler.OnCanvasClick(localPoint, Input.mousePosition);
            }
        }
    }

    public void Register(ICanvasClickHandler handler)
    {
        if (!handlers.Contains(handler))
        {
            handlers.Add(handler);
        }
    }

    public void Unregister(ICanvasClickHandler handler)
    {
        if (handlers.Contains(handler))
        {
            handlers.Remove(handler);
        }
    }
}
