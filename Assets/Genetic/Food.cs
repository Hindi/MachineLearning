using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private FoodManager foodManager;

    public void init(FoodManager nFoodManager)
    {
        foodManager = nFoodManager;
    }

    public void eat()
    {
        foodManager.goEaten(this);
    }

    public void teleportTo(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
