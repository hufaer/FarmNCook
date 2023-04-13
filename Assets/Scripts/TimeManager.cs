using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Action action;

    public void Init()
    {
        GameManager.instance.timeController.AddTimeManager(this);
    }

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        GameManager.instance.timeController.DeleteTimeManager(this);
    }

    public void Invoke()
    {
        action?.Invoke();
    }
}
