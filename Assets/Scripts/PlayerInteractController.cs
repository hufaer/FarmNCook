using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody2D rb;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfArea = 1.2f;
    

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !PauseGame.isPaused)
        {
            Interact();
        }
    }


    private void Interact()
    {
        Vector2 position = rb.position + player.lastPosition * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfArea);

        foreach (Collider2D collider in colliders)
        {
            Interactable interactableItem = collider.GetComponent<Interactable>();
            if (interactableItem != null)
            {
                interactableItem.Interact(player);
                break;
            }
        }
    }

}
