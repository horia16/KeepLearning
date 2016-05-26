using System;
using UnityEngine;

public class GameItem : MonoBehaviour, IMovable
{
    public bool IsCorectItem;
    public static event Action<GameItem> GameItemChoosed;

    public virtual void Tap()
    {
        if (GameItemChoosed != null)
            GameItemChoosed(this);
    }

    public virtual void Drag()
    {

    }

    public virtual void Drop()
    {

    }
}