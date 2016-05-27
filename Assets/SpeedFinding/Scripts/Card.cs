using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Card: MonoBehaviour
{
    public delegate void Action(bool parameter);
    public static event Action OnTap;

    const string path = "Sprites\\SpeedFinding\\canvas";
    private static GameObject canvasPrefab;

    private bool correct;

    public void SetUp(string content,bool image,bool correct)
    {

        GameObject child;

        if (canvasPrefab == null)
            canvasPrefab = Resources.Load<GameObject>(path);

        this.correct = correct;

        if(image)
        {
            child = new GameObject();

            child.transform.SetParent(transform);

            child.transform.localPosition = Vector3.zero;
            SpriteRenderer rnd = child.AddComponent<SpriteRenderer>();

            rnd.sprite = Resources.Load<Sprite>(content);
                
            return;
        }

        child =GameObject.Instantiate(canvasPrefab);

        child.transform.SetParent(transform);
        child.transform.localPosition = Vector3.zero;

        Text  txt = child.GetComponentInChildren<Text>();

        txt.text = content;
        txt.resizeTextForBestFit = true;
        txt.color = Color.black;


        child.AddComponent<Button>().onClick.AddListener(() => Tap());

    }

    public void Tap()
    {
        Fade tmp = gameObject.AddComponent<Fade>();
        tmp.FadeDown(0.5f);
        tmp.OnFinish += Over;



        Destroy(gameObject.transform.GetChild(0).GetComponent<Button>());

        if(OnTap!=null)
            OnTap(correct);
    }

    public void Over()
    {
        Destroy(gameObject);
        SpeedFinding.OnDestroy(this);
    }


}

