using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Genome : IComparer
{

    private float fitness;
    public float Fitness
    {
        get {
            return fitness;
        }

        set {
            fitness = value;
        }
    }

    private List<Gene> genes;
    public List<Gene> Genes
    {
        get {
            return genes;
        }

        set {
            genes = value;
        }
    }

    private Mutator mutator;
    public Mutator Mutator
    {
        get {
            return mutator;
        }

        set {
            mutator = value;
        }
    }

    public abstract void crossOver(ref Genome genomeB);
    public abstract Genome clone();
    
    public Genome()
    {
        Genes = new List<Gene>();
    }

    public int Compare(object x, object y)
    {
        return (int)(10 * (((Genome)x).Fitness - ((Genome)y).Fitness));
    }

    protected List<Gene> cloneGenes()
    {
        List<Gene> newList = new List<Gene>();
        foreach(Gene gene in Genes)
        {
            newList.Add(gene.clone());
        }
        return newList;
    }

    public void mutate(float probability)
    {
        foreach (Gene gene in Genes)
        {
            if (Random.Range(0, 1) <= probability)
            {
                gene.mutate(Mutator);
            }
        }
    }
}
