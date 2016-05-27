using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SpeedFinding : MiniGame
{
    static SpeedFinding manager;
    static GameObject boardPrefab;

    GameObject board;

    List<string> CorrectContent = new List<string> ();
    List<string> OtherContent = new List<string>();
    List<Vector2> Points;
    List<Card> Cards;
    
    Category domain, category;

    int score;
    int nr;
   public  int guessed;

    public void OnTap(Card c,bool correct)
    {
        manager.Cards.Remove(c);
        if (correct)
        {
            score++;
            guessed++;
            if(guessed==5)
            {
                GameFinished();
            }
        }
        else
            score--;
    }

    public override void StartGame(Category domain, GameObject canvas)
    {
        guessed = score = nr = 0;

        if (boardPrefab == null)
            boardPrefab = Resources.Load<GameObject>("SpeedFinding\\Prefabs\\Board");

        board = GameObject.Instantiate(boardPrefab);
        board.transform.SetParent(canvas.transform);
        board.transform.localScale = Vector3.one;

        this.domain = domain;
        manager = this;

        Cards = new List<Card>();
        Points = new List<Vector2>();

        Card.ClearEvent();
        Card.OnTap += OnTap;

        int  i = 0;

        foreach (GameObject go in board.transform.GetChildrenWithTag("Card"))
        {
            Cards.Add(go.AddComponent<Card>());

            Fade.SetAlfa(Cards[i].gameObject, 0f);
            Points.Add(go.transform.position);

            i++;
        }


        base.StartGame(domain, canvas);

        Invoke("WriteDownCorrectCards", 0.5f);
    }
        
    void WriteDownCorrectCards()
    {
        int i = 0;
        category = CategoryRandomer.ChooseSubcategory(domain, delegate (Category c) { return c.Words.Count >= 5; });
        CorrectContent = (List<string>)CategoryRandomer.ChooseItems(category.Words, 5);
        
        foreach (string c in CorrectContent)
        {
            Cards[i].SetUp(c, false, true);

            Fade.StopFadeing(Cards[i].gameObject);
            Fade.FadeIn(Cards[i].gameObject);
            i++;
        }

        Invoke("WriteDownRestOfCards", 2f);
    }

    void WriteDownRestOfCards()
    {
        int i = CorrectContent.Count;
        OtherContent = (List<string>)CategoryRandomer.GetRandomWords(domain, 15, delegate (Category c) { return c == category; });

        foreach (string c in OtherContent)
        {
            Cards[i].SetUp(c, false, false);
            Fade.StopFadeing(Cards[i].gameObject);
            Fade.FadeIn(Cards[i].gameObject);
            i++;
        }

        Invoke("Shuffle", 1f);
    }

    void Shuffle()
    {
        List<Vector2> Points = new List<Vector2>(this.Points);

        for (int i = 0; i < Cards.Count; i++)
        {
            int tmp = UnityEngine.Random.Range(0, Points.Count);
            try
            {
                Cards[i].gameObject.transform.position = Points[tmp];
                Points.RemoveAt(tmp);
            }
            catch { }
        }

        nr++;

        if (nr == 1)
        {
            foreach(Card c in Cards)
            {
                c.AddTapEvent();
            }
        }

        Invoke("Shuffle", 5f);
    } 

    internal override int GetPoints()
    {
        return max(score*2,0);
    }

    void OnDestroy()
    {
        try { CancelInvoke("Shuffle"); } catch { }
        
        //Timer.StopCountDown();

        Destroy(board);
    }

    int max(int a, int b) { return a > b ? a : b; }
}
