
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private Population population;

    [SerializeField]
    private FoodManager foodManager;

    [SerializeField]
    private Vector2 wordSize;

    private void Start()
    {
        foodManager.spawnInitialFoodBatch(wordSize);
        population.createNewPopulation(wordSize);
    }

    public Vector3 getRandomPositionInWorld()
    {
        Vector3 v = new Vector3(Random.Range(0, wordSize.x), 0, Random.Range(0, wordSize.y));
        return v;
    }
}
