using Coffee.UIExtensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointOnCanvas : MonoBehaviour, ICanvasClickHandler
{
    [SerializeField] private Transform childHolder;

    [SerializeField] private Button myButton;

    private List<UIParticle> particlesList = new List<UIParticle>();

    void Awake()
    {
        RefreshList();
    }

    void OnEnable()
    {
        if (CanvasClickDetector.Ins != null)
            CanvasClickDetector.Ins.Register(this);
    }

    void OnDisable()
    {
        if (CanvasClickDetector.Ins != null)
            CanvasClickDetector.Ins.Unregister(this);

        if (particlesList.Count <= 0)
            return;

        foreach (var particle in particlesList)
        {
            particle.gameObject.SetActive(false);
        }
    }

    public void OnCanvasClick(Vector2 localPos, Vector2 screenPos)
    {
        if (!this.gameObject.activeSelf)
            return;

        if (myButton != null)
        {
            RectTransform btnRect = myButton.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(btnRect, screenPos, null))
            {
                Debug.Log("Click vào chính cái Button -> bỏ qua effect");
                return; // không spawn
            }
        }

        // Nếu không click vào button thì spawn particle
        if (particlesList.Count > 0)
        {
            int rand = Random.Range(0, particlesList.Count);
            var go = particlesList[rand].gameObject;

            go.SetActive(false);
            go.SetActive(true);
            go.transform.localPosition = localPos;

            Debug.Log($"[ParticleSpawner] Spawn {go.name} at {localPos}");
        }
    }

    private void RefreshList()
    {
        particlesList.Clear();
        for (int i = 0; i < childHolder.childCount; i++)
        {
            UIParticle ui = childHolder.GetChild(i).GetComponent<UIParticle>();
            if (ui != null) particlesList.Add(ui);
        }
    }
}