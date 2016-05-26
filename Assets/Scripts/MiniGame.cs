using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public Category Domain;
     
    public event Action<int> OnGameFinished;

    public virtual void StartGame(Category domain, GameObject canvas)
    {
        Domain = domain;
        transform.SetParent(canvas.transform, false);
    }

    internal virtual int GetPoints()
    {
        return 0;
    }

    internal virtual void Refresh()
    {
        OnGameFinished = null;
        Domain = null;
    }

    internal virtual void GameFinished()
    {
        if (OnGameFinished != null)
            OnGameFinished(GetPoints());
        Refresh();
        Destroy(gameObject);
    }
}
