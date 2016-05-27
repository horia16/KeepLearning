using UnityEngine;
using UnityEngine.UI;

class Card: MonoBehaviour
{
    public delegate void Action(bool parameter);
    public static event Action OnTap;


    private bool correct;

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
        txt.resizeTextForBestFit = true;
        txt.color = Color.white;
        txt.font = Font.CreateDynamicFontFromOSFont("Arial",12);
        gameObject.GetComponent<Button>().onClick.AddListener(() => Tap());

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
    }
}

