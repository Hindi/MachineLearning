using UnityEngine;

public class GenomeOwnerTest : GenomeOwner
{
    protected override void initializeGenome()
    {
        Genome = new GenomeBux();

        Genome.Genes.Add(new Gene(Random.Range(0.0f, 1.0f), 0, 1, 0.5f));  //R
        Genome.Genes.Add(new Gene(Random.Range(0.0f, 1.0f), 0, 1, 0.5f));  //G
        Genome.Genes.Add(new Gene(Random.Range(0.0f, 1.0f), 0, 1, 0.5f));  //B

        Genome.Mutator = new MutatorRandom();
    }

    protected override float calculateFitness()
    {
        float fitness = 0;
        fitness += (0.3f - (Genome.Genes[0].Value) * 0.3f);
        fitness += (0.3f - (Genome.Genes[1].Value) * 0.3f);
        fitness += (0.3f - (Genome.Genes[2].Value) * 0.3f);
        return fitness;
    }
    
    void Update()
    {
        Color curColor = new Color(Genome.Genes[0].Value, Genome.Genes[1].Value, Genome.Genes[2].Value);
        GetComponent<Renderer>().material.color = curColor;
    }
}
