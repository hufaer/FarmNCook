using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Actions/Milking Action")]
public class MilkingCowAction : ToolAction
{
    [SerializeField] Item milk;

    public override bool OnApply(Vector2 position)
    {
        Debug.Log("In apply");
        float sizeOfArea = 0.65f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfArea);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Cow")
            {
                Debug.Log("There is a cow");
                Cow cow = collider.gameObject.GetComponent<Cow>();
                if (cow.isReadyForMilking)
                {
                    GameManager.instance.inventory.Add(milk, 1);
                    GameManager.instance.toolbar.ShowInventory();
                    cow.isReadyForMilking = false;
                    return true;
                }
            }
        }

        return false;
    }
}
