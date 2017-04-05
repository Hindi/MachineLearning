using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Population : MonoBehaviour
{
    List<GenomeOwner> genomeOwners = new List<GenomeOwner>();

    [SerializeField]
    private GameObject genomeOwnerPrefab;
    
    [SerializeField]
    private int numberOfIndividuals;

    [SerializeField]
    private float percentOfMatingPerGeneration;

    [SerializeField]
    private float mutationProbability;
    
    protected float meanFitness;

    public void createNewPopulation(Vector2 worldSize)
    {
        for(int i = 0; i < numberOfIndividuals; ++i)
        {
            float posX = Random.Range(0, worldSize.x);
            float posY = Random.Range(0, worldSize.y);
            genomeOwners.Add(Instantiate(genomeOwnerPrefab, new Vector3(posX, 0, posY), Quaternion.identity).GetComponent<GenomeOwner>());
        }
    }

    public void createNewGeneration()
    {
        //Update fitness
        foreach(GenomeOwner test in genomeOwners)
        {
            test.updateFitness();
        }

        //Sort genomes by fitness
        genomeOwners.Sort((g1, g2) => (int) (g1.Genome.Fitness - g2.Genome.Fitness));
        
        //Update the mean fitness of the population
        meanFitness = calculateMeanFitness();
        
        //Create the new population
        foreach(GenomeOwner owner in genomeOwners)
        {
            int numberOfChildren = (int)(owner.Genome.Fitness  / meanFitness);

            int childrenCount = 0;
            for (int i = 0; i < genomeOwners.Count && childrenCount < numberOfChildren; ++i)
            {
                if(genomeOwners[i].Genome.Fitness < meanFitness)
                {
                    genomeOwners[i].Genome = owner.Genome.clone();
                    ++numberOfChildren;
                }
            }
        }


        //Mate the new population
        int matingQuantity = (int)percentOfMatingPerGeneration * genomeOwners.Count / 2;
        for (int i = 0; i < matingQuantity; ++i)
        {
            Genome genomeA = genomeOwners[Random.Range(0, genomeOwners.Count)].Genome;
            Genome genomeB = genomeOwners[Random.Range(0, genomeOwners.Count)].Genome;
            genomeA.crossOver(ref genomeB);
        }

        //Mutate the new population
        foreach (GenomeOwner genome in genomeOwners)
        {
            genome.Genome.mutate(mutationProbability);
        }
    }
    
    float calculateMeanFitness()
    {
        float sumFitness = 0;
        foreach (GenomeOwner owner in genomeOwners)
            sumFitness += owner.Genome.Fitness;
        return sumFitness / genomeOwners.Count;
    }
}
