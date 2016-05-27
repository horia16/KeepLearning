using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Card: MonoBehaviour
{
    public delegate void Action(Card who,bool parameter);
    public static event Action OnTap;


    private bool correct;

    public static void ClearEvent()
    {
        OnTap = null;
    }

    public void SetUp(string content,bool image,bool correct)
    {

        GameObject child;
        child = new GameObject();
        child.transform.SetParent(transform);
        child.transform.localPosition = Vector3.zero;

        this.correct = correct;
        if (image)
        {
            SpriteRenderer rnd = child.AddComponent<SpriteRenderer>();
            rnd.sprite = Resources.Load<Sprite>(content);
            return;
        }
    
        Text  txt = child.AddComponent<Text>();
        txt.text = content;
        txt.color = Color.white;
        txt.font = Font.CreateDynamicFontFromOSFont("Arial",10);

        txt.resizeTextForBestFit = true;

        ((RectTransform)child.transform).anchorMin = Vector2.zero;
        ((RectTransform)child.transform).anchorMax = Vector2.one;
        ((RectTransform)child.transform).offsetMin = Vector2.zero;
        ((RectTransform)child.transform).offsetMax = Vector2.zero;
        ((RectTransform)child.transform).localScale = Vector3.one;
        

    }

    public void AddTapEvent()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() => Tap());
    }

    public void Tap()
    {
        Fade tmp = gameObject.AddComponent<Fade>();

        tmp.FadeDown(0.5f);
        tmp.OnFinish += Over;

        Destroy(gameObject.GetComponent<Button>());

        if(OnTap!=null)
            OnTap(this,correct);
    }

    public void Over()
    {
        Destroy(gameObject);
    }
}

