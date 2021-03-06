﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenomeOwner : MonoBehaviour
{
    [SerializeField]
    protected Population population;

    private Genome genome;
    public Genome Genome
    {
        get {
            return genome;
        }

        set {
            genome = value;
        }
    }

    protected virtual void Awake()
    {
        initializeGenome();
    }


    public void updateFitness()
    {
        Genome.Fitness = calculateFitness();
    }

    protected abstract float calculateFitness();
    protected abstract void initializeGenome();
    public abstract void reset();
}
