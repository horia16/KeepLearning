using UnityEngine;

public class Timer : MonoBehaviour {
   
    private static Timer timer;
    private static GameObject Me;
    private static GameObject Needle;
    
    private static bool wasLoaded;
    private static int target;
    private static int secondsPast;

    private static Quaternion rotation {
        get { throw new System.Exception();
              return Quaternion.identity; }

        set { Needle.transform.rotation = value; }
    }
    private static Quaternion startRotation = Quaternion.Euler(0f, 0f, 90f);

    public static event System.Action OnTic;

    void Awake()
    { 
        timer = this;
        Me = this.gameObject;
        Needle = this.transform.GetDeepChild("Needle").gameObject;
        
    }


    public static void StartCountDown(int seconds)
    {
        Initialize();
        target = seconds;
        secondsPast = 0;

        timer.InvokeRepeating("OneSecondPast",1f,1f);
        timer.Invoke("Smooth", 0.5f);

    }

    public static void StopCountDown()
    {
        try
        {
            timer.CancelInvoke("OneSecondPast");
        }
        catch { }
    }

    public static void Initialize()
    {
        rotation = startRotation;
    }


    private void OneSecondPast()
    {
        secondsPast++;
        
        if(secondsPast==target)
        {
            if (OnTic != null)
                OnTic();
            StopCountDown();
        }

        rotation = Quaternion.Euler(0f,0f,  -360 * secondsPast / target + 90);

        if(secondsPast!=target)
            Invoke("Smooth", 0.5f);
        
    }

    private void Smooth()
    {
        rotation = Quaternion.Euler(0f, 0f, -360 * (secondsPast + 0.5f )/ target + 90);
    }

}
