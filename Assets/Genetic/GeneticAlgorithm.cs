using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour {

    [SerializeField]
    private Population population;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            population.createNewGeneration();
        }
    }
}
