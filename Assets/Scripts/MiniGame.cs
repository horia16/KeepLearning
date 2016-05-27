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
    
    internal GameObject timer;
    internal GameObject Border;
    internal Text Score;

    static GameObject gameBorderPrefab;

    internal void SetCategory(Category c)
    {
        Border.transform.GetDeepChild("CategoryName").GetComponent<Text>().text = c.Name;
    }

    public virtual void StartGame(Category domain, GameObject canvas)
    {
        if (gameBorderPrefab == null)
            gameBorderPrefab = Resources.Load<GameObject>("Prefabs\\GameBorder");

        Domain = domain;
        transform.SetParent(canvas.transform, false);

        Border = GameObject.Instantiate(gameBorderPrefab);
        Border.transform.SetParent(canvas.transform,false);
      //  Border.transform.localPosition = Vector3.zero;
       // Border.transform.localScale = Vector3.one;

        timer = Border.transform.GetDeepChild("Timer").gameObject;
        Score = Border.transform.GetDeepChild("Score").GetComponent<Text>();
        Border.transform.GetDeepChild("Back").GetComponent<Button>().onClick.AddListener(() => GameFinished() );
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

    public virtual void GameFinished()
    {
        Destroy(Border);
        if (OnGameFinished != null)
            OnGameFinished(GetPoints());
        Refresh();
        Destroy(gameObject);
    }
}
