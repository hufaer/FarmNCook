using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speedOfAttraction = 3f;
    [SerializeField] float distance = 1.15f;
    [SerializeField] float timeToDissapear = 10f;

    public Item item;
    public int count = 1;

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    private void Awake()
    {
        player = GameManager.instance.player.transform;
    }

    private void Update()
    {
        // Если время до исчезновения объекта закончилось - удаляем
        timeToDissapear -= Time.deltaTime;
        if (timeToDissapear <= 0)
        {
            Destroy(gameObject);
        }


        float distanceBetweenPlayerAndObject = Vector3.Distance(transform.position, player.position);

        if (distanceBetweenPlayerAndObject > distance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, speedOfAttraction * Time.deltaTime);
        if (distanceBetweenPlayerAndObject < 0.1f)
        {
            Destroy(gameObject);
            if (GameManager.instance.inventory != null)
            {
                GameManager.instance.inventory.Add(item, count);
                GameManager.instance.toolbar.ShowInventory();
            }
            else
            {
                Debug.LogWarning("no space left");
            }
        }
    }

}
