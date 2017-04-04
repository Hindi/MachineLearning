using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutatorRandom : Mutator
{
    public override float mutate(float value, float min, float max, float variance)
    {
        value = Mathf.Max(Mathf.Min(value + Random.Range(0, variance), max), min);
        return value;
    }
}
