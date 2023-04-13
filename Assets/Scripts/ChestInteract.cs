using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : Interactable
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool isOpened;
    public override void Interact(PlayerMovement player)
    {
        if (!isOpened)
        {
            isOpened = true;
            closed.SetActive(false);
            opened.SetActive(true);
        } else
        {
            isOpened = false;
            closed.SetActive(true);
            opened.SetActive(false);

        }
    }
}
