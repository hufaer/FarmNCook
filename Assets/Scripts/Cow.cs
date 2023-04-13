using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : TimeManager
{
    [SerializeField] int timeToGiveAMilk;
    int timeToMove = 7;
    int timeToSpawnMilk = 60;
    int currTime = 0;
    int currTimeForMilk = 0;
    Animator animator;

    private bool isFacingRight = true;
    public bool isReadyForMilking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action += Tick;
        Init();
    }

    public void Tick()
    {
        if (currTimeForMilk < timeToSpawnMilk)
        {
            currTimeForMilk++;
        } else
        {
            isReadyForMilking = true;
            currTimeForMilk = 0;
        }



        if (currTime < timeToMove)
        {
            currTime++;
        }
        else
        {
            StartCoroutine(Walk());
            currTime = 0;
        }
    }

    public IEnumerator Walk()
    {
        float x = UnityEngine.Random.Range(-12.2f, -10.3f);
        float y = UnityEngine.Random.Range(-4.3f, -3.5f);

        Debug.Log(x + " " + y);
        Debug.Log(transform.position.x + " " + transform.position.y);
        Vector2 destinationPoint = new Vector2(x, y);

        if (transform.position.x - destinationPoint.x > 0 && isFacingRight)
        {
            FlipSprite();
            isFacingRight = false;
        }

        if (transform.position.x - destinationPoint.x < 0 && !isFacingRight)
        {
            FlipSprite();
            isFacingRight = true;
        }

        while (Vector2.Distance(transform.position, destinationPoint) > 0.001f)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, destinationPoint, 0.5f * Time.deltaTime);
            yield return null ;
        }
        animator.SetBool("isWalking", false);
        Debug.Log("End");
    }

    public void FlipSprite()
    {
        Vector3 currScale = transform.localScale;
        currScale.x *= -1;
        transform.localScale = currScale;
    }
}
