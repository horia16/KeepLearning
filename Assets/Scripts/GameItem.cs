using System;
using UnityEngine;

public class GameItem : MonoBehaviour, IMovable
{
    public bool IsCorectItem;
    public static event Action<GameItem> GameItemChoosed;

    public void Tap()
    {
        if (GameItemChoosed != null)
            GameItemChoosed(this);
    }

    public void Drag()
    {

    }

    public void Drop()
    {

    }
}