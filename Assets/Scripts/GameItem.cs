using System;
using UnityEngine;

public class GameItem : MonoBehaviour, IMovable
{
    public bool IsCorectItem;
    public static event Action<GameItem> GameItemChoosed;

    public virtual void Tap()
    {
        ItemChoosed();
    }

    public virtual void Drag()
    {

    }

    public virtual void Drop()
    {

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