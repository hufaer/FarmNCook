using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreetingsPanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Destroy(gameObject);
        }
    }
}
