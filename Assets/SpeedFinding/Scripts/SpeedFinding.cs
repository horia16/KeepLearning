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

    public static void OnTap(Card c)
    {
        manager.Cards.Remove(c);
    }

    public override void StartGame(Category domain, GameObject canvas)
    {
        if (boardPrefab == null)
            boardPrefab = Resources.Load<GameObject>("SpeedFinding\\Prefabs\\Board");

        board = GameObject.Instantiate(boardPrefab);
        board.transform.SetParent(canvas.transform);

        this.domain = domain;
        manager = this;

        Cards = new List<Card>();
        Points = new List<Vector2>();

        int  i = 0;

        foreach (GameObject go in board.transform.GetChildrenWithTag("Card"))
        {
            Cards.Add(go.AddComponent<Card>());

            Fade.SetAlfa(Cards[i].gameObject, 0f);
            Points.Add(go.transform.position);

            i++;
        }

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
        OtherContent = (List<string>)CategoryRandomer.GetRandomWords(domain, 15, delegate (Category c) { return c != category; });

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
            int tmp = UnityEngine.Random.Range(0, Points.Count - 1);

            Cards[i].gameObject.transform.position =  Points[tmp];
            Points.RemoveAt(tmp);
        }

        Invoke("Shuffle", 5f);
    } 

    internal override int GetPoints()
    {
        return 10;
    }
    void OnDestroy()
    {
        try { CancelInvoke("Shuffle"); } catch { }
        
        //Timer.StopCountDown();

        Destroy(board);
    }


}
