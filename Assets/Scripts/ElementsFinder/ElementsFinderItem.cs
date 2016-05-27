using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ElementsFinderItem : GameItem
{
    RectTransform rect;

    void Awake()
    {
        rect = (RectTransform)transform;
    }

    public override void Drag()
    {
        rect.anchoredPosition += Input.touches[0].deltaPosition;
    }

    public void SetRandom()
    {
        if (transform.parent == null)
            throw new Exception("Not in game space");

        float width = ((RectTransform)transform).rect.width;
        float height = ((RectTransform)transform).rect.height;
        float panelW = ((RectTransform)transform.parent).rect.width;
        float panelH = ((RectTransform)transform.parent).rect.height;

        float X = UnityEngine.Random.Range(- panelW / 2 + width / 2, panelW / 2 - width / 2);
        float Y = UnityEngine.Random.Range(-panelH / 2 + height / 2, panelH / 2 - height / 2);

        ((RectTransform)transform).anchoredPosition = new Vector2(X, Y);
    }
}