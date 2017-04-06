using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField]
    private GameObject foodPrefab;

    [SerializeField]
    private int intialFoodQuantity;

    List<Food> foods;

    private Vector2 wordSize;

    private void Awake()
    {
        foods = new List<Food>();
    }

    private Food spawnFoodRandom()
    {
        float posX = Random.Range(0, wordSize.x);
        float posY = Random.Range(0, wordSize.y);
        Food newFood = Instantiate(foodPrefab, new Vector3(posX, 0, posY), Quaternion.identity).GetComponent<Food>();
        newFood.init(this);
        return newFood;
    }

    public override void reset()
    {

    }

    public void spawnInitialFoodBatch(Vector2 nWordSize)
    {
        wordSize = nWordSize;
        for (int i = 0; i < intialFoodQuantity; ++i)
        {
            foods.Add(spawnFoodRandom());
        }
    }

    public void goEaten(Food food)
    {
        float posX = Random.Range(0, wordSize.x);
        float posY = Random.Range(0, wordSize.y);
        food.teleportTo(new Vector3(posX, 0, posY));
    }
}
