using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenomeBux : Genome
{
    public static float CrossoverProbability;

    public override void crossOver(ref Genome genomeB)
    {
        for(int index = 0; index < Genes.Count; ++index)
        {
            Gene gene = Genes[index];
            if (Random.Range(0, 1) <= CrossoverProbability)
            {
                Gene tempGene = gene;
                Genes[index] = genomeB.Genes[index];
                genomeB.Genes[index] = gene;
            }
        }
    }

    public override Genome clone()
    {
        GenomeBux newGenome = new GenomeBux();

        List<Gene> list = cloneGenes();
        newGenome.Genes = list;

        newGenome.Mutator = Mutator;

        return newGenome;
    }
}