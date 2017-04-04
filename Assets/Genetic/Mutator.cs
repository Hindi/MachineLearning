using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Mutator
{
    public abstract float mutate(float value, float min, float max, float variance);
}
