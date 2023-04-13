using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : Interactable
{
    [SerializeField] AlchemyManager alchemyManager;
    public bool isInteractingNow = false;

    public override void Interact(PlayerMovement player)
    {
        if (!isInteractingNow)
        {
            alchemyManager.StartAlchemy();
        }
    }
}
