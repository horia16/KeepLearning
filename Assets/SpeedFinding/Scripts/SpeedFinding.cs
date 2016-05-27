using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class SpeedFinding : MiniGame
{
    struct Content
    {
        string content;
        bool isImage, isCorrect;
    }

      
    List<string> CorrectContent = new List<string> ();
    List<string> OtherContent = new List<string> ();

    static SpeedFinding manager;
        
    List<Vector2> Points;

    List<Card> Cards;

    int N, M;

    Category domain, category;

    public static void OnDestroy(Card c)
    {
        manager.Cards.Remove(c);
    }

    public override void StartGame(Category domain, GameObject canvas)
    {
        transform.GetChild(0).gameObject.SetActive(true);

        this.domain = domain;

        manager = this;
        Cards = new List<Card>();
        Points = new List<Vector2>();
        N = 4;
        M = 5;

        int  i = 0;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Card"))
        {


            Cards.Add(go.AddComponent<Card>());


            Fade.SetAlfa(Cards[i].gameObject, 0f);
            i++;

            Points.Add(go.transform.position);
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
                Debug.Log(Cards[i]);
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



}
