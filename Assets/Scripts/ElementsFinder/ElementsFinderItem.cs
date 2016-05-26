using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ElementsFinderItem : GameItem
{
    public override void Drag()
    {
        base.Drag();
    }

    public void SetRandom()
    {
        if (transform.parent == null)
            throw new Exception("Not in game space");

        float width = ((RectTransform)transform).rect.width;
        float height = ((RectTransform)transform).rect.height;
        float panelW = ((RectTransform)transform.parent).rect.width;
        float panelH = ((RectTransform)transform.parent).rect.height;

        float X = UnityEngine.Random.Range(width / 2, panelW - width / 2);
        float Y = UnityEngine.Random.Range(height / 2, panelH - height / 2);

       // ((RectTransform)transform).offsetMin = new Vector2(X + width / 2, Y - height / 2);
        //((RectTransform)transform).offsetMax = new Vector2(X - width / 2, Y + height / 2);
    }
}