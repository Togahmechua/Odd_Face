using UnityEngine;

[CreateAssetMenu(fileName = "LegSO", menuName = "ScriptableObjects/Pair/LegSO", order = 4)]
public class LegSO : PairPartSO<PartPairConfig>
{
    public PartPairConfig Get()
    {
        return GetRandom();
    }
}