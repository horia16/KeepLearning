using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public Category Domain;
     
    public event Action<int> OnGameFinished;
    
    internal GameObject Timer;
    internal GameObject Border;
    internal Text Score;

    static GameObject gameBorderPrefab;

    public virtual void StartGame(Category domain, GameObject canvas)
    {
        if (gameBorderPrefab == null)
            gameBorderPrefab = Resources.Load<GameObject>("Prefabs\\GameBorder");

        Domain = domain;
        transform.SetParent(canvas.transform, false);

        Border = GameObject.Instantiate(gameBorderPrefab);
        Border.transform.SetParent(canvas.transform);
        Border.transform.localPosition = Vector3.zero;
        Border.transform.localScale = Vector3.one;

        Timer = Border.transform.GetDeepChild("Timer").gameObject;
        Score = Border.transform.GetDeepChild("Score").GetComponent<Text>();
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
        Destroy(Border);
        if (OnGameFinished != null)
            OnGameFinished(GetPoints());
        Refresh();
        Destroy(gameObject);
    }
}
