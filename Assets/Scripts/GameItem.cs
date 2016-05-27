using System;
using UnityEngine;

public class GameItem : MonoBehaviour, IMovable
{
    bool isDraging;
    public bool IsCorectItem;
    public static event Action<GameItem> GameItemChoosed;
    

    public virtual void Tap()
    {
        if (!isDraging)
            ItemChoosed();
    }

    public virtual void BeginDrag()
    {
        if(Input.touchCount == 1)
            isDraging = true;
    }

    public virtual void Drag()
    {
        if (Input.touchCount == 1 && isDraging)
            transform.position += (Vector3)Input.touches[0].deltaPosition;
    }

    public virtual void Drop()
    {
        isDraging = false;
    }

    internal void ItemChoosed()
    {
        GameItemChoosed(this);
    }

    public static void Refresh()
    {
        GameItemChoosed = null;
    }
}