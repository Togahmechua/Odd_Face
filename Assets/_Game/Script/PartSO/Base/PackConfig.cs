using UnityEngine;
using System;

[Serializable]
public class PackConfig
{
    [Header("Single Parts")]
    public PartConfig body;
    public PartConfig face;
    public PartConfig hair;
    public PartConfig mouth;
    public PartConfig nose;
    public PartConfig other;

    [Header("Pair Parts")]
    public PartPairConfig eyes;
    public PartPairConfig eyeBrows;
    public PartPairConfig hands;
    public PartPairConfig legs;
    public PartPairConfig otherPair;
}
