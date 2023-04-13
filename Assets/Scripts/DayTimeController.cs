using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class DayTimeController : MonoBehaviour, IDataPersistence
{
    const float secondsInDay = 86400f;

    [SerializeField] float startTime = 12;
    float currTime;
    [SerializeField] Color nightColor;
    [SerializeField] AnimationCurve nightCurve;
    [SerializeField] Color dayColor = Color.white;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Text text;
    [SerializeField] float playPhase = 60f;
    [SerializeField] Light2D globalLight;
    private int currDay = 0;
    List<TimeManager> timeManagers = new List<TimeManager>();
    
    public void AddTimeManager(TimeManager timeManager)
    {
        timeManagers.Add(timeManager);
    }

    public void DeleteTimeManager(TimeManager timeManager)
    {
        timeManagers.Remove(timeManager);
    } 

    private void Start()
    {
        currTime = startTime * 3600f;    
    }

    float Hours
    {
        get { return currTime / 3600f; }
    }
    float Minutes
    {
        get { return currTime % 3600f / 60f; ; }
    }

    int prevPhase = 0;
    private void Update()
    {
        currTime += Time.deltaTime * timeScale;
        text.text = "Δενό " + (currDay + 1) + " " + ((int)Hours).ToString("00") + ":" + ((int)Minutes).ToString("00");
        float v = nightCurve.Evaluate(Hours);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globalLight.color = c;
        if (currTime > secondsInDay)
        {
            NextDay();
        }

        int currPhase = (int)(currTime / playPhase);

        if (prevPhase != currPhase)
        {
            prevPhase = currPhase;
            for (int i = 0; i < timeManagers.Count; ++i)
            {
                timeManagers[i].Invoke();
            }
        }
    }

    private void NextDay()
    {
        currTime = 0;
        currDay++;
    }

    public void LoadData(GameData gameData)
    {
        currTime = gameData.timeOfDay;
        currDay = gameData.day;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.timeOfDay = currTime;
        gameData.day = currDay;
    }
}
