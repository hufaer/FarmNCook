using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IDataPersistence
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    public Vector2 lastPosition;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement.x = x;
        movement.y = y;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (x != 0 || y != 0)
        {
            lastPosition.x = x;
            lastPosition.y = y;

            animator.SetFloat("LastHorizontal", x);
            animator.SetFloat("LastVertical", y);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    public void LoadData(GameData gameData)
    {
        rb.position = gameData.playerPosition;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerPosition = rb.position;
    }
}
