using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Fade : MonoBehaviour
{

    public event System.Action OnFinish;

    private float time;
    private float target;

    private Color tmpColor;

    private List<SpriteRenderer> SpriteList;
    private List<Image> ImageList;
    private List<Text> TextList;

    #region Static

    const float defaultTime = 0.5f;

    public static void FadeIn(GameObject go)
    {
        FadeIn(go, defaultTime);
    }

    public static void FadeDown(GameObject go)
    {
        FadeDown(go, defaultTime);
    }

    public static void FadeIn(GameObject go,float time)
    {
        StopFadeing(go);
        go.AddComponent<Fade>().FadeIn(time);
    }

    public static void FadeDown(GameObject go, float time)
    {
        StopFadeing(go);
        go.AddComponent<Fade>().FadeDown(time);
    }

    public static void StopFadeing(GameObject go)
    {
        foreach (Fade f in go.GetComponents<Fade>())
        {
            f.StopFading();
            Destroy(f);
        }
        foreach (Fade f in go.GetComponentsInChildren<Fade>())
        {
            f.StopFading();
            Destroy(f);
        }
    }

    public static void SetAlfa(GameObject go,float alfa)
    {
        StopFadeing(go);

        go.AddComponent<Fade>().SetAlfa(alfa);

    }
    
    #endregion

    private float A {
        get {
            throw new System.Exception();
            return 0f; }
        set
        {

            foreach (SpriteRenderer sp in SpriteList)
            {
                if (sp == null)
                    continue;
                tmpColor = sp.color;
                tmpColor.a = value;
                sp.color = tmpColor;
            }
            foreach (Image sp in ImageList)
            {
                tmpColor = sp.color;
                tmpColor.a = value;
                sp.color = tmpColor;
            }
            foreach (Text sp in TextList)
            {
                tmpColor = sp.color;
                tmpColor.a = value;
                sp.color = tmpColor;
            }
        }
    }

    void Awake()
    {
        SpriteList = new List<SpriteRenderer>();
        ImageList = new List<Image>();
        TextList = new List<Text>();

        GetDeepChild<SpriteRenderer>(gameObject,ref SpriteList);
        GetDeepChild<Image>(gameObject, ref ImageList);
        GetDeepChild<Text>(gameObject, ref TextList);

    }

    void GetDeepChild<T>(GameObject go, ref List<T> X)
    {
        if(go.GetComponent<T>()!=null)
            X.Add(go.GetComponent<T>());
    
        foreach(Transform tr in go.transform)
            GetDeepChild<T>(tr.gameObject,ref X);

    }

    public void StopFading()
    {
        try
        {
            CancelInvoke("IncreaseOpacity");
        }
        catch { }
        try
        {
            CancelInvoke("DecreaseOpacity");
        }
        catch { }
    }

    public void SetAlfa(float alfa)
    {
        A = alfa;
        Destroy(this);
    }

    public void FadeIn(float time)
    {
        A = 0f;

        this.target = time;
        this.time = 0f;

       InvokeRepeating("IncreaseOpacity", 0.05f, 0.05f);
    }

    public void FadeDown(float time)
    {
        A = 1f;

        this.target = time;
        this.time = time;

        InvokeRepeating("DecreaseOpacity", 0.05f, 0.05f);
    }

    private void IncreaseOpacity()
    {
        time += 0.05f;

        if(time>=target)
        {
            A = 1;

            CancelInvoke("IncreaseOpacity");

            Destroy(this);

            if (OnFinish != null)
                OnFinish();


            return;
        }

        A =  time/ target ; 
    }

    private void DecreaseOpacity()
    {
        time -= 0.05f;

        if (time <= 0f)
        {
            A = 0f;

            CancelInvoke("DecreaseOpacity");

            Destroy(this);

            if (OnFinish != null)
                OnFinish();

            return;
        }

        A = time / target;
    }

}

