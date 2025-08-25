using UnityEngine;

[CreateAssetMenu(fileName = "EyeBrowSO", menuName = "ScriptableObjects/Pair/EyeBrowSO", order = 2)]
public class EyeBrowSO : PairPartSO<PartPairConfig>
{
    public PartPairConfig Get()
    {
        return GetRandom();
    }
}