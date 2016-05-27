using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class ElementsFinderItem : GameItem
{
    float minX, maxX, minY, maxY;
     
    public void SetRandom()
    {
        if (transform.parent == null)
            throw new Exception("Not in game space");

        float width = ((RectTransform)transform).rect.width * transform.localScale.x;
        float height = ((RectTransform)transform).rect.height * transform.localScale.y;
        float panelW = ((RectTransform)transform.parent).rect.width;
        float panelH = ((RectTransform)transform.parent).rect.height;

        minX = width / 2;
        minY = height / 2;
        maxX = panelW - width / 2;
        maxY = panelH - height / 2;

        float X = UnityEngine.Random.Range(minX, maxX);
        float Y = UnityEngine.Random.Range(minY, maxY);

        ((RectTransform)transform).anchoredPosition = new Vector2(X, Y);
    }
}