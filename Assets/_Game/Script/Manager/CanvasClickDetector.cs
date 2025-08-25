using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasClickDetector : Singleton<CanvasClickDetector>
{
    [SerializeField] private RectTransform canvasRect;
    private readonly List<ICanvasClickHandler> handlers = new List<ICanvasClickHandler>();

    void Update()
    {
        if (handlers.Count == 0)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, Input.mousePosition, null, out Vector2 localPos))
            {
                foreach (var handler in handlers)
                {
                    handler.OnCanvasClick(localPos, Input.mousePosition);
                }
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
