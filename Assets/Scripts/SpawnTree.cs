using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTree : MonoBehaviour
{
    [SerializeField] int timeToGrow = 80;
    [SerializeField] GameObject tree;

    private int currTime = 0;

    public void Tick()
    {
        currTime++;
        
    }
}
