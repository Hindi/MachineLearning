using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    private float variance;
    private float minimum;
    private float maximum;

    private float value;
    public float Value
    {
        get {
            return value;
        }

        set {
            this.value = value;
        }
    }

    public Gene(float nValue, float min, float max, float nVariance)
    {
        Value = nValue;
        minimum = min;
        maximum = max;
        variance = nVariance;
    }

    public void mutate(Mutator mutator)
    {
        Value = mutator.mutate(Value, minimum, maximum, variance);
    }

    public Gene clone()
    {
        return new Gene(Value, minimum, maximum, variance);
    }
}
