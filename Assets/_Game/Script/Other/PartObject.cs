using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector3 offset;
    private bool dragging = false;
    private Camera cam;

    public event Action<PartObject> onReleased;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!this.gameObject.activeSelf) return;

        dragging = true;
        Vector3 worldPos = cam.ScreenToWorldPoint(eventData.position);
        offset = transform.position - new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging) return;
        Vector3 worldPos = cam.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z) + offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!dragging) return;
        dragging = false;

        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        // gọi callback để level biết phần này vừa thả xong
        onReleased?.Invoke(this);
    }
}